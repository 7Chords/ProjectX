using GameCore.RefData;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public static class TBSEnemyActionHandler
    {
        public static long GetEnemyActionId(long _characterId,int _characterLevel)
        {
            //CharacterRefObj characterRefObj = SCRefDataMgr.instance.characterRefList.refDataList.Find(x => x.id == _characterId);
            //if(characterRefObj == null)
            //{
            //    SCDebugHelper.LogError("找不到id为" + _characterId + "的角色配表数据！！！");
            //    return GameConst.ENEMY_NORMAL_ATTACK_ID;
            //}
            LevelRefObj levelRefObj = SCRefDataMgr.instance.levelRefList.refDataList.Find(x => x.characterId == _characterId && x.characterLevel == _characterLevel);
            if (levelRefObj == null)
            {
                SCDebugHelper.LogError("找不到角色id为" + _characterId + ",等级为"+_characterLevel + "的角色配表数据！！！");
                return GameConst.ENEMY_NORMAL_ATTACK_ID;
            }

            switch (_characterId)
            {
                case 1004://红色史莱姆
                    return GameConst.ENEMY_NORMAL_ATTACK_ID;
                case 1005://尖刺史莱姆
                    return GameConst.ENEMY_NORMAL_ATTACK_ID;
                case 1006://石头巨人
                    {
                        int randomNum = Random.Range(0, 2);
                        if (randomNum == 0)
                            return GameConst.ENEMY_NORMAL_ATTACK_ID;
                        else
                            return levelRefObj.skill_list[Random.Range(0, levelRefObj.skill_list.Count)];
                    }
                case 1008:
                    {
                        int randomNum = Random.Range(0, 3);
                        if (randomNum == 0)
                            return GameConst.ENEMY_NORMAL_ATTACK_ID;
                        else
                            return levelRefObj.skill_list[Random.Range(0, levelRefObj.skill_list.Count)];
                    }
                case 1010://仙人掌拳手
                    {
                        int randomNum = Random.Range(0, 3);
                        if (randomNum == 0)
                            return GameConst.ENEMY_NORMAL_ATTACK_ID;
                        else
                            return levelRefObj.skill_list[Random.Range(0, levelRefObj.skill_list.Count)];
                    }
                default:
                    break;
            }

            return GameConst.ENEMY_NORMAL_ATTACK_ID;
        }

    }
}
