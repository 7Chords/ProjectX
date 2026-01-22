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
        public UIPanelTBSWin(UIMonoTBSWin _mono, SCUIShowType _showType) : base(_mono, _showType)
        {

        }

        public override void AfterInitialize()
        {
        }

        public override void BeforeDiscard()
        {
        }

        public override void OnHidePanel()
        {
            mono.btnExit.RemoveClickDown(onBtnExitClickDown);
        }

        public override void OnShowPanel()
        {
            mono.btnExit.AddMouseLeftClickDown(onBtnExitClickDown);
        }
        private void onBtnExitClickDown(PointerEventData _data, object[] _objs)
        {
            GameCoreMgr.instance.uiCoreMgr.RemoveAllNodes(SCUINodeFuncType.TBS);
            TBSCursorMgr.instance.HideSelectionCursor();
            TBSGameStarter.instance.UnloadTBSGame();

        }
    }
}
