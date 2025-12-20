using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSSkillComp : TBSCompBase
    {
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SKILL_INPUT, onTBSSkillInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_CONFIRM_INPUT, onTBSConfirmInput);

        }

        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SKILL_INPUT, onTBSSkillInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_CONFIRM_INPUT, onTBSConfirmInput);

        }

        private void onTBSSkillInput()
        {
            _ASCUINodeBase node = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSMain));
            if (node == null || node.hasHideNode)
                return;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSSkill(SCFrame.UI.SCUIShowType.FULL));
        }

        private void onTBSConfirmInput()
        {
            _ASCUINodeBase topNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
            if (topNode == null || topNode.hasHideNode)
                return;
            switch(topNode.GetNodeName())
            {
                case nameof(UINodeTBSSkill):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL_CONFIRM);
                    break;
                case nameof(UINodeTBSConfirm):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_CONFIRM_RELEASE);
                    break;
                case nameof(UINodeTBSItem):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ITEM_CONFIRM);
                    break;
                default:
                    break;

            }
        }
    }
}
