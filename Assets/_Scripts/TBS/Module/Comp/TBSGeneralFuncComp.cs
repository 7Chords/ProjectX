using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSGeneralFuncComp : TBSCompBase
    {
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.ESC_INPUT, onESCInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.MOUSE_RIGHT_INPUT, onMouseRightInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_CONFIRM_INPUT, onTBSConfirmInput);

        }

        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.ESC_INPUT, onESCInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.MOUSE_RIGHT_INPUT, onMouseRightInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_CONFIRM_INPUT, onTBSConfirmInput);

        }

        public override void OnResume()
        {

        }

        public override void OnSuspend()
        {


        }

        private void onESCInput()
        {
            _ASCUINodeBase topNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
            if (topNode == null || topNode.GetNodeName() == nameof(UINodeTBSInfo))
                return;

            GameCoreMgr.instance.uiCoreMgr.CloseNodeByEsc();
        }
        private void onMouseRightInput()
        {
            _ASCUINodeBase topNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
            if (topNode == null || topNode.GetNodeName() == nameof(UINodeTBSInfo))
                return;

            GameCoreMgr.instance.uiCoreMgr.CloseNodeByMouseRight();
        }

        private void onTBSConfirmInput()
        {
            _ASCUINodeBase topNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
            if (topNode == null || topNode.hasHideNode)
                return;
            switch (topNode.GetNodeName())
            {
                case nameof(UINodeTBSSkill):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL_CONFIRM);
                    break;
                case nameof(UINodeTBSItem):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ITEM_CONFIRM);
                    break;
                case nameof(UINodeTBSConfirm):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_CONFIRM_RELEASE);
                    break;
                default:
                    break;

            }
        }

    }
}
