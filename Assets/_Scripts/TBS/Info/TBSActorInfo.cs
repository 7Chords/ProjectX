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

        #region original
        public int originalAttack;
        public int originalDefend;
        public float originalMissChance;
        public float originalCriticalChance;
        public ETargetType originalAttackTargetType;
        public EArmorLevelType originalArmorLevel;
        public EMagicResistanceLevelType originalMagicResistanceLevel;
        public EDamageType originalAttackDamageType;
        public EPhysicalLevelType originalAttackPhysicalLevel;
        public EMagicAttributeType originalAttackMagicAttribute;
        #endregion

        #region Runtime

        public int curHp;
        public int maxHp;
        public int curMp;
        public int maxMp;
        public int attack;
        public int defend;
        public float missChance;
        public float criticalChance;
        public ETargetType attackTargetType;
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

        #endregion

        public CharacterRefObj characterRefObj;

        public long runningId;//游戏运行分配的id 唯一标识一个actor
        public bool hasDead;//是否死亡
        public bool isEnemy;//是否是敌人
        public bool isDefending;//是否正在防御
        public void InitNewInfo(CharacterRefObj _characterRefObj,bool _isEnemy)
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

            originalAttack = _characterRefObj.initAttack;
            originalDefend = _characterRefObj.initDefend;
            originalMissChance = _characterRefObj.initMiss;
            originalCriticalChance = _characterRefObj.initCritical;
            originalAttackTargetType = _characterRefObj.attackTargetType;
            originalArmorLevel = _characterRefObj.initArmorLevel;
            originalMagicResistanceLevel = _characterRefObj.initMgicResistanceLevel;
            originalAttackDamageType = _characterRefObj.attackDamageType;
            originalAttackPhysicalLevel = _characterRefObj.attackPhysicalLevel;
            originalAttackMagicAttribute = _characterRefObj.attackMagicAttribute;




            maxHp = _characterRefObj.initHp;
            maxMp = _characterRefObj.initMp;
            attack = _characterRefObj.initAttack;
            defend = _characterRefObj.initDefend;
            missChance = _characterRefObj.initMiss;
            criticalChance = _characterRefObj.initCritical;
            attackTargetType = _characterRefObj.attackTargetType;
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

            runningId = SCModel.instance.tbsModel.TakeRunningId();
            hasDead = false;
            isEnemy = _isEnemy;

        }

        public void LoadInfo()
        {

        }
    }
}
