using UnityEngine;

namespace SCFrame.UI
{
    /// <summary>
    /// UIMonoÅäÖÃ³éÏó»ùÀà
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class _ASCUIMonoBase : MonoBehaviour
    {
        public GameObject uiMask;
        public CanvasGroup canvasGroup;
        public float fadeInDuration;
        public float fadeOutDuration;
    }
}
