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
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_TARGET_HIGHLIGHT_LEFT);
        }

        private void onTBSSwitchToRightInput()
        {
            _ASCUINodeBase mainNode = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSMain));
            _ASCUINodeBase confirmNode = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSConfirm));

            if ((mainNode == null || mainNode.hasHideNode) && (confirmNode == null || confirmNode.hasHideNode))
                return;
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_TARGET_HIGHLIGHT_RIGHT);
        }

        private void onTBSSwitchToUpInput()
        {
            _ASCUINodeBase node = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSSkill));
            if (node == null || node.hasHideNode)
                return;
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP);
        }

        private void onTBSSwitchToDownInput()
        {
            _ASCUINodeBase node = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSSkill));
            if (node == null || node.hasHideNode)
                return;
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN);
        }
    }
}
