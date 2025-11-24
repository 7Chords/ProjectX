using GameCore.RefData;
using SCFrame;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSActorInfo
    {
        public EProfessionType professionType;
        public int characterLv;
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
        public List<EMagicAttributeType> weakAttributeList;
        public List<EMagicAttributeType> normalAttributeList;
        public List<EMagicAttributeType> resistentAttributeList;
        public List<EMagicAttributeType> invilidAttributeList;
        public List<EMagicAttributeType> bounceAttributeList;
        public List<EMagicAttributeType> suckAttributeList;

        public CharacterRefObj characterRefObj;

        public bool hasDead;//是否死亡
        public void InitNewInfo(CharacterRefObj _characterRefObj)
        {
            if(_characterRefObj == null)
            {
                Debug.LogError("TBSActorInfo传入空参数！！！");
                return;
            }
            characterRefObj = _characterRefObj;

            characterLv = 1;//todo
            ProfessionRefObj professioRefObj = SCRefDataMgr.instance.professionRefList.refDataList.Find(x => x.id == _characterRefObj.characterProfession);
            if(professioRefObj == null)
            {
                Debug.LogError("读取professioRefObj时出错！！！");
                return;
            }
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
            weakAttributeList = _characterRefObj.weakAttributeList;
            normalAttributeList = _characterRefObj.normalAttributeList;
            resistentAttributeList = _characterRefObj.resistentAttributeList;
            invilidAttributeList = _characterRefObj.invilidAttributeList;
            bounceAttributeList = _characterRefObj.bounceAttributeList;
            suckAttributeList = _characterRefObj.suckAttributeList;


            curHp = maxHp;
            curMp = maxMp;
            hasDead = false;

        }

        public void LoadInfo()
        {

        }
    }
}
