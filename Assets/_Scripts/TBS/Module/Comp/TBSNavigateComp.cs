using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    /// <summary>
    /// µ¼º½×é¼þ
    /// </summary>
    public class TBSNavigateComp : TBSCompBase
    {
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SWITCH_TO_LEFT_INPUT, onTBSSwitchToLeftInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SWITCH_TO_RIGHT_INPUT, onTBSSwitchToRightInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SWITCH_TO_DOWN_INPUT, onTBSSwitchToDownInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SWITCH_TO_UP_INPUT, onTBSSwitchToUpInput);

        }


        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SWITCH_TO_LEFT_INPUT, onTBSSwitchToLeftInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SWITCH_TO_RIGHT_INPUT, onTBSSwitchToRightInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SWITCH_TO_DOWN_INPUT, onTBSSwitchToDownInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SWITCH_TO_UP_INPUT, onTBSSwitchToUpInput);

        }

        public override void OnSuspend()
        {

        }

        public override void OnResume()
        {

        }

        private void onTBSSwitchToLeftInput()
        {
            _ASCUINodeBase mainNode = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSMain));
            _ASCUINodeBase confirmNode = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSConfirm));

            if ((mainNode == null || mainNode.hasHideNode) && (confirmNode == null || confirmNode.hasHideNode))
                return;
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_TARGET_HIGHLIGHT_LEFT);
        }

        private void onTBSSwitchToRightInput()
        {
            _ASCUINodeBase mainNode = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSMain));
            _ASCUINodeBase confirmNode = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSConfirm));

            if ((mainNode == null || mainNode.hasHideNode) && (confirmNode == null || confirmNode.hasHideNode))
                return;
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_TARGET_HIGHLIGHT_RIGHT);
        }

        private void onTBSSwitchToUpInput()
        {
            _ASCUINodeBase topNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode();
            if (topNode == null || topNode.hasHideNode)
                return;
            switch (topNode.GetNodeName())
            {
                case nameof(UINodeTBSSkill):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP);
                    break;
                case nameof(UINodeTBSItem):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ITEM_HIGHTLIGHT_UP);
                    break;
                default:
                    break;
            }
        }

        private void onTBSSwitchToDownInput()
        {


            _ASCUINodeBase topNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode();
            if (topNode == null || topNode.hasHideNode)
                return;
            switch (topNode.GetNodeName())
            {
                case nameof(UINodeTBSSkill):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN);
                    break;
                case nameof(UINodeTBSItem):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ITEM_HIGHTLIGHT_DOWN);
                    break;
                default:
                    break;
            }
        }
    }
}
