using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.TBS
{
    public class TBSAttackComp : TBSCompBase
    {
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ATTACK_INPUT, onTBSAttackInput);
        }

        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ATTACK_INPUT, onTBSAttackInput);
        }

        public override void OnSuspend()
        {
            
        }

        public override void OnResume()
        {
            
        }

        private void onTBSAttackInput()
        {
            _ASCUINodeBase node = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSMain));
            if (node == null || node.hasHideNode)
                return;
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_ATTACK);
        }
    }
}
