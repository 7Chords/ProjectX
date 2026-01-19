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

        public void Init(List<ActorData> _playerTeamDataList, List<ActorData> _enemyTeamDataList)
        {
            firstMoveTurnType = ETBSTurnType.PLAYER;
            effectInfoList = new List<TBSEffectInfo>();
            basicCompTypeList = new List<ETBSCompType>();

            playerTeamInfo = new TBSTeamInfo();
            playerTeamInfo.Init("Player", _playerTeamDataList, false);
            enemyTeamInfo = new TBSTeamInfo();
            enemyTeamInfo.Init("Enemy", _enemyTeamDataList, true);

            //初始四件套
            basicCompTypeList.Add(ETBSCompType.NORMAL_ATTACK);
            basicCompTypeList.Add(ETBSCompType.DEFEND);
            basicCompTypeList.Add(ETBSCompType.ITEM);
            basicCompTypeList.Add(ETBSCompType.SKILL);
        }

        public void DeepCopy(TBSBattleInfo _anotherInfo)
        { 

        }
    }
}
