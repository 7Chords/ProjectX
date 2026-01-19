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
            GameCoreMgr.instance.uiCoreMgr.CloseTopNode();
            //todo
            SCModel.instance.tbsModel.InitNewData();
            GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSEnemyHud));
            GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSPlayerHud));
            GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSMain));
            GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSInfo));

            SCMsgCenter.SendMsg(SCMsgConst.TBS_GAME_FINISH);
            SCMsgCenter.SendMsg(SCMsgConst.TBS_GAME_START);
        }
        private void onBtnExitClickDown(PointerEventData _data, object[] _objs)
        {
            GameCoreMgr.instance.uiCoreMgr.CloseTopNode();
            //todo
            SCModel.instance.tbsModel.InitNewData();
            SCMsgCenter.SendMsg(SCMsgConst.TBS_GAME_FINISH);
        }
    }
}
