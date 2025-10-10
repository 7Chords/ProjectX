using GameCore.RefData;
using SCFrame;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSActorInfo
    {
        public long characterId;
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
        public float criticalChance;
        public EArmorLevelType armorLevel;
        public EMagicResistanceLevelType magicResistanceLevel;
        public EDamageType attackDamageType;
        public EPhysicalLevelType attackPhysicalLevel;
        public EMagicAttributeType attackMagicAttribute;

        public void InitNewInfo(CharacterRefObj _characterRefObj)
        {
            if(_characterRefObj == null)
            {
                Debug.LogError("TBSActorInfo传入空参数！！！");
                return;
            }
            characterId = _characterRefObj.id;
            characterName = _characterRefObj.characterName;
            ProfessionRefObj professioRefObj = SCRefDataMgr.instance.professionRefList.refDataList.Find(x => x.id == _characterRefObj.characterProfession);
            if(professioRefObj == null)
            {
                Debug.LogError("读取professioRefObj时出错！！！");
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
            criticalChance = _characterRefObj.initCritical;
            armorLevel = _characterRefObj.initArmorLevel;
            magicResistanceLevel = _characterRefObj.initMgicResistanceLevel;
            attackDamageType = _characterRefObj.attackDamageType;
            attackPhysicalLevel = _characterRefObj.attackPhysicalLevel;
            attackMagicAttribute = _characterRefObj.attackMagicAttribute;

            curHp = maxHp;
            curMp = maxMp;
        }

        public void LoadInfo()
        {

        }
    }
}
