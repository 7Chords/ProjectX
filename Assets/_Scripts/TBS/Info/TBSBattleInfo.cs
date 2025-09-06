using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{

    /// <summary>
    /// 回合制游戏战斗信息 用于初始化战斗
    /// </summary>
    public class TBSBattleInfo
    {
        public ETBSTurnType firstMoveTurnType;
        public TBSTeamInfo playerTeamInfo;
        public TBSTeamInfo enemyTeamInfo;
        public List<TBSEffectInfo> effectInfoList;
        public List<ETBSCompType> basicCompTypeList;
    }
}
