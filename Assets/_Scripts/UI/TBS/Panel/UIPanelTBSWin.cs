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
            GameCoreMgr.instance.uiCoreMgr.CloseTopNode();
            GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSEnemyHud));
            GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSPlayerHud));
            GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSMain));
            GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSInfo));
            TBSGameStarter.instance.UnloadTBSGame();

        }
    }
}
