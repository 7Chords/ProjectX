using SCFrame.TBS;
using System.Collections.Generic;


namespace SCFrame
{
    public class GameCoreMgr : _ASCLifeObjBase
    {
        private List<ACoreMgrBase> _m_coreMgrList;//核心管理器列表

        public override void OnInitialize()
        {
            //注册事件
            SCMsgCenter.RegisterMsg(SCMsgConst.GAME_START, onGameStart);
            SCMsgCenter.RegisterMsg(SCMsgConst.GAME_END, onGameEnd);

            _m_coreMgrList = new List<ACoreMgrBase>();
            initAllCoreMgr();
        }
        public override void OnDiscard()
        {
            //反注册事件
            SCMsgCenter.UnregisterMsg(SCMsgConst.GAME_START, onGameStart);
            SCMsgCenter.UnregisterMsg(SCMsgConst.GAME_END, onGameEnd);

            discardAllCoreMgr();
            _m_coreMgrList.Clear();
            _m_coreMgrList = null;
        }

        public override void OnResume()
        {
            resumeAllCoreMgr();
        }

        public override void OnSuspend()
        {
            suspendAllCoreMgr();
        }


        //初始化所有的子管理器
        private void initAllCoreMgr()
        {
            TBSCoreMgr tbsCoreMgr = new TBSCoreMgr();
            tbsCoreMgr.Initialize();
            _m_coreMgrList.Add(tbsCoreMgr);

            
        }

        //销毁所有的子管理器
        private void discardAllCoreMgr()
        {
            foreach(var mgr in _m_coreMgrList)
            {
                if (mgr == null)
                    return;
                mgr.Discard();
            }
        }

        //恢复所有的子管理器
        private void resumeAllCoreMgr()
        {
            foreach (var mgr in _m_coreMgrList)
            {
                if (mgr == null)
                    return;
                mgr.Resume();
            }
        }

        //挂起所有的子管理器
        private void suspendAllCoreMgr()
        {
            foreach (var mgr in _m_coreMgrList)
            {
                if (mgr == null)
                    return;
                mgr.Suspend();
            }
        }

        private void onGameStart(object[] _objs)
        {

        }

        private void onGameEnd(object[] _objs)
        {

        }
    }
}
