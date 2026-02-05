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
        public int curExp;
        public int levelFullExp;
        public int dropExp;
        public int dropMoney;
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

        public void Init(ActorData _data,bool _isEnemy)
        {
            CharacterRefObj refObj = SCRefDataMgr.instance.characterRefList.refDataList.Find(x => x.id == _data.characterId);
            if (refObj == null)
            {
                Debug.LogError("没有id为"+_data.characterId+"的角色配表数据！！！");
                return;
            }
            characterRefObj = refObj;

            characterLv = _data.characterLv;
            curExp = _data.curExp;

            //找到当前角色当前等级的配表数据
            LevelRefObj levelRefObj = SCRefDataMgr.instance.levelRefList.refDataList.Find(x => (x.characterId == _data.characterId && x.characterLevel == characterLv));
            levelFullExp = levelRefObj.needExpToNextLevel;
            dropExp = levelRefObj.dropExp;
            dropMoney = levelRefObj.dropMoney;

            ProfessionRefObj professioRefObj = SCRefDataMgr.instance.professionRefList.refDataList.Find(x => x.id == characterRefObj.characterProfession);
            if (professioRefObj == null)
            {
                Debug.LogError("读取professioRefObj时出错！！！");
                return;
            }
            professionType = professioRefObj.professionType;
            skillList = _data.skillList;

            originalAttack = _data.attack;
            originalDefend = _data.defend;
            originalMissChance = _data.missChance;
            originalCriticalChance = _data.criticalChance;
            originalAttackTargetType = _data.attackTargetType;
            originalArmorLevel = _data.armorLevel;
            originalMagicResistanceLevel = _data.magicResistanceLevel;
            originalAttackDamageType = _data.attackDamageType;
            originalAttackPhysicalLevel = _data.attackPhysicalLevel;
            originalAttackMagicAttribute = _data.attackMagicAttribute;




            maxHp = _data.maxHp;
            maxMp = _data.maxMp;
            attack = _data.attack;
            defend = _data.defend;
            missChance = _data.missChance;
            criticalChance = _data.criticalChance;
            attackTargetType = _data.attackTargetType;
            armorLevel = _data.armorLevel;
            magicResistanceLevel = _data.magicResistanceLevel;
            attackDamageType = _data.attackDamageType;
            attackPhysicalLevel = _data.attackPhysicalLevel;
            attackMagicAttribute = _data.attackMagicAttribute;
            weakAttributeList = _data.weakAttributeList;
            normalAttributeList = _data.normalAttributeList;
            resistentAttributeList = _data.resistentAttributeList;
            invilidAttributeList = _data.invilidAttributeList;
            bounceAttributeList = _data.bounceAttributeList;
            suckAttributeList = _data.suckAttributeList;


            curHp = maxHp;
            curMp = maxMp;

            runningId = SCModel.instance.tbsModel.TakeRunningId();
            hasDead = false;
            isEnemy = _isEnemy;
        }

        public void CopyData(TBSActorInfo _other)
        {
            characterRefObj = _other.characterRefObj;
            characterLv = _other.characterLv;
            curExp = _other.curExp;
            levelFullExp = _other.levelFullExp;

            professionType = _other.professionType;
            skillList = _other.skillList;

            originalAttack = _other.attack;
            originalDefend = _other.defend;
            originalMissChance = _other.missChance;
            originalCriticalChance = _other.criticalChance;
            originalAttackTargetType = _other.attackTargetType;
            originalArmorLevel = _other.armorLevel;
            originalMagicResistanceLevel = _other.magicResistanceLevel;
            originalAttackDamageType = _other.attackDamageType;
            originalAttackPhysicalLevel = _other.attackPhysicalLevel;
            originalAttackMagicAttribute = _other.attackMagicAttribute;

            maxHp = _other.maxHp;
            maxMp = _other.maxMp;
            attack = _other.attack;
            defend = _other.defend;
            missChance = _other.missChance;
            criticalChance = _other.criticalChance;
            attackTargetType = _other.attackTargetType;
            armorLevel = _other.armorLevel;
            magicResistanceLevel = _other.magicResistanceLevel;
            attackDamageType = _other.attackDamageType;
            attackPhysicalLevel = _other.attackPhysicalLevel;
            attackMagicAttribute = _other.attackMagicAttribute;
            weakAttributeList = _other.weakAttributeList;
            normalAttributeList = _other.normalAttributeList;
            resistentAttributeList = _other.resistentAttributeList;
            invilidAttributeList = _other.invilidAttributeList;
            bounceAttributeList = _other.bounceAttributeList;
            suckAttributeList = _other.suckAttributeList;

            curHp = maxHp;
            curMp = maxMp;

            runningId = SCModel.instance.tbsModel.TakeRunningId();
            hasDead = _other.hasDead;
            isEnemy = _other.isEnemy;
        }

        public void ResetDataByLevel()
        {
            //找到当前角色当前等级的配表数据
            LevelRefObj levelRefObj = SCRefDataMgr.instance.levelRefList.refDataList.Find(x => (x.characterId == characterRefObj.id && x.characterLevel == characterLv));
            levelFullExp = levelRefObj.needExpToNextLevel;
            dropExp = levelRefObj.dropExp;
            dropMoney = levelRefObj.dropMoney;
            skillList = levelRefObj.skill_list;
            extraCompList = levelRefObj.extraCompList;

            maxHp = levelRefObj.maxHp;
            maxMp = levelRefObj.maxMp;
            attack = levelRefObj.attack;
            defend = levelRefObj.defend;
            missChance = levelRefObj.missChance;
            criticalChance = levelRefObj.criticalChance;
            attackTargetType = levelRefObj.attackTargetType;
            armorLevel = levelRefObj.armorLevel;
            magicResistanceLevel = levelRefObj.magicResistanceLevel;
            attackDamageType = levelRefObj.attackDamageType;
            attackPhysicalLevel = levelRefObj.attackPhysicalLevel;
            attackMagicAttribute = levelRefObj.attackMagicAttribute;
            weakAttributeList = levelRefObj.weakAttributeList;
            normalAttributeList = levelRefObj.normalAttributeList;
            resistentAttributeList = levelRefObj.resistentAttributeList;
            invilidAttributeList = levelRefObj.invilidAttributeList;
            bounceAttributeList = levelRefObj.bounceAttributeList;
            suckAttributeList = levelRefObj.suckAttributeList;
        }
    }
}
