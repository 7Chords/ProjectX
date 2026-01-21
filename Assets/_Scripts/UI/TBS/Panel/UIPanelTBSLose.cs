using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCore.UI
{
    public class UIPanelTBSLose : _ASCUIPanelBase<UIMonoTBSLose>
    {
        public UIPanelTBSLose(UIMonoTBSLose _mono, SCUIShowType _showType) : base(_mono, _showType)
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
            mono.btnRetry.RemoveClickDown(onBtnRetryClickDown);
            mono.btnExit.RemoveClickDown(onBtnExitClickDown);
        }

        public override void OnShowPanel()
        {
            mono.btnRetry.AddMouseLeftClickDown(onBtnRetryClickDown);
            mono.btnExit.AddMouseLeftClickDown(onBtnExitClickDown);
        }

        private void onBtnRetryClickDown(PointerEventData _data, object[] _objs)
        {
            GameCoreMgr.instance.uiCoreMgr.RemoveAllNodes(SCUINodeFuncType.TBS);
            TBSGameStarter.instance.ReloadTBSGame();
        }
        private void onBtnExitClickDown(PointerEventData _data, object[] _objs)
        {
            GameCoreMgr.instance.uiCoreMgr.RemoveAllNodes(SCUINodeFuncType.TBS);
            TBSGameStarter.instance.UnloadTBSGame();
            SCInputListener.instance.SetCanInput(true);
        }
    }
}
