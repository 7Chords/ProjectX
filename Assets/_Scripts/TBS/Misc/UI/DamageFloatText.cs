using DG.Tweening;
using GameCore.Util;
using SCFrame;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.TBS
{
    /// <summary>
    /// TBSÉËº¦Æ®×Ö
    /// </summary>
    public class DamageFloatText : MonoBehaviour
    {
        public Text damageText;
        public CanvasGroup canvasGroup;
        public float biggerDuration;
        public float floatDuration;
        public float fadeOutDuration;

        private TweenContainer _m_tweenContainer;
        public void Initialize(int _damage)
        {
            _m_tweenContainer = new TweenContainer();
            damageText.text = _damage.ToString();
            Tween biggerTween = gameObject.GetRectTransform().DOScale(1, biggerDuration).OnStart(() =>
            {
                gameObject.GetRectTransform().localScale = Vector3.zero;
            });

            Sequence seq = DOTween.Sequence();
            Tween floatTween = gameObject.GetRectTransform().DOLocalMoveY(
                gameObject.GetRectTransform().localPosition.y + 200f
                , floatDuration);

            Tween fadeOutTween = canvasGroup.DOFade(0, fadeOutDuration).OnComplete(() =>
            {
                SCCommon.DestoryGameObject(gameObject);
            });

            seq.Append(floatTween);
            seq.Append(fadeOutTween);

            _m_tweenContainer.RegDoTween(biggerTween);
            _m_tweenContainer.RegDoTween(seq);

        }

        private void OnDestroy()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }



    }
}
