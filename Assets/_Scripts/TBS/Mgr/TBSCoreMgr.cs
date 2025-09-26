using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;

namespace GameCore.TBS
{
    public class TBSCoreMgr : ACoreMgrBase
    {
        public override ECoreMgrType coreMgrType => ECoreMgrType.TBS;

        //private List<TBSSubMgrBase> _m_subMgrList;//子管理器列表
        private Dictionary<ETBSSubMgrType, TBSSubMgrBase> _m_subMgrDict;//子管理器字典
        public override void OnInitialize()
        {
            //注册事件
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_GAME_START,onTBSGameStart);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_GAME_FINISH, onTBSGameFinish);

            _m_subMgrDict = new Dictionary<ETBSSubMgrType, TBSSubMgrBase>();

            initAllSubMgr();
        }
        public override void OnDiscard()
        {
            //反注册事件
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_GAME_START, onTBSGameStart);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_GAME_FINISH, onTBSGameFinish);


            discardAllSubMgr();

            _m_subMgrDict.Clear();
            _m_subMgrDict = null;
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

            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSMain(SCUIShowType.FULL));
        }

        private void onTBSGameFinish()
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_TURN_MGR_REST);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_MGR_REST);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_EFFECT_MGR_REST);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_COMP_MGR_REST);

            //显示大世界玩家
            SCCommon.SetGameObjectEnable(SCGame.instance.playerGO, false);
        }
        #endregion

        
        /// <summary>
        /// 获得当前的回合持有方
        /// </summary>
        /// <returns></returns>
        public ETBSTurnType getCurTurnType()
        {
            return (_m_subMgrDict[ETBSSubMgrType.TURN] as TBSTurnMgr).curTurnType;
        }

        /// <summary>
        /// 获得当前的行动角色
        /// </summary>
        /// <returns></returns>
        public TBSActorBase getCurActor()
        {
            return (_m_subMgrDict[ETBSSubMgrType.ACTOR] as TBSActorMgr).GetCurActor();
        }
    }
}
