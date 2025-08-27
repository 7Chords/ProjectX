using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.TBS
{
    public class TBSCoreMgr : ACoreMgrBase
    {
        public override ECoreMgrType coreMgrType => ECoreMgrType.TBS;

        private List<TBSSubMgrBase> _m_subMgrList;//子管理器列表

        public override void OnInitialize()
        {
            //注册事件
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_GAME_START,onTBSGameStart);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_GAME_FINISH, onTBSGameFinish);

            _m_subMgrList = new List<TBSSubMgrBase>();

            initAllSubMgr();
        }
        public override void OnDiscard()
        {
            //反注册事件
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_GAME_START, onTBSGameStart);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_GAME_FINISH, onTBSGameFinish);


            discardAllSubMgr();

            _m_subMgrList.Clear();
            _m_subMgrList = null;
        }
        public override void OnResume() 
        {
            resumeAllSubMgr();
        }

        public override void OnSuspend() 
        {
            suspendAllSubMgr();
        }

        //初始化所有的子管理器
        private void initAllSubMgr()
        {
            //回合轮转管理器
            TBSTurnMgr turnMgr = new TBSTurnMgr();
            turnMgr.Initialize();
            //回合效果管理器
            TBSEffectMgr effectMgr = new TBSEffectMgr();
            effectMgr.Initialize();
            //单位管理器
            TBSActorMgr actorMgr = new TBSActorMgr();
            actorMgr.Initialize();
            //功能组件管理器
            TBSCompMgr compMgr = new TBSCompMgr();
            compMgr.Initialize();

            _m_subMgrList.Add(turnMgr);
            _m_subMgrList.Add(effectMgr);
            _m_subMgrList.Add(actorMgr);
            _m_subMgrList.Add(compMgr);
        }

        //销毁所有的子管理器
        private void discardAllSubMgr()
        {
            foreach(var mgr in _m_subMgrList)
            {
                if (mgr == null)
                    continue;
                mgr.Discard();
            }
        }

        //恢复所有的子管理器
        private void resumeAllSubMgr()
        {
            foreach (var mgr in _m_subMgrList)
            {
                if (mgr == null)
                    continue;
                mgr.Resume();
            }
        }

        //挂起所有的子管理器
        private void suspendAllSubMgr()
        {
            foreach (var mgr in _m_subMgrList)
            {
                if (mgr == null)
                    continue;
                mgr.Suspend();
            }
        }


        private void onTBSGameStart(object[] _objs)
        {

        }

        private void onTBSGameFinish(object[] _objs)
        {

        }

    }
}
