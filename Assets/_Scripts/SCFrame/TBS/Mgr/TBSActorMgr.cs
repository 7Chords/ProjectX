using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.TBS
{
    public class TBSActorMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.ACTOR;

        private TBSTeamInfo _m_playerTeamInfo;
        private TBSTeamInfo _m_enemyTeamInfo;

        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
        }
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
        }

        public override void OnResume() { }

        public override void OnSuspend() { }

        #region 事件回调

        private void onTBSActorMgrWork(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0) return;
            _m_playerTeamInfo = _objs[0] as TBSTeamInfo;
            _m_enemyTeamInfo = _objs[1] as TBSTeamInfo;
        }
        #endregion
    }
}
