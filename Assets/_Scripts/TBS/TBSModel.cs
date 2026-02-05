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

        private int _m_curSelectSingleEnemyTargetIdx;//当前单体选择敌人目标的索引

        public int curSelectSingleEnemyTargetIdx
        {
            get { return _m_curSelectSingleEnemyTargetIdx; }
            set
            {
                _m_curSelectSingleEnemyTargetIdx = value;
                SCMsgCenter.SendMsg(SCMsgConst.TBS_SELECT_SINGLE_ENEMY_TARGET_CHG);
            }
        }

        private int _m_curSelectSinglePlayerTargetIdx;//当前单体选择玩家角色目标的索引

        public int curSelectSinglePlayerTargetIdx
        {
            get { return _m_curSelectSinglePlayerTargetIdx; }
            set
            {
                _m_curSelectSinglePlayerTargetIdx = value;
                SCMsgCenter.SendMsg(SCMsgConst.TBS_SELECT_SINGLE_PLAYER_TARGET_CHG);
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

        private int _m_curKillEnemyExp;

        private int _m_curKillEnemyMoney;
        /// <summary>
        /// 创新新游戏的时候初始化新的数据
        /// </summary>
        public void Init(TBSBattleInfo _battleInfo)
        {
            if (_battleInfo == null)
                return;
            _m_curTurnType = ETBSTurnType.PLAYER;
            _m_curTurnCount = 1;
            _m_battleInfo = _battleInfo;

            _m_curActorIndex = 0;
            _m_curSelectSingleEnemyTargetIdx = 0;
            _m_curSelectSinglePlayerTargetIdx = 0;
            _m_selectTargetType = battleInfo.playerTeamInfo.actorInfoList[0].attackTargetType;
            _m_playerActorModuleList = new List<TBSActorBase>();
            _m_enemyActorModuleList = new List<TBSActorBase>();
            _m_enemyActorGOList = new List<GameObject>();
            _m_playerActorGOList = new List<GameObject>();
            _m_curSelectSkillIdx = 0;
            _m_curSelectItemIdx = 0;
            _m_canUseRunningId = 0;
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
            {
                if (curActorIndex < 0 || curActorIndex >= playerActorModuleList.Count)
                    return null;
                return playerActorModuleList[curActorIndex].actorInfo;
            }
            else
            {
                if (curActorIndex < 0 || curActorIndex >= enemyActorModuleList.Count)
                    return null;
                return enemyActorModuleList[curActorIndex].actorInfo;
            }
        }

        /// <summary>
        /// 获得当前行动的角色Actor
        /// </summary>
        /// <returns></returns>
        public TBSActorBase GetCurActor()
        {
            if (curTurnType == ETBSTurnType.PLAYER)
            {
                if (curActorIndex < 0 || curActorIndex >= playerActorModuleList.Count)
                    return null;
                return playerActorModuleList[curActorIndex];
            }
            else
            {
                if (curActorIndex < 0 || curActorIndex >= enemyActorModuleList.Count)
                    return null;
                return enemyActorModuleList[curActorIndex];
            }
        }

        /// <summary>
        /// 获得当前的单个选择目标的Actor
        /// </summary>
        /// <returns></returns>
        public TBSActorBase GetCurSelectSingleEnemyTargetActor()
        {
            if (enemyActorModuleList == null || _m_curSelectSingleEnemyTargetIdx < 0 || _m_curSelectSingleEnemyTargetIdx >= enemyActorModuleList.Count)
                return null;
            return enemyActorModuleList[_m_curSelectSingleEnemyTargetIdx];
        }


        /// <summary>
        /// 获得当前的单个选择目标的Actor
        /// </summary>
        /// <returns></returns>
        public TBSActorBase GetCurSelectSinglePlayerTargetActor()
        {
            if (playerActorModuleList == null || _m_curSelectSinglePlayerTargetIdx < 0 || _m_curSelectSinglePlayerTargetIdx >= playerActorModuleList.Count)
                return null;
            return playerActorModuleList[_m_curSelectSinglePlayerTargetIdx];
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

        public bool CheckHasPlayerActorDead()
        {
            foreach (var actor in playerActorModuleList)
            {
                if (actor.actorInfo.hasDead)
                    return true;
            }
            return false;
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

        public List<TBSActorInfo> GetActorInfoList(bool _isPlayer)
        {
            List<TBSActorInfo> resInfoList = new List<TBSActorInfo>();
            if(_isPlayer)
            {
                for (int i = 0; i < playerActorModuleList.Count; i++)
                {
                    resInfoList.Add(playerActorModuleList[i].actorInfo);
                }
            }
            else
            {
                for (int i = 0; i < enemyActorModuleList.Count; i++)
                {
                    resInfoList.Add(enemyActorModuleList[i].actorInfo);
                }
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



        public TBSActorBase GetNextAliveActor(bool _isPlayerActor, int _startIdx,bool _includeSelf = false)
        {
            int tmpIdx = 0;
            if (_includeSelf)
                tmpIdx = _startIdx;
            else
                tmpIdx = _startIdx +1;

            if (_isPlayerActor)
            {
                if (tmpIdx >= playerActorModuleList.Count)
                    tmpIdx = 0;
                TBSActorBase tmpActor = playerActorModuleList[tmpIdx];
                while (tmpActor.actorInfo.hasDead)
                {
                    tmpIdx++;
                    if (tmpIdx >= playerActorModuleList.Count)
                        tmpIdx = 0;
                    tmpActor = playerActorModuleList[tmpIdx];
                }
                return tmpActor;
            }
            else
            {
                if (tmpIdx >= enemyActorModuleList.Count)
                    tmpIdx = 0;
                TBSActorBase tmpActor = enemyActorModuleList[tmpIdx];
                while (tmpActor.actorInfo.hasDead)
                {
                    tmpIdx++;
                    if (tmpIdx >= enemyActorModuleList.Count)
                        tmpIdx = 0;
                    tmpActor = enemyActorModuleList[tmpIdx];
                }
                return tmpActor;
            }
        }

        public int GetNextAliveActorIndex(bool _isPlayerActor, int _startIdx,bool _includeSelf = false)
        {
            int tmpIdx = 0;
            if (_includeSelf)
                tmpIdx = _startIdx;
            else
                tmpIdx = _startIdx + 1;
            if (_isPlayerActor)
            {
                if (tmpIdx >= playerActorModuleList.Count)
                    tmpIdx = 0;
                TBSActorBase tmpActor = playerActorModuleList[tmpIdx];
                while (tmpActor.actorInfo.hasDead)
                {
                    tmpIdx++;
                    if (tmpIdx >= playerActorModuleList.Count)
                        tmpIdx = 0;
                    tmpActor = playerActorModuleList[tmpIdx];
                }
                return tmpIdx;
            }
            else
            {
                if (tmpIdx >= enemyActorModuleList.Count)
                    tmpIdx = 0;
                TBSActorBase tmpActor = enemyActorModuleList[tmpIdx];
                while (tmpActor.actorInfo.hasDead)
                {
                    tmpIdx++;
                    if (tmpIdx >= enemyActorModuleList.Count)
                        tmpIdx = 0;
                    tmpActor = enemyActorModuleList[tmpIdx];
                }
                return tmpIdx;
            }
        }

        public int GetLastAliveActorIndex(bool _isPlayerActor, int _startIdx,bool _includeSelf = false)
        {
            int tmpIdx = 0;
            if (_includeSelf)
                tmpIdx = _startIdx;
            else
                tmpIdx = _startIdx - 1;
            if (_isPlayerActor)
            {
                if (tmpIdx < 0)
                    tmpIdx = playerActorModuleList.Count - 1;
                TBSActorBase tmpActor = playerActorModuleList[tmpIdx];
                while (tmpActor.actorInfo.hasDead)
                {
                    tmpIdx--;
                    if (tmpIdx < 0)
                        tmpIdx = playerActorModuleList.Count - 1;
                    tmpActor = playerActorModuleList[tmpIdx];
                }
                return tmpIdx;
            }
            else
            {
                if (tmpIdx < 0)
                    tmpIdx = enemyActorModuleList.Count - 1;
                TBSActorBase tmpActor = enemyActorModuleList[tmpIdx];
                while (tmpActor.actorInfo.hasDead)
                {
                    tmpIdx--;
                    if (tmpIdx < 0)
                        tmpIdx = enemyActorModuleList.Count - 1;
                    tmpActor = enemyActorModuleList[tmpIdx];
                }
                return tmpIdx;
            }
        }


        public TBSActorBase GetNextDeadPlayerActor(int _startIdx,bool _includeSelf = false)
        {
            //没有人死 就返回空
            if (!CheckHasPlayerActorDead())
                return null;
            int tmpIdx = 0;
            if(_includeSelf)
                tmpIdx = _startIdx;
            else
                 tmpIdx = _startIdx + 1;

            if (tmpIdx >= playerActorModuleList.Count)
                tmpIdx = 0;
            TBSActorBase tmpActor = playerActorModuleList[tmpIdx];
            while (!tmpActor.actorInfo.hasDead)
            {
                tmpIdx++;
                if (tmpIdx >= playerActorModuleList.Count)
                    tmpIdx = 0;
                tmpActor = playerActorModuleList[tmpIdx];
            }
            return tmpActor;
        }

        public int GetNextDeadPlayerActorIndex(int _startIdx, bool _includeSelf = false)
        {
            //没有人死 就返回原来的索引
            if (!CheckHasPlayerActorDead())
                return _startIdx;


            int tmpIdx = 0;
            if(_includeSelf)
                tmpIdx = _startIdx;
            else
                tmpIdx = _startIdx + 1;

            if (tmpIdx >= playerActorModuleList.Count)
                tmpIdx = 0;
            TBSActorBase tmpActor = playerActorModuleList[tmpIdx];
            while (!tmpActor.actorInfo.hasDead)
            {
                tmpIdx++;
                if (tmpIdx >= playerActorModuleList.Count)
                    tmpIdx = 0;
                tmpActor = playerActorModuleList[tmpIdx];
            }
            return tmpIdx;
        }
        public TBSActorBase GetLastDeadPlayerActor(int _startIdx,bool _includeSelf = false)
        {
            //没有人死 就返回空
            if (!CheckHasPlayerActorDead())
                return null;
            int tmpIdx = 0;
            if(_includeSelf)
                tmpIdx = _startIdx;
            else
                tmpIdx = _startIdx - 1;

            if (tmpIdx < 0)
                tmpIdx = playerActorModuleList.Count - 1;
            TBSActorBase tmpActor = playerActorModuleList[tmpIdx];
            while (!tmpActor.actorInfo.hasDead)
            {
                tmpIdx--;
                if (tmpIdx < 0)
                    tmpIdx = playerActorModuleList.Count - 1;
                tmpActor = playerActorModuleList[tmpIdx];
            }
            return tmpActor;
        }

        public int GetLastDeadPlayerActorIndex(int _startIdx,bool _includeSelf = false)
        {
            //没有人死 就返回原来的索引
            if (!CheckHasPlayerActorDead())
                return _startIdx;
            int tmpIdx = 0;
            if (_includeSelf)
                tmpIdx = _startIdx;
            else
                tmpIdx = _startIdx - 1;

            if (tmpIdx < 0)
                tmpIdx = playerActorModuleList.Count - 1;
            TBSActorBase tmpActor = playerActorModuleList[tmpIdx];
            while (!tmpActor.actorInfo.hasDead)
            {
                tmpIdx--;
                if (tmpIdx < 0)
                    tmpIdx = playerActorModuleList.Count - 1;
                tmpActor = playerActorModuleList[tmpIdx];
            }
            return tmpIdx;
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

        public List<Vector3> GetPosList(bool _isPlayer,ETargetAliveType _aliveType)
        {
            List<Vector3> posList = new List<Vector3>();
            TBSActorBase actor = null;
            if (_isPlayer)
            {
                for (int i = 0; i < playerActorModuleList.Count; i++)
                {
                    actor = playerActorModuleList[i];
                    if (_aliveType == ETargetAliveType.ALIVE && actor.actorInfo.hasDead)
                        continue;
                    if (_aliveType == ETargetAliveType.DEAD && !actor.actorInfo.hasDead)
                        continue;
                    posList.Add(actor.GetCursorPos());
                }
            }
            else
            {
                for (int i = 0; i < enemyActorModuleList.Count; i++)
                {
                    actor = enemyActorModuleList[i];
                    if (_aliveType == ETargetAliveType.ALIVE && actor.actorInfo.hasDead)
                        continue;
                    if (_aliveType == ETargetAliveType.DEAD && !actor.actorInfo.hasDead)
                        continue;
                    posList.Add(enemyActorModuleList[i].GetCursorPos());
                }
            }
            return posList;
        }

        public List<bool> ApplyExp2AllPlayerActor()
        {
            List<bool> resList = new List<bool>();
            List<TBSActorInfo> actorInfoList = GetActorInfoList(true);
            TBSActorInfo actorInfo = null;
            bool tmpBool = false;
            for (int i = 0; i < actorInfoList.Count; i++)
            {
                actorInfo = actorInfoList[i];
                if (actorInfo == null)
                    continue;

                tmpBool = SCDataMgr.instance.GetExp(actorInfo.characterRefObj.id, _m_curKillEnemyExp);
                resList.Add(tmpBool);
            }
            return resList;

        }
        public int ApplyMoney2Player()
        {
            SCDataMgr.instance.GetMoney(_m_curKillEnemyMoney);
            return _m_curKillEnemyMoney;
        }

        public void AddKillEnemyLoot(TBSActorInfo _enemyInfo)
        {
            if (_enemyInfo == null)
                return;
            _m_curKillEnemyExp += _enemyInfo.dropExp;
            _m_curKillEnemyMoney += _enemyInfo.dropMoney;
        }



    }
}
