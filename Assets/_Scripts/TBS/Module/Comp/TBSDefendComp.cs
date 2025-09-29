using SCFrame;
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
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_DEFENCE);
        }
    }

}