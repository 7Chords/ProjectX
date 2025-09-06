#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public static class CustomTextMenu
{
    [MenuItem("GameObject/UI/TextSC", false, 2001)]
    static void CreateCustomText(MenuCommand menuCommand)
    {
        // 创建GameObject
        GameObject go = new GameObject("TextSC");

        // 确保有Canvas作为父对象
        if (Selection.activeGameObject != null &&
            Selection.activeGameObject.GetComponentInParent<Canvas>())
        {
            go.transform.SetParent(Selection.activeGameObject.transform, false);
        }
        else
        {
            Canvas canvas = Object.FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                GameObject canvasGo = new GameObject("Canvas");
                canvas = canvasGo.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGo.AddComponent<CanvasScaler>();
                canvasGo.AddComponent<GraphicRaycaster>();
            }
            go.transform.SetParent(canvas.transform, false);
        }

        // 添加CustomText组件
        TextSC customText = go.AddComponent<TextSC>();
        customText.text = "New Text";
        customText.color = Color.white;
        customText.alignment = TextAnchor.MiddleCenter;

        // 设置RectTransform
        RectTransform rectTransform = go.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(150f, 30f);
        rectTransform.anchoredPosition = Vector2.zero;

        // 注册Undo操作
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);

        // 选择新创建的对象
        Selection.activeObject = go;
    }
}
#endif