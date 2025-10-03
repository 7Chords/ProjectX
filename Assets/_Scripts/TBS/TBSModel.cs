using System.Collections.Generic;


namespace GameCore.TBS
{
    /// <summary>
    /// 全局唯一 兼具数据持久化功能
    /// </summary>
    public class TBSModel
    {
        public ETBSTurnType _m_curTurnType;

        public ETBSTurnType curTurnType
        {
            get { return _m_curTurnType; }
            set { _m_curTurnType = value; }
        }

        public int _m_curTurnCount;

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

        public int _m_curActorIndex;
        public int curActorIndex
        {
            get { return _m_curActorIndex; }
            set { _m_curActorIndex = value; }
        }


        /// <summary>
        /// 创新新游戏的时候初始化新的数据
        /// </summary>
        public void InitNewData()
        {
            curTurnType = ETBSTurnType.PLAYER;
            //curTurnCount = 1;
            battleInfo = new TBSBattleInfo();
            battleInfo.InitNewInfo();
        }


        /// <summary>
        /// 从存档中加载数据
        /// </summary>
        public void LoadData()
        {

        }

        /// <summary>
        /// 重制当前数据为新游戏的数据
        /// </summary>
        public void ResetData()
        {
            curTurnType = ETBSTurnType.PLAYER;
            //curTurnCount = 1;
            battleInfo = null;
        }


        /// <summary>
        /// 获得当前的角色信息
        /// </summary>
        /// <returns></returns>
        public TBSActorInfo getCurActorInfo()
        {
            if (curTurnType == ETBSTurnType.PLAYER)
                return battleInfo.playerTeamInfo.actorInfoList[curActorIndex];
            else
                return battleInfo.enemyTeamInfo.actorInfoList[curActorIndex];
        }
    }
}
