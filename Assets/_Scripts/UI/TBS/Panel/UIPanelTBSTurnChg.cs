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
        public UIPanelTBSTurnChg(UIMonoTBSTurnChg _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }


        private TweenContainer _m_tweenContainer;

        public override void OnInitialize()
        {
            _m_tweenContainer = new TweenContainer();
            mono.canvasGroup.alpha = 0;
        }
        public override void OnDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }
        public override void OnShowPanel()
        {
            Tween tween = mono.canvasGroup.DOFade(1, mono.fadeInDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 0;
            });

            Tween tween_2 = DOVirtual.DelayedCall(mono.showDuration, () =>
            {
                GameCoreMgr.instance.uiCoreMgr.CloseTopNode();
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_TURN_CHG_SHOW_END);
            });
            _m_tweenContainer?.RegDoTween(tween);
            _m_tweenContainer?.RegDoTween(tween_2);


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
