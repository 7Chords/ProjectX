using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;

namespace GameCore.TBS
{
    public class TBSCoreMgr : ACoreMgrBase
    {
        public override ECoreMgrType coreMgrType => ECoreMgrType.TBS;
        private Dictionary<ETBSSubMgrType, TBSSubMgrBase> _m_subMgrDict;//子管理器字典

        private bool _m_tbsGameHasStarted;
        public bool tbsGameHasStarted => _m_tbsGameHasStarted;
        public override void OnInitialize()
        {
            //注册事件
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_GAME_START,onTBSGameStart);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_GAME_FINISH, onTBSGameFinish);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ALL_PLAYER_ACTOR_DIE, onTBSAllPlayerActorDie);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ALL_ENEMY_ACTOR_DIE, onTBSAllEnemyActorDie);

            initAllSubMgr();
        }
        public override void OnDiscard()
        {
            //反注册事件
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_GAME_START, onTBSGameStart);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_GAME_FINISH, onTBSGameFinish);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ALL_PLAYER_ACTOR_DIE, onTBSAllPlayerActorDie);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ALL_ENEMY_ACTOR_DIE, onTBSAllEnemyActorDie);


            discardAllSubMgr();
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
            _m_subMgrDict = new Dictionary<ETBSSubMgrType, TBSSubMgrBase>();
            //回合轮转管理器
            TBSTurnMgr turnMgr = new TBSTurnMgr();
            turnMgr.Initialize();
            //回合效果管理器
            TBSEventMgr effectMgr = new TBSEventMgr();
            effectMgr.Initialize();
            //单位管理器
            TBSActorMgr actorMgr = new TBSActorMgr();
            actorMgr.Initialize();
            //功能组件管理器
            TBSCompMgr compMgr = new TBSCompMgr();
            compMgr.Initialize();

            _m_subMgrDict.Add(ETBSSubMgrType.TURN,turnMgr);
            _m_subMgrDict.Add(ETBSSubMgrType.EFFECT, effectMgr);
            _m_subMgrDict.Add(ETBSSubMgrType.ACTOR, actorMgr);
            _m_subMgrDict.Add(ETBSSubMgrType.COMP, compMgr);
        }

        //销毁所有的子管理器
        private void discardAllSubMgr()
        {
            foreach(var mgr in _m_subMgrDict.Values)
            {
                if (mgr == null)
                    continue;
                mgr.Discard();
            }
            _m_subMgrDict.Clear();
            _m_subMgrDict = null;
        }

        //恢复所有的子管理器
        private void resumeAllSubMgr()
        {
            foreach (var mgr in _m_subMgrDict.Values)
            {
                if (mgr == null)
                    continue;
                mgr.Resume();
            }
        }

        //挂起所有的子管理器
        private void suspendAllSubMgr()
        {
            foreach (var mgr in _m_subMgrDict.Values)
            {
                if (mgr == null)
                    continue;
                mgr.Suspend();
            }
        }

        #region 事件回调
        private void onTBSGameStart()
        {
            //隐藏大世界玩家
            SCCommon.SetGameObjectEnable(SCGame.instance.playerGO, false);

            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_TURN_MGR_WORK);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_MGR_WORK);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_EFFECT_MGR_WORK);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_COMP_MGR_WORK);


            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSInfo(SCUIShowType.FULL));
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSMain(SCUIShowType.FULL),true);
            _m_tbsGameHasStarted = true;
            SCModel.instance.tbsModel.gameStarted = true;
        }

        private void onTBSGameFinish()
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_TURN_MGR_REST);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_MGR_REST);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_EFFECT_MGR_REST);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_COMP_MGR_REST);

            //显示大世界玩家
            SCCommon.SetGameObjectEnable(SCGame.instance.playerGO, false);

            _m_tbsGameHasStarted = false;
        }

        private void onTBSAllPlayerActorDie()
        {
            _m_tbsGameHasStarted = false;
            SCModel.instance.tbsModel.gameStarted = false;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSLose(SCUIShowType.ADDITION));
        }

        private void onTBSAllEnemyActorDie()
        {
            _m_tbsGameHasStarted = false;
            SCModel.instance.tbsModel.gameStarted = false;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSWin(SCUIShowType.ADDITION));
        }
        #endregion
    }
}
