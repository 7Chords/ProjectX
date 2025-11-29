using UnityEngine;

namespace SCFrame.UI
{
    /// <summary>
    /// UIMono配置抽象基类
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class _ASCUIMonoBase : MonoBehaviour
    {
        [Header("遮罩")]
        public GameObject uiMask;
        [Header("画布Group")]
        public CanvasGroup canvasGroup;
        [Header("淡入时间")]
        public float fadeInDuration;
        [Header("淡出时间")]
        public float fadeOutDuration;
        [Header("UI动画机")]
        public Animator uiAnimator;
        [Header("打开UI动画名")]
        public string showUIName;
        [Header("关闭UI动画名")]
        public string hideUIName;

    }
}
