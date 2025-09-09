using GameCore.TBS;
using SCFrame.UI;


namespace SCFrame
{
    /// <summary>
    /// 游戏核心管理器
    /// </summary>
    public class GameCoreMgr : Singleton<GameCoreMgr>
    {
        public TBSCoreMgr tbsCoreMgr;
        public UICoreMgr uiCoreMgr;
        public override void OnInitialize()
        {
            //注册事件
            SCMsgCenter.RegisterMsg(SCMsgConst.GAME_START, onGameStart);
            SCMsgCenter.RegisterMsg(SCMsgConst.GAME_END, onGameEnd);

            initAllCoreMgr();
        }
        public override void OnDiscard()
        {
            //反注册事件
            SCMsgCenter.UnregisterMsg(SCMsgConst.GAME_START, onGameStart);
            SCMsgCenter.UnregisterMsg(SCMsgConst.GAME_END, onGameEnd);

            discardAllCoreMgr();
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
            tbsCoreMgr = new TBSCoreMgr();
            tbsCoreMgr.Initialize();
            uiCoreMgr = new UICoreMgr();
            uiCoreMgr.Initialize();


        }

        //销毁所有的子管理器
        private void discardAllCoreMgr()
        {
            if (tbsCoreMgr != null)
                tbsCoreMgr.Discard();
        }

        //恢复所有的子管理器
        private void resumeAllCoreMgr()
        {
            if (tbsCoreMgr != null)
                tbsCoreMgr.Resume();
        }

        //挂起所有的子管理器
        private void suspendAllCoreMgr()
        {
            if (tbsCoreMgr != null)
                tbsCoreMgr.Suspend();
        }

        private void onGameStart(object[] _objs)
        {

        }

        private void onGameEnd(object[] _objs)
        {

        }
    }
}
