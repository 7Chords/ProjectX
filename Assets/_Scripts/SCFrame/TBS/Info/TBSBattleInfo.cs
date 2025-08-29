using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.TBS
{

    /// <summary>
    /// 回合制游戏战斗信息 用于初始化战斗
    /// </summary>
    public class TBSBattleInfo
    {
        public ETBSTurnType firstMoveTurnType;
        public List<TBSTeamInfo> teamInfoList;
        public List<TBSEffectInfo> effectInfoList;
    }
}
