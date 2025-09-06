using SCFrame;

namespace GameCore.TBS
{
    public class TBSTurnMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.TURN;

        private ETBSTurnType _m_curTurnType;
        private int _m_curTurnCount;
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_TURN_MGR_WORK,onTBSTurnWork);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_TURN_CHG, onTBSTurnChg);
        }
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_TURN_MGR_WORK, onTBSTurnWork);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_TURN_CHG, onTBSTurnChg);
        }
        public override void OnResume() { }

        public override void OnSuspend() { }


        #region 事件回调
        private void onTBSTurnWork()
        {
            _m_curTurnType = SCModel.instance.tbsModel.curTurnType;
            _m_curTurnCount = SCModel.instance.tbsModel.curTurnCount;
        }

        private void onTBSTurnChg()
        {
            _m_curTurnType = _m_curTurnType == ETBSTurnType.PLAYER 
                ? ETBSTurnType.ENEMY : ETBSTurnType.PLAYER;
        }
        #endregion
    }


}
