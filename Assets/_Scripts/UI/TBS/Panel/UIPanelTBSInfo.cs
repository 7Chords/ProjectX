using DG.Tweening;
using GameCore.TBS;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;

namespace GameCore.UI
{
    public class UIPanelTBSInfo : _ASCUIPanelBase<UIMonoTBSInfo>
    {
        private TweenContainer _m_tweenContainer;
        private UIPanelTBSInfoContainer _m_infoContainer;
        private UIPanelTBSCharacterActionContainer _m_characterActionContainer;
        public UIPanelTBSInfo(UIMonoTBSInfo _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void OnInitialize()
        {
            mono.canvasGroup.alpha = 0;

            _m_tweenContainer = new TweenContainer();
            if (mono.monoContainer != null)
                _m_infoContainer = new UIPanelTBSInfoContainer(mono.monoContainer);

            if (mono.monoCharacterActionContainer != null)
                _m_characterActionContainer = new UIPanelTBSCharacterActionContainer(mono.monoCharacterActionContainer);
        }
        public override void OnDiscard()
        {
            if (_m_infoContainer != null)
                _m_infoContainer.Discard();

            if (_m_characterActionContainer != null)
                _m_characterActionContainer.Discard();

            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
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
            _refreshPanel();
        }

        private void _refreshPanel()
        {
            _refreshInfoContainer();
        }

        private void _refreshInfoContainer()
        {
            List<TBSActorInfo> actorInfoList = SCModel.instance.tbsModel.battleInfo.playerTeamInfo.actorInfoList;
            if (actorInfoList == null || actorInfoList.Count == 0)
                return;
            _m_infoContainer.SetInfoList(actorInfoList);
            _m_infoContainer.ShowPanel();


        }
    }
}
