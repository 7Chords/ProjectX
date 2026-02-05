using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCore.UI
{
    public class UIPanelTBSWin : _ASCUIPanelBase<UIMonoTBSWin>
    {
        private UIPanelTBSWinExpContainer _m_winExpContainer;
        private List<bool> _m_hasLevelUpStateList;
        private int _m_money;
        public UIPanelTBSWin(UIMonoTBSWin _mono, SCUIShowType _showType) : base(_mono, _showType)
        {

        }

        public override void AfterInitialize()
        {
            _m_winExpContainer = new UIPanelTBSWinExpContainer(mono.monoExpContainer,SCUIShowType.INTERNAL);
        }

        public override void BeforeDiscard()
        {
            _m_winExpContainer?.Discard();
        }

        public override void OnHidePanel()
        {
            mono.btnExit.RemoveClickDown(onBtnExitClickDown);
            _m_winExpContainer?.HidePanel();

        }

        public override void OnShowPanel()
        {
            _m_winExpContainer?.ShowPanel();
            mono.btnExit.AddMouseLeftClickDown(onBtnExitClickDown);
        }
        private void onBtnExitClickDown(PointerEventData _data, object[] _objs)
        {
            GameCoreMgr.instance.uiCoreMgr.RemoveAllNodes(SCUINodeFuncType.TBS);
            TBSCursorMgr.instance.HideSelectionCursor();
            TBSGameStarter.instance.UnloadTBSGame(true);
        }


        public void SetInfo(List<bool> _hasLevelUpStateList,int _money)
        {
            _m_hasLevelUpStateList = _hasLevelUpStateList;
            _m_money = _money;
            refreshShow();
        }
        private void refreshShow()
        {
            if (_m_hasLevelUpStateList == null)
                return;
            _m_winExpContainer?.SetListInfo(SCModel.instance.tbsModel.GetActorInfoList(true), _m_hasLevelUpStateList);
            mono.txtMoney.text = LanguageHelper.instance.GetTextTranslate("#2_get_money", _m_money.ToString());
        }
    }
}
