using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.TBS
{
    public class TBSTurnMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.TURN;

        private ETBSTurnType _m_curTurnType;
        private int _m_curTurnCount;
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_TURN_MGR_WORK,onTBSTurnWork);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_TURN_CHG, onTBSTurnChg);
        }
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_TURN_MGR_WORK, onTBSTurnWork);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_TURN_CHG, onTBSTurnChg);
        }
        public override void OnResume() { }

        public override void OnSuspend() { }


        #region 事件回调
        private void onTBSTurnWork(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0) return;
            _m_curTurnType = (ETBSTurnType)_objs[0];
            _m_curTurnCount = 1;
            SCModel.instance.tbsModel.curTurnType = _m_curTurnType;
            SCModel.instance.tbsModel.curTurnCount = _m_curTurnCount;
        }

        private void onTBSTurnChg()
        {
            _m_curTurnType = _m_curTurnType == ETBSTurnType.PLAYER 
                ? ETBSTurnType.ENEMY : ETBSTurnType.PLAYER;
        }
        #endregion
    }


}
