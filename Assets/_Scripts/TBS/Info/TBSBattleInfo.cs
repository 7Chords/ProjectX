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

        public void InitNewInfo()
        {
            firstMoveTurnType = ETBSTurnType.PLAYER;
            playerTeamInfo = new TBSTeamInfo();
            playerTeamInfo.InitNewInfo();
            enemyTeamInfo = new TBSTeamInfo();
            enemyTeamInfo.InitNewInfo();
            effectInfoList = new List<TBSEffectInfo>();
            basicCompTypeList = new List<ETBSCompType>();
            //初始四件套
            basicCompTypeList.Add(ETBSCompType.NORMAL_ATTACK);
            basicCompTypeList.Add(ETBSCompType.DEFEND);
            basicCompTypeList.Add(ETBSCompType.ITEM);
            basicCompTypeList.Add(ETBSCompType.SKILL);

        }



        public void InitTempInfo()
        {
            firstMoveTurnType = ETBSTurnType.PLAYER;
            playerTeamInfo = new TBSTeamInfo();
            playerTeamInfo.InitNewInfo();
            enemyTeamInfo = new TBSTeamInfo();
            enemyTeamInfo.InitNewInfo();
            effectInfoList = new List<TBSEffectInfo>();
            basicCompTypeList = new List<ETBSCompType>();
            //初始四件套
            basicCompTypeList.Add(ETBSCompType.NORMAL_ATTACK);
            basicCompTypeList.Add(ETBSCompType.DEFEND);
            basicCompTypeList.Add(ETBSCompType.ITEM);
            basicCompTypeList.Add(ETBSCompType.SKILL);

        }
    }
}
