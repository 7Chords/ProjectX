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
        }
        public override void OnDiscard()
        {
            //反注册事件
            SCMsgCenter.UnregisterMsg(SCMsgConst.GAME_START, onGameStart);
            SCMsgCenter.UnregisterMsg(SCMsgConst.GAME_END, onGameEnd);
        }

        public override void OnResume()
        {
        }

        public override void OnSuspend()
        {
        }


        //初始化所有的子管理器
        private void initAllCoreMgr()
        {
        }

        //销毁所有的子管理器
        private void discardAllCoreMgr()
        {
        }

        //恢复所有的子管理器
        private void resumeAllCoreMgr()
        {
        }

        //挂起所有的子管理器
        private void suspendAllCoreMgr()
        {

        }

        private void onGameStart(object[] _objs)
        {
            _m_coreMgrList = new List<ACoreMgrBase>();
            initAllCoreMgr();
        }

        private void onGameEnd(object[] _objs)
        {
            discardAllCoreMgr();
            _m_coreMgrList.Clear();
            _m_coreMgrList = null;
        }
    }
}
