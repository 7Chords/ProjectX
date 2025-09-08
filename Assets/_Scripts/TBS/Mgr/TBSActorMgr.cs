using SCFrame;

namespace GameCore.TBS
{
    public class TBSActorMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.ACTOR;

        private TBSTeamInfo _m_playerTeamInfo;
        private TBSTeamInfo _m_enemyTeamInfo;

        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
        }
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
        }

        public override void OnResume() { }

        public override void OnSuspend() { }

        #region 事件回调

        private void onTBSActorMgrWork()
        {
            _m_playerTeamInfo = SCModel.instance.tbsModel.battleInfo?.playerTeamInfo;
            _m_enemyTeamInfo = SCModel.instance.tbsModel.battleInfo?.enemyTeamInfo;
        }
        #endregion
    }
}
