using DG.Tweening;
using SCFrame;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.OW
{
    public class InteractiveText : MonoBehaviour
    {
        [Header("Tip文本")]
        public Text tipText;
        [Header("画布组件")]
        public CanvasGroup canvasGroup;
        [Header("淡入时间")]
        public float fadeInDuration;
        [Header("淡出时间")]
        public float fadeOutDuration;

        private TweenContainer _m_tweenContainer;
        private Transform _m_followTran;
        private RectTransform _m_rectTransform;
        public void Initialize(string _content,Transform _followTran)
        {
            _m_rectTransform = gameObject.GetRectTransform();
            _m_followTran = _followTran;
            _m_tweenContainer = new TweenContainer();

            tipText.text = _content;
            canvasGroup.alpha = 0f;

            _m_tweenContainer.RegDoTween(canvasGroup.DOFade(1, fadeInDuration));

            this.OnUpdate(onUpdate);
        }

        public void Discard()
        {
            this.RemoveUpdate(onUpdate);

            canvasGroup.alpha = 1f;

            _m_tweenContainer.RegDoTween(canvasGroup.DOFade(0, fadeOutDuration)
                .OnComplete(()=> 
                {
                    SCCommon.DestoryGameObject(gameObject);
                }));
        }

        private void onUpdate()
        {
            if(_m_rectTransform)
                _m_rectTransform.localPosition = SCUICommon.WorldPointToUIPoint(SCGame.instance.topLayerRoot.GetRectTransform(), _m_followTran.position);
        }

        private void OnDestroy()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }
    }
}
