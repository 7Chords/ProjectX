using DG.Tweening;
using GameCore.TBS;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSTurnChg : _ASCUIPanelBase<UIMonoTBSTurnChg>
    {
        private TweenContainer _m_tweenContainer;
        public UIPanelTBSTurnChg(UIMonoTBSTurnChg _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            _m_tweenContainer = new TweenContainer();
        }
        public override void BeforeDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }


        public override void OnShowPanel()
        {

            Tween tween = DOVirtual.DelayedCall(mono.showDuration, () =>
            {
                GameCoreMgr.instance.uiCoreMgr.CloseTopNode();
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_TURN_CHG_SHOW_END);
            });
            _m_tweenContainer?.RegDoTween(tween);


            refreshPanelShow();
        }
        public override void OnHidePanel()
        {
            Tween tween = mono.canvasGroup.DOFade(0, mono.fadeOutDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 1;
            });
            _m_tweenContainer?.RegDoTween(tween);
        }

        private void refreshPanelShow()
        {
            ETBSTurnType turnType =  SCModel.instance.tbsModel.curTurnType;
            mono.txtTurnChg.text = LanguageHelper.instance.GetTextTranslate(
                turnType == ETBSTurnType.PLAYER
                ? "#1_tbs_player_turn"
                : "#1_tbs_enemy_turn");
        }
    }
}
