using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

        public void InitData()
        {

        }

        public void ResetData()
        {
            curTurnType = ETBSTurnType.PLAYER;
            curTurnCount = 1;
            battleInfo = null;
        }
    }
}
