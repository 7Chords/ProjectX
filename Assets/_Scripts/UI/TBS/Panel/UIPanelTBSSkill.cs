using DG.Tweening;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSSkill : _ASCUIPanelBase<UIMonoTBSSkill>
    {
        private TweenContainer _m_tweenContainer;

        public UIPanelTBSSkill(UIMonoTBSSkill _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }
        public override void OnInitialize()
        {
            _m_tweenContainer = new TweenContainer();
            mono.canvasGroup.alpha = 0;


        }
        public override void OnDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();

        }

        public override void OnHidePanel()
        {
            Tween tween = mono.canvasGroup.DOFade(0, mono.fadeOutDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 1;
            });
            _m_tweenContainer?.RegDoTween(tween);
        }


        public override void OnShowPanel()
        {

            Tween tween = mono.canvasGroup.DOFade(1, mono.fadeInDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 0;
            });
            _m_tweenContainer?.RegDoTween(tween);
        }
    }
}
