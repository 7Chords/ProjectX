using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace GameCore.Util
{
    /// <summary>
    /// Timeline播放特效节点
    /// </summary>
    [System.Serializable]
    public class PlayParticleEffectPlayableAsset : PlayableAsset
    {
        [Header("特效名称")]
        public string effectName;

        [Header("特效资源预制（只在编辑器预览下用）")]
        public GameObject effectPrefab;

        [Header("位置设置（为空则使用持有者位置）")]
        public ExposedReference<Transform> targetTransform;

        [Header("位置偏移")]
        public Vector3 positionOffset = Vector3.zero;

        [Header("使用局部坐标偏移还是世界坐标偏移")]
        public bool useLocalOffset = true;

        [Header("旋转设置")]
        public Vector3 rotation = Vector3.zero;

        [Header("是否相对于目标旋转")]
        public bool relativeToTarget = false;

        [Header("缩放设置")]
        public Vector3 scale = Vector3.one;

        [Header("是否播放完自动销毁")]
        public bool autoDestroy = true;

        [Header("销毁延迟时间")]
        public float destroyDelay = 0f;

        [Header("跟随目标移动")]
        public bool followTarget = false;

        [Header("粒子速度")]
        public float simulateSpeed;

        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph _graph, GameObject _go)
        {
            var scriptPlayable = ScriptPlayable<PlayParticleEffectPlayableBehaviour>.Create(_graph);
            var behaviour = scriptPlayable.GetBehaviour();

            // 传递参数到行为类
            behaviour.effectName = effectName;
            behaviour.effectPrefab = effectPrefab;
            behaviour.targetTransform = targetTransform.Resolve(_graph.GetResolver());
            behaviour.positionOffset = positionOffset;
            behaviour.useLocalOffset = useLocalOffset;
            behaviour.rotation = rotation;
            behaviour.relativeToTarget = relativeToTarget;
            behaviour.scale = scale;
            behaviour.autoDestroy = autoDestroy;
            behaviour.destroyDelay = destroyDelay;
            behaviour.followTarget = followTarget;
            behaviour.directorGameObject = _go;
            behaviour.simulateSpeed = simulateSpeed;

            return scriptPlayable;
        }
    }
}