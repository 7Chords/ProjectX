using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


namespace GameCore.Editor
{
    /// <summary>
    /// 自动为预制体生成适配大小的碰撞体编辑器拓展（最终修复版：Vector3运算+偏移问题）
    /// </summary>
    public class AutoColliderGenerator : EditorWindow
    {
        // 碰撞体类型枚举
        private enum ColliderType
        {
            BoxCollider,
            MeshCollider
        }

        private ColliderType selectedColliderType = ColliderType.BoxCollider;
        private bool overwriteExistingCollider = true; // 是否覆盖已有碰撞体

        // 菜单栏入口
        [MenuItem("MBC编辑器/AutoColliderGenerator/OpenGenerator")]
        public static void ShowWindow()
        {
            GetWindow<AutoColliderGenerator>("Auto Collider Generator");
        }

        // 快速生成BoxCollider（快捷菜单）
        [MenuItem("MBC编辑器/AutoColliderGenerator/Quick Generate BoxCollider")]
        private static void QuickGenerateBoxCollider()
        {
            GenerateCollidersForSelectedPrefabs(ColliderType.BoxCollider, true);
        }

        // 快速生成MeshCollider（快捷菜单）
        [MenuItem("MBC编辑器/AutoColliderGenerator/Quick Generate MeshCollider")]
        private static void QuickGenerateMeshCollider()
        {
            GenerateCollidersForSelectedPrefabs(ColliderType.MeshCollider, true);
        }

        private void OnGUI()
        {
            GUILayout.Space(10);
            GUILayout.Label("Collider Generation Settings", EditorStyles.boldLabel);
            GUILayout.Space(5);

            // 选择碰撞体类型
            selectedColliderType = (ColliderType)EditorGUILayout.EnumPopup("Collider Type", selectedColliderType);

            // 是否覆盖已有碰撞体
            overwriteExistingCollider = EditorGUILayout.Toggle("Overwrite Existing Collider", overwriteExistingCollider);

            GUILayout.Space(10);

            // 生成按钮
            if (GUILayout.Button("Generate Collider for Selected Prefabs", GUILayout.Height(30)))
            {
                GenerateCollidersForSelectedPrefabs(selectedColliderType, overwriteExistingCollider);
                EditorUtility.DisplayDialog("Success", "Collider generation completed!", "OK");
            }

            // 提示信息
            GUILayout.Space(10);
            EditorGUILayout.HelpBox("1. Select prefabs in Project window first\n2. Click generate button to create colliders", MessageType.Info);
        }

        /// <summary>
        /// 为选中的预制体生成碰撞体
        /// </summary>
        private static void GenerateCollidersForSelectedPrefabs(ColliderType colliderType, bool overwrite)
        {
            Object[] selectedPrefabs = Selection.GetFiltered<Object>(SelectionMode.Assets);
            if (selectedPrefabs.Length == 0)
            {
                EditorUtility.DisplayDialog("Warning", "No prefabs selected in Project window!", "OK");
                return;
            }

            int successCount = 0;
            foreach (Object obj in selectedPrefabs)
            {
                if (PrefabUtility.IsPartOfPrefabAsset(obj))
                {
                    GameObject prefab = obj as GameObject;
                    if (prefab != null && GenerateColliderForPrefab(prefab, colliderType, overwrite))
                    {
                        successCount++;
                    }
                }
            }

            AssetDatabase.Refresh();
            Debug.Log($"AutoColliderGenerator: Successfully generated colliders for {successCount}/{selectedPrefabs.Length} prefabs");
        }

        /// <summary>
        /// 为单个预制体生成碰撞体（修复Vector3除法+偏移问题）
        /// </summary>
        private static bool GenerateColliderForPrefab(GameObject prefab, ColliderType colliderType, bool overwrite)
        {
            // 创建隐藏的临时父物体
            GameObject tempParent = new GameObject("TempColliderCalculation");
            tempParent.hideFlags = HideFlags.HideAndDontSave;

            // 临时实例化预制体并重置变换
            GameObject tempInstance = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            if (tempInstance == null)
            {
                DestroyImmediate(tempParent);
                return false;
            }

            tempInstance.transform.SetParent(tempParent.transform);
            tempInstance.transform.localPosition = Vector3.zero;
            tempInstance.transform.localRotation = Quaternion.identity;
            tempInstance.transform.localScale = Vector3.one;

            try
            {
                Renderer[] renderers = tempInstance.GetComponentsInChildren<Renderer>();
                if (renderers.Length == 0)
                {
                    Debug.LogWarning($"AutoColliderGenerator: No renderers found in prefab {prefab.name}, skip collider generation");
                    return false;
                }

                // 计算合并后的世界包围盒
                Bounds combinedWorldBounds = renderers[0].bounds;
                for (int i = 1; i < renderers.Length; i++)
                {
                    combinedWorldBounds.Encapsulate(renderers[i].bounds);
                }

                // ========== 核心修复：正确计算本地尺寸（替换Vector3除法） ==========
                // 1. 将世界包围盒中心转换为预制体本地坐标
                Vector3 localCenter = tempInstance.transform.InverseTransformPoint(combinedWorldBounds.center);

                // 2. 计算缩放的倒数（避免Vector3除法，手动计算每个分量）
                Vector3 lossyScale = tempInstance.transform.lossyScale;
                // 防止除以0的异常（给极小值兜底）
                float scaleX = Mathf.Approximately(lossyScale.x, 0) ? 1e-6f : lossyScale.x;
                float scaleY = Mathf.Approximately(lossyScale.y, 0) ? 1e-6f : lossyScale.y;
                float scaleZ = Mathf.Approximately(lossyScale.z, 0) ? 1e-6f : lossyScale.z;
                // 显式创建缩放倒数的Vector3（替代除法）
                Vector3 scaleReciprocal = new Vector3(1f / scaleX, 1f / scaleY, 1f / scaleZ);

                // 3. 按缩放倒数校正包围盒尺寸（分量级相乘，替代除法）
                Vector3 localSize = Vector3.Scale(combinedWorldBounds.size, scaleReciprocal);

                // 打开预制体编辑模式
                GameObject prefabRoot = PrefabUtility.LoadPrefabContents(PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab));

                // 处理已有碰撞体
                Collider existingCollider = prefabRoot.GetComponent<Collider>();
                if (existingCollider != null)
                {
                    if (overwrite)
                    {
                        DestroyImmediate(existingCollider);
                    }
                    else
                    {
                        Debug.LogWarning($"AutoColliderGenerator: Prefab {prefab.name} already has a collider, skip (overwrite = false)");
                        PrefabUtility.UnloadPrefabContents(prefabRoot);
                        return false;
                    }
                }

                // 生成碰撞体
                switch (colliderType)
                {
                    case ColliderType.BoxCollider:
                        GenerateBoxCollider(prefabRoot, localCenter, localSize);
                        break;
                    case ColliderType.MeshCollider:
                        GenerateMeshCollider(prefabRoot, tempInstance);
                        break;
                }

                // 保存预制体修改
                PrefabUtility.SaveAsPrefabAsset(prefabRoot, PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(prefab));
                PrefabUtility.UnloadPrefabContents(prefabRoot);

                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"AutoColliderGenerator: Failed to generate collider for {prefab.name} - {e.Message}");
                return false;
            }
            finally
            {
                DestroyImmediate(tempInstance);
                DestroyImmediate(tempParent);
            }
        }

        /// <summary>
        /// 生成BoxCollider（使用校正后的本地坐标）
        /// </summary>
        private static void GenerateBoxCollider(GameObject target, Vector3 localCenter, Vector3 localSize)
        {
            BoxCollider boxCollider = target.AddComponent<BoxCollider>();
            boxCollider.center = localCenter;
            boxCollider.size = localSize;
        }

        /// <summary>
        /// 生成MeshCollider
        /// </summary>
        private static void GenerateMeshCollider(GameObject target, GameObject tempInstance)
        {
            MeshCollider meshCollider = target.AddComponent<MeshCollider>();

            MeshFilter meshFilter = tempInstance.GetComponent<MeshFilter>();
            if (meshFilter != null && meshFilter.sharedMesh != null)
            {
                meshCollider.sharedMesh = meshFilter.sharedMesh;
            }
            else
            {
                MeshCombineUtility.CombineMeshes(tempInstance, out Mesh combinedMesh);
                meshCollider.sharedMesh = combinedMesh;
                // 正确保存Mesh文件（避免路径错误）
                string prefabPath = AssetDatabase.GetAssetPath(target);
                string meshPath = prefabPath.Substring(0, prefabPath.LastIndexOf('.')) + "_collider_mesh.asset";
                AssetDatabase.CreateAsset(combinedMesh, meshPath);
            }

            meshCollider.convex = true;
        }
    }

    /// <summary>
    /// Mesh合并工具类
    /// </summary>
    public static class MeshCombineUtility
    {
        public static void CombineMeshes(GameObject root, out Mesh combinedMesh)
        {
            combinedMesh = new Mesh();
            List<CombineInstance> combineInstances = new List<CombineInstance>();

            MeshFilter[] meshFilters = root.GetComponentsInChildren<MeshFilter>();
            foreach (MeshFilter mf in meshFilters)
            {
                if (mf.sharedMesh == null) continue;

                CombineInstance ci = new CombineInstance();
                ci.mesh = mf.sharedMesh;
                ci.transform = mf.transform.localToWorldMatrix;
                combineInstances.Add(ci);
            }

            combinedMesh.CombineMeshes(combineInstances.ToArray(), true, true);
            combinedMesh.RecalculateBounds();
            combinedMesh.RecalculateNormals();
        }
    }
}
