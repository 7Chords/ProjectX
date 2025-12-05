using SCFrame;
using System.Collections.Generic;
using Radnom = UnityEngine.Random;


namespace GameCore.TBS
{
    /// <summary>
    /// 全局唯一 兼具数据持久化功能
    /// </summary>
    public class TBSModel
    {
        private bool _m_gameStarted;

        public bool gameStarted
        {
            get { return _m_gameStarted; }
            set
            {
                _m_gameStarted = value;
            }
        }




        private ETBSTurnType _m_curTurnType;

        public ETBSTurnType curTurnType
        {
            get { return _m_curTurnType; }
            set 
            { 
                _m_curTurnType = value;
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_TURN_CHG);
            }
        }

        private int _m_curTurnCount;

        public int curTurnCount
        {
            get { return _m_curTurnCount; }
            set { _m_curTurnCount = value; }
        }

        private TBSBattleInfo _m_battleInfo;

        public TBSBattleInfo battleInfo
        {
            get { return _m_battleInfo; }
            set { _m_battleInfo = value; }
        }

        private int _m_curActorIndex;
        public int curActorIndex
        {
            get { return _m_curActorIndex; }
            set 
            { 
                _m_curActorIndex = value;
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_CHG);
            }
        }

        private int _m_curSelectSingleTargetIdx;

        public int curSelectSingleTargetIdx
        {
            get { return _m_curSelectSingleTargetIdx; }
            set
            {
                _m_curSelectSingleTargetIdx = value;
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SELECT_SINGLE_ENEMY_TARGET_CHG);
            }
        }

        private ETargetType _m_selectTargetType;
        public ETargetType selectTargetType
        {
            get { return _m_selectTargetType; }
            set
            {
                _m_selectTargetType = value;
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SELECT_ENEMY_ALL_OR_SINGLE_STATE_SWITCH);
            }
        }


        private List<TBSActorBase> _m_playerActorModuleList;
        
        public List<TBSActorBase> playerActorModuleList
        {
            get { return _m_playerActorModuleList; }
            set
            {
                _m_playerActorModuleList = value;
            }
        }

        private List<TBSActorBase> _m_enemyActorModuleList;

        public List<TBSActorBase> enemyActorModuleList
        {
            get { return _m_enemyActorModuleList; }
            set
            {
                _m_enemyActorModuleList = value;
            }
        }


        private TBSGameMono _m_gameMono;//回合制战斗全局mono

        public TBSGameMono gameMono
        {
            get { return _m_gameMono; }
            set
            {
                _m_gameMono = value;
            }
        }
        /// <summary>
        /// 创新新游戏的时候初始化新的数据
        /// </summary>
        public void InitNewData()
        {
            curTurnType = ETBSTurnType.PLAYER;
            curTurnCount = 1;
            battleInfo = new TBSBattleInfo();
            battleInfo.InitNewInfo();
            curActorIndex = 0;
            curSelectSingleTargetIdx = 0;
            selectTargetType = battleInfo.playerTeamInfo.actorInfoList[0].attackTargetType;
            playerActorModuleList = new List<TBSActorBase>();
            enemyActorModuleList = new List<TBSActorBase>();

        }


        /// <summary>
        /// 从存档中加载数据
        /// </summary>
        public void LoadData()
        {

        }

        /// <summary>
        /// 重制当前数据
        /// </summary>
        public void ResetData()
        {
            curTurnType = ETBSTurnType.PLAYER;
            //curTurnCount = 1;
            battleInfo = null;
        }


        /// <summary>
        /// 获得当前行动的角色信息
        /// </summary>
        /// <returns></returns>
        public TBSActorInfo GetCurActorInfo()
        {
            if (curTurnType == ETBSTurnType.PLAYER)
                return battleInfo.playerTeamInfo.actorInfoList[curActorIndex];
            else
                return battleInfo.enemyTeamInfo.actorInfoList[curActorIndex];
        }

        public TBSActorBase GetCurSingleSelectTargetActor()
        {
            if (enemyActorModuleList == null || _m_curSelectSingleTargetIdx < 0 || _m_curSelectSingleTargetIdx >= enemyActorModuleList.Count)
                return null;
            return enemyActorModuleList[_m_curSelectSingleTargetIdx];
        }

        public bool CheckAllActorsDead(bool _isPlayer)
        {
            if(_isPlayer)
            {
                foreach(var actor in battleInfo.playerTeamInfo.actorInfoList)
                {
                    if (!actor.hasDead)
                        return false;
                }
                return true;
            }
            else
            {
                foreach (var actor in battleInfo.enemyTeamInfo.actorInfoList)
                {
                    if (!actor.hasDead)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 取得一个随机的未死亡的actor
        /// </summary>
        /// <param name="_isPlayerActor"></param>
        /// <returns></returns>
        public TBSActorBase GetRandomAliveActor(bool _isPlayerActor)
        {
            if (!gameStarted)
                return null;
            if(_isPlayerActor)
            {
                if (playerActorModuleList == null)
                    return null;
                int randomIdx = Radnom.Range(0, playerActorModuleList.Count);

                while(playerActorModuleList[randomIdx].actorInfo.hasDead)
                {
                    randomIdx = Radnom.Range(0, playerActorModuleList.Count);
                }
                return playerActorModuleList[randomIdx];
            }
            else
            {
                if (enemyActorModuleList == null)
                    return null;

                int randomIdx = Radnom.Range(0, enemyActorModuleList.Count);

                while (enemyActorModuleList[randomIdx].actorInfo.hasDead)
                {
                    randomIdx = Radnom.Range(0, enemyActorModuleList.Count);
                }

                return enemyActorModuleList[randomIdx];
            }
        }
    }
}
