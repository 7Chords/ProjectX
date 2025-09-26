using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSCompMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.COMP;

        private Dictionary<ETBSCompType, TBSCompBase> _m_tbsCompDict;
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_DEFEND_INPUT, onTBSDefendInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ATTACK_INPUT, onTBSAttackInput);
        }

        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_DEFEND_INPUT, onTBSDefendInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ATTACK_INPUT, onTBSAttackInput);
        }

        public override void OnResume() { }

        public override void OnSuspend() { }


        private void onTBSDefendInput()
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_DEFENCE);
        }

        private void onTBSAttackInput()
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_ATTACK);
        }
    }
}
