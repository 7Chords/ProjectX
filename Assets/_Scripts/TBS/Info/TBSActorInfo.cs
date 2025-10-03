using GameCore.RefData;
using SCFrame;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSActorInfo
    {

        public string characterName;

        public EProfessionType professionType;

        public string assetModelObjName;
        public string assetHeadIconObjName;

        public List<ETBSCompType> extraCompList;
        public List<long> skillList;

        public int curHp;
        public int maxHp;
        public int curMp;
        public int maxMp;
        public int attack;
        public int defend;
        public float missChance;
        public EArmorLevelType armorLevel;
        public EMagicResistanceLevelType magicResistanceLevel;

        public void InitNewInfo(CharacterRefObj _characterRefObj)
        {
            characterName = _characterRefObj.characterName;
            ProfessionRefObj professioRefObj = SCRefDataMgr.instance.professionRefList.refDataList.Find(x => x.id == _characterRefObj.characterProfession);
            if(professioRefObj == null)
            {
                Debug.LogError("¶ÁÈ¡professioRefObjÊ±³ö´í£¡£¡£¡");
                return;
            }
            assetModelObjName = _characterRefObj.assetModelObjName;
            assetHeadIconObjName = _characterRefObj.assetHeadIconObjName;
            professionType = professioRefObj.professionType;
            skillList = _characterRefObj.init_skill_list;
            maxHp = _characterRefObj.initHp;
            maxMp = _characterRefObj.initMp;
            attack = _characterRefObj.initAttack;
            defend = _characterRefObj.initDefend;
            missChance = _characterRefObj.initMiss;
            armorLevel = _characterRefObj.initArmorLevel;
            magicResistanceLevel = _characterRefObj.initMgicResistanceLevel;

            curHp = maxHp;
            curMp = maxMp;
        }

        public void LoadInfo()
        {

        }
    }
}
