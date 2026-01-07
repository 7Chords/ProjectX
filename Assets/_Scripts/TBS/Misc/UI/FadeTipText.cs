using DG.Tweening;
using SCFrame;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.TBS
{
    public class FadeTipText : MonoBehaviour
    {
        [Header("Tip文本")]
        public Text tipText;
        [Header("画布组件")]
        public CanvasGroup canvasGroup;
        [Header("淡入持续时间")]
        public float fadeInDuration;
        [Header("淡出持续时间")]
        public float fadeOutDuration;
        [Header("静止持续时间")]
        public float stopDuration;

        private TweenContainer _m_tweenContainer;

        public void Initialize(string _content)
        {
            _m_tweenContainer = new TweenContainer();


            tipText.text = _content;

            //初始状态
            canvasGroup.alpha = 0f;


            //组合动画序列
            Sequence mainSequence = DOTween.Sequence();
            if (fadeInDuration > 0)
            {
                Tween fadeInTween = canvasGroup.DOFade(1, fadeInDuration);
                mainSequence.Append(fadeInTween);
            }
            mainSequence.AppendInterval(stopDuration);

            if (fadeOutDuration > 0)
            {
                //创建淡出动画
                Tween fadeOutTween = canvasGroup.DOFade(0, fadeOutDuration).OnComplete(() =>
                {
                    SCCommon.DestoryGameObject(gameObject);
                });
                mainSequence.Append(fadeOutTween);
            }
            _m_tweenContainer.RegDoTween(mainSequence);
        }

        private void OnDestroy()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }

    }
}
