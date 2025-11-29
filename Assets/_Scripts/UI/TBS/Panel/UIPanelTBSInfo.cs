using DG.Tweening;
using GameCore.TBS;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using System.Linq;

namespace GameCore.UI
{
    public class UIPanelTBSInfo : _ASCUIPanelBase<UIMonoTBSInfo>
    {
        //private TweenContainer _m_tweenContainer;
        private UIPanelTBSInfoContainer _m_infoContainer;
        private UIPanelTBSCharacterActionContainer _m_characterActionContainer;
        private List<TBSActorInfo> _m_allActorInfoList;
        public UIPanelTBSInfo(UIMonoTBSInfo _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }
        public override void BeforeDiscard()
        {
            if (_m_infoContainer != null)
                _m_infoContainer.Discard();

            if (_m_characterActionContainer != null)
                _m_characterActionContainer.Discard();

            //_m_tweenContainer?.KillAllDoTween();
            //_m_tweenContainer = null;
        }

        public override void AfterInitialize()
        {
            //mono.canvasGroup.alpha = 0;
            //_m_tweenContainer = new TweenContainer();


            if (mono.monoContainer != null)
                _m_infoContainer = new UIPanelTBSInfoContainer(mono.monoContainer);

            if (mono.monoCharacterActionContainer != null)
                _m_characterActionContainer = new UIPanelTBSCharacterActionContainer(mono.monoCharacterActionContainer);
        }
        public override void OnHidePanel()
        {
            if(_m_infoContainer != null)
                _m_infoContainer?.HidePanel();

            if (_m_characterActionContainer != null)
                _m_characterActionContainer.HidePanel();

            //Tween tween = mono.canvasGroup.DOFade(0, mono.fadeOutDuration).OnStart(() =>
            //{
            //    mono.canvasGroup.alpha = 1;
            //});
            //_m_tweenContainer?.RegDoTween(tween);
        }

        public override void OnShowPanel()
        {
            //Tween tween = mono.canvasGroup.DOFade(1, mono.fadeInDuration).OnStart(() =>
            //{
            //    mono.canvasGroup.alpha = 0;
            //});
            //_m_tweenContainer?.RegDoTween(tween);

            if(_m_allActorInfoList == null)
                _m_allActorInfoList = new List<TBSActorInfo>();
            _m_allActorInfoList.Clear();

            List<TBSActorInfo> playerActorInfoList = SCModel.instance.tbsModel.battleInfo.playerTeamInfo.actorInfoList;
            List<TBSActorInfo> enemyActorInfoList = SCModel.instance.tbsModel.battleInfo.enemyTeamInfo.actorInfoList;
            for(int i =0;i<playerActorInfoList.Count;i++)
            {
                _m_allActorInfoList.Add(playerActorInfoList[i]);
            }
            for (int i = 0; i < enemyActorInfoList.Count; i++)
            {
                _m_allActorInfoList.Add(enemyActorInfoList[i]);
            }

            refreshPanel();
        }

        private void refreshPanel()
        {
            refreshInfoContainer();
            refreshCharacterActionContainer();
        }

        private void refreshInfoContainer()
        {
            List<TBSActorInfo> actorInfoList = SCModel.instance.tbsModel.battleInfo.playerTeamInfo.actorInfoList;
            if (actorInfoList == null || actorInfoList.Count == 0)
                return;
            _m_infoContainer.SetInfoList(actorInfoList);
            _m_infoContainer.ShowPanel();

        }

        private void refreshCharacterActionContainer()
        {
            List<TBSActorInfo> aliveActorInfoList = new List<TBSActorInfo>();
            for(int i =0;i<_m_allActorInfoList.Count;i++)
            {
                if (_m_allActorInfoList[i].hasDead)
                    continue;
                aliveActorInfoList.Add(_m_allActorInfoList[i]);
            }
            if (_m_allActorInfoList == null || _m_allActorInfoList.Count == 0)
                return;
            _m_characterActionContainer.SetInfoList(aliveActorInfoList);
            _m_characterActionContainer.ShowPanel();
        }

    }
}
