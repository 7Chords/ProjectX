using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSDefendComp : TBSCompBase
    {

        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_DEFEND_INPUT, onTBSDefendInput);
        }

        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_DEFEND_INPUT, onTBSDefendInput);
        }

        public override void OnResume()
        {
            
        }

        public override void OnSuspend()
        {

            
        }
        private void onTBSDefendInput()
        {
            _ASCUINodeBase node = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSMain));
            if (node == null || node.hasHideNode)
                return;
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_DEFENCE);
        }
    }

}