using GameCore.RefData;
using SCFrame;
using System.Collections.Generic;
using UnityEngine;
using Radnom = UnityEngine.Random;


namespace GameCore.TBS
{
    /// <summary>
    /// 全局唯一 兼具数据持久化功能
    /// </summary>
    public class TBSModel
    {
        private bool _m_gameStarted;//游戏是否开始
        public bool gameStarted
        {
            get { return _m_gameStarted; }
            set
            {
                _m_gameStarted = value;
            }
        }


        private ETBSTurnType _m_curTurnType;//当前的回合类型(玩家/敌人)

        public ETBSTurnType curTurnType
        {
            get { return _m_curTurnType; }
            set 
            { 
                _m_curTurnType = value;
                SCMsgCenter.SendMsg(SCMsgConst.TBS_TURN_CHG);
            }
        }

        private int _m_curTurnCount;//当前的回合数

        public int curTurnCount
        {
            get { return _m_curTurnCount; }
            set { _m_curTurnCount = value; }
        }

        private TBSBattleInfo _m_battleInfo;//战斗信息

        public TBSBattleInfo battleInfo
        {
            get { return _m_battleInfo; }
            set { _m_battleInfo = value; }
        }

        private int _m_curActorIndex;//当前行动的Actor索引
        public int curActorIndex
        {
            get { return _m_curActorIndex; }
            set 
            { 
                _m_curActorIndex = value;
                SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_CHG);
            }
        }

        private int _m_curSelectSingleTargetIdx;//当前单体选择目标的索引

        public int curSelectSingleTargetIdx
        {
            get { return _m_curSelectSingleTargetIdx; }
            set
            {
                _m_curSelectSingleTargetIdx = value;
                SCMsgCenter.SendMsg(SCMsgConst.TBS_SELECT_SINGLE_ENEMY_TARGET_CHG);
            }
        }

        private ETargetType _m_selectTargetType;//当前的选择目标类型（单体/全体）
        public ETargetType selectTargetType
        {
            get { return _m_selectTargetType; }
            set
            {
                _m_selectTargetType = value;
                SCMsgCenter.SendMsg(SCMsgConst.TBS_SELECT_ENEMY_ALL_OR_SINGLE_STATE_SWITCH);
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

        private List<GameObject> _m_enemyActorGOList;

        public List<GameObject> enemyActorGOList
        {
            get { return _m_enemyActorGOList; }
            set
            {
                _m_enemyActorGOList = value;
            }
        }

        private List<GameObject> _m_playerActorGOList;

        public List<GameObject> playerActorGOList
        {
            get { return _m_playerActorGOList; }
            set
            {
                _m_playerActorGOList = value;
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

        private long _m_canUseRunningId;


        private int _m_curSelectSkillIdx;

        public int curSelectSkillIdx
        {
            get { return _m_curSelectSkillIdx; }
            set
            {
                _m_curSelectSkillIdx = value;
            }
        }

        private int _m_curSelectItemIdx;

        public int curSelectItemIdx
        {
            get { return _m_curSelectItemIdx; }
            set
            {
                _m_curSelectItemIdx = value;
            }
        }

        /// <summary>
        /// 创新新游戏的时候初始化新的数据
        /// </summary>
        public void InitNewData()
        {
            _m_curTurnType = ETBSTurnType.PLAYER;
            _m_curTurnCount = 1;
            _m_battleInfo = new TBSBattleInfo();
            _m_battleInfo.InitNewInfo();
            _m_curActorIndex = 0;
            _m_curSelectSingleTargetIdx = 0;
            _m_selectTargetType = battleInfo.playerTeamInfo.actorInfoList[0].attackTargetType;
            _m_playerActorModuleList = new List<TBSActorBase>();
            _m_enemyActorModuleList = new List<TBSActorBase>();
            _m_enemyActorGOList = new List<GameObject>();
            _m_playerActorGOList = new List<GameObject>();
            _m_canUseRunningId = 0;
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
            _m_curTurnType = ETBSTurnType.PLAYER;
            _m_curTurnCount = 0;
            _m_battleInfo = null;
            _m_canUseRunningId = 0;

        }


        /// <summary>
        /// 获得当前行动的角色信息
        /// </summary>
        /// <returns></returns>
        public TBSActorInfo GetCurActorInfo()
        {
            if (curTurnType == ETBSTurnType.PLAYER)
                return playerActorModuleList[curActorIndex].actorInfo;
            else
                return enemyActorModuleList[curActorIndex].actorInfo;
        }

        /// <summary>
        /// 获得当前的单个选择目标的Actor
        /// </summary>
        /// <returns></returns>
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
                foreach(var actor in playerActorModuleList)
                {
                    if (!actor.actorInfo.hasDead)
                        return false;
                }
                return true;
            }
            else
            {
                foreach (var actor in enemyActorModuleList)
                {
                    if (!actor.actorInfo.hasDead)
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

        public List<TBSActorInfo> GetAllActorInfo()
        {
            List<TBSActorInfo> resInfoList = new List<TBSActorInfo>();
            for (int i = 0; i < playerActorModuleList.Count; i++)
            {
                resInfoList.Add(playerActorModuleList[i].actorInfo);
            }
            for (int i = 0; i < enemyActorModuleList.Count; i++)
            {
                resInfoList.Add(enemyActorModuleList[i].actorInfo);
            }
            return resInfoList;
        }

        /// <summary>
        /// 根据运行id获得对应的actor
        /// </summary>
        /// <param name="_runningId"></param>
        /// <returns></returns>
        public TBSActorBase GetActorByRunningId(long _runningId)
        {
            foreach(var actor in playerActorModuleList)
            {
                if (actor.actorInfo.runningId == _runningId)
                    return actor;
            }
            foreach(var actor in enemyActorModuleList)
            {
                if (actor.actorInfo.runningId == _runningId)
                    return actor;
            }
            return null;
        }

        /// <summary>
        /// 取走可分配的运行时ActorId
        /// </summary>
        /// <returns></returns>
        public long TakeRunningId()
        {
            _m_canUseRunningId++;
            return _m_canUseRunningId;
        }

        /// <summary>
        /// 获得某个Actor的GO索引
        /// </summary>
        /// <param name="_go"></param>
        /// <param name="_isPlayerActorGO"></param>
        /// <returns></returns>
        public int GetActorGOIndex(GameObject _go, bool _isPlayerActorGO)
        {
            if (_go == null)
            {
                SCDebugHelper.LogError("传入参数为空！！！");
                return -1;
            }
            if(_isPlayerActorGO)
            {
                for(int i =0;i<_m_playerActorGOList.Count;i++)
                {
                    if (_m_playerActorGOList[i] == _go)
                        return i;
                }    
            }
            else
            {
                for (int i = 0; i < _m_enemyActorGOList.Count; i++)
                {
                    if (_m_enemyActorGOList[i] == _go)
                        return i;
                }
            }
            SCDebugHelper.LogError("找不到这个物体所在的索引！！！");
            return -1;
        }

        /// <summary>
        /// 获得当前选择的技能的refObj
        /// </summary>
        /// <returns></returns>
        public TBSActorSkillRefObj GetCurSkillRefObj()
        {
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
            if (actorInfo == null)
                return null;
            long curSkillId = actorInfo.skillList[SCModel.instance.tbsModel.curSelectSkillIdx];
            TBSActorSkillRefObj refObj = SCRefDataMgr.instance.tbsActorSkillRefList.refDataList.Find(x => x.id == curSkillId);
            if (refObj == null)
            {
                SCDebugHelper.LogError("找不到id为" + curSkillId + "的技能配表数据！！！");
                return null;
            }
            return refObj;
        }

        /// <summary>
        /// 获得当前选择的道具的refObj
        /// </summary>
        /// <returns></returns>
        public ItemRefObj GetCurItemRefObj()
        {
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
            if (actorInfo == null)
                return null;
            long curItemId = SCDataMgr.instance.itemDataList[SCModel.instance.tbsModel.curSelectItemIdx].itemId;
            ItemRefObj refObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == curItemId);
            if(refObj == null)
            {
                SCDebugHelper.LogError("找不到id为" + curItemId + "的道具配表数据！！！");
                return null;
            }
            return refObj;
        }
    }
}
