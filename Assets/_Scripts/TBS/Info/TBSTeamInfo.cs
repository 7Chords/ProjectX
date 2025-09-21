using GameCore.RefData;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSTeamInfo
    {
        //public ETBSTeamType teamType;
        public string teamName;
        public List<TBSActorInfo> actorInfoList;

        public void InitNewPlayerTeamInfo()
        {
            teamName = SCRefDataMgr.instance.gameInitRefObj?.init_player_team_name;
            List<long> characterList = SCRefDataMgr.instance.gameInitRefObj?.init_player_team_list;
            List<CharacterRefObj> characterRefObjList = SCRefDataMgr.instance.characterRefList.refDataList;
            if (characterList == null || characterList.Count == 0)
            {
                Debug.LogError("init teamInfo时出错，init_team_character_list信息为空！！！");
                return;
            }
            if (characterRefObjList == null || characterRefObjList.Count == 0)
            {
                Debug.LogError("init teamInfo时出错，characterRefList信息为空！！！");
                return;
            }
            actorInfoList = new List<TBSActorInfo>();
            CharacterRefObj characterRefObj = null;
            for (int i =0;i< characterList.Count;i++)
            {
                characterRefObj = characterRefObjList.Find(x => x.id == characterList[i]);
                if (characterRefObj == null)
                    continue;
                TBSActorInfo playerActorInfo = new TBSActorInfo();
                playerActorInfo.InitNewInfo(characterRefObj);
                actorInfoList.Add(playerActorInfo);
            }
        }

        public void InitNewEnemyTeamInfo()
        {
            teamName = SCRefDataMgr.instance.gameInitRefObj?.init_enemy_team_name;
            List<long> characterList = SCRefDataMgr.instance.gameInitRefObj?.init_enemy_team_list;
            List<CharacterRefObj> characterRefObjList = SCRefDataMgr.instance.characterRefList.refDataList;
            if (characterList == null || characterList.Count == 0)
            {
                Debug.LogError("init teamInfo时出错，init_team_character_list信息为空！！！");
                return;
            }
            if (characterRefObjList == null || characterRefObjList.Count == 0)
            {
                Debug.LogError("init teamInfo时出错，characterRefList信息为空！！！");
                return;
            }
            actorInfoList = new List<TBSActorInfo>();
            CharacterRefObj characterRefObj = null;
            for (int i = 0; i < characterList.Count; i++)
            {
                characterRefObj = characterRefObjList.Find(x => x.id == characterList[i]);
                if (characterRefObj == null)
                    continue;
                TBSActorInfo enemyActorInfo = new TBSActorInfo();
                enemyActorInfo.InitNewInfo(characterRefObj);
                actorInfoList.Add(enemyActorInfo);
            }
        }
    }
}
