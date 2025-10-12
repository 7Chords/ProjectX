using GameCore.RefData;
using SCFrame;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{

    /// <summary>
    /// TBS攻击和技能处理器
    /// 物理没有弱点设计
    /// 法术有弱点 抵抗等设计
    /// </summary>
    public static class TBSAttackHandler
    {

        public static TBSGameAttackInfo CreateTBSAttackInfo()
        {
            return new TBSGameAttackInfo();
        }
        public static void DealAttack(TBSGameAttackInfo _attackInfo)
        {
            if (_attackInfo == null)
                return;
            if (_attackInfo.srcActorList == null)
                return;
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
                return;
            for (int i =0;i<_attackInfo.srcActorList.Count;i++)
            {
                if (_attackInfo.srcUseHpList != null && _attackInfo.srcUseHpList.Count > i)
                    _attackInfo.srcActorList[i].TakeDamage(_attackInfo.srcUseHpList[i]);
                if (_attackInfo.srcUseMpList != null && _attackInfo.srcUseMpList.Count > i)
                    _attackInfo.srcActorList[i].TakeMagic(_attackInfo.srcUseMpList[i]);
            }
            //List<int> damageList = new List<int>();
            //如果是多人合击技能 不计算闪避
            //todo：目前没有多人合击设计 略过
            if(_attackInfo.srcActorList.Count > 1)
            {

            }
            else
            {
                //闪避 --> 穿透 -->暴击
                //闪避 --> 抵抗 -->克制
                float tmpDamage = 0;
                foreach(var actor in _attackInfo.targetActorList)
                {
                    if (actor.MissJudge())
                    {

                    }
                    else
                    {
                        tmpDamage = _attackInfo.baseDamage;
                        if(_attackInfo.damageType == EDamageType.PHYSICAL)
                        {
                            tmpDamage  *= getPhysicalPenetrateRate(_attackInfo.physicsLevelType, actor.actorInfo.armorLevel);
                            if (_attackInfo.srcActorList[0].CriticalJudge())
                                tmpDamage *= tbsConfigRefObj.tbsCrticalMultiplier;
                        }
                        else if(_attackInfo.damageType == EDamageType.MAGIC)
                        {
                            if(magicInvalidJudge(_attackInfo.magicAttributeType, actor.actorInfo))
                            {
                                actor.GetAttackInvalid();
                                return;
                            }
                            else if(magicBounceJudge(_attackInfo.magicAttributeType, actor.actorInfo))
                            {
                                actor.GetAttackBounce();
                                return;
                            }
                            else if (magicSuckJudge(_attackInfo.magicAttributeType, actor.actorInfo))
                            {
                                actor.GetAttackSuck();
                                return;
                            }
                            else
                            {
                                tmpDamage *= getMagicResistanceRate(actor.actorInfo.magicResistanceLevel);
                                tmpDamage *= getMagicWeakRate(_attackInfo.magicAttributeType, actor.actorInfo);
                            }
                        }
                        else if(_attackInfo.damageType == EDamageType.REAL)
                        {
                            //真实伤害无处理
                        }
                        actor.TakeDamage(Mathf.RoundToInt(tmpDamage));
                        Debug.Log("===TBS===" + actor.actorInfo.characterName + "受到了" + Mathf.RoundToInt(tmpDamage) + "点伤害");
                    }
                }
            }
        }

        /// <summary>
        /// 获得物理穿透倍率
        /// </summary>
        /// <param name="_physicalLevelType"></param>
        /// <param name="_armorLevelType"></param>
        /// <returns></returns>
        private static float getPhysicalPenetrateRate(EPhysicalLevelType _physicalLevelType,EArmorLevelType _armorLevelType)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
                return 0;

            switch(_physicalLevelType)
            {
                case EPhysicalLevelType.LIGHT:
                    switch(_armorLevelType)
                    {
                        case EArmorLevelType.LIGHT:
                            return tbsConfigRefObj.tbsPhysicalPenetrateSameMultiplier;
                        case EArmorLevelType.MEDIUM:
                            return tbsConfigRefObj.tbsPhysicalPenetrateLowerOneMultiplier;
                        case EArmorLevelType.HEAVY:
                            return tbsConfigRefObj.tbsPhysicalPenetrateLowerTwoMultiplier;
                        case EArmorLevelType.HERO:
                            return tbsConfigRefObj.tbsPhysicalPenetrateLowerThreeMultiplier;
                        default:
                            return 0;
                    }
                case EPhysicalLevelType.MEDIUM:
                    switch (_armorLevelType)
                    {
                        case EArmorLevelType.LIGHT:
                            return tbsConfigRefObj.tbsPhysicalPenetrateHigherOneMultiplier;
                        case EArmorLevelType.MEDIUM:
                            return tbsConfigRefObj.tbsPhysicalPenetrateSameMultiplier;
                        case EArmorLevelType.HEAVY:
                            return tbsConfigRefObj.tbsPhysicalPenetrateLowerOneMultiplier;
                        case EArmorLevelType.HERO:
                            return tbsConfigRefObj.tbsPhysicalPenetrateLowerTwoMultiplier;
                        default:
                            return 0;
                    }
                case EPhysicalLevelType.HEAVY:
                    switch (_armorLevelType)
                    {
                        case EArmorLevelType.LIGHT:
                            return tbsConfigRefObj.tbsPhysicalPenetrateHigherTwoMultiplier;
                        case EArmorLevelType.MEDIUM:
                            return tbsConfigRefObj.tbsPhysicalPenetrateHigherOneMultiplier;
                        case EArmorLevelType.HEAVY:
                            return tbsConfigRefObj.tbsPhysicalPenetrateSameMultiplier;
                        case EArmorLevelType.HERO:
                            return tbsConfigRefObj.tbsPhysicalPenetrateLowerOneMultiplier;
                        default:
                            return 0;
                    }
                case EPhysicalLevelType.HERO:
                    switch (_armorLevelType)
                    {
                        case EArmorLevelType.LIGHT:
                            return tbsConfigRefObj.tbsPhysicalPenetrateHigherThreeMultiplier;
                        case EArmorLevelType.MEDIUM:
                            return tbsConfigRefObj.tbsPhysicalPenetrateHigherTwoMultiplier;
                        case EArmorLevelType.HEAVY:
                            return tbsConfigRefObj.tbsPhysicalPenetrateHigherOneMultiplier;
                        case EArmorLevelType.HERO:
                            return tbsConfigRefObj.tbsPhysicalPenetrateSameMultiplier;
                        default:
                            return 0;
                    }
                default:
                    return 0;
            }
        }


        /// <summary>
        /// 获得魔法抵抗倍率
        /// </summary>
        /// <param name="_physicalLevelType"></param>
        /// <param name="_armorLevelType"></param>
        /// <returns></returns>
        private static float getMagicResistanceRate(EMagicResistanceLevelType _magicResistanceLevelType)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
                return 0;
            switch (_magicResistanceLevelType)
            {
                case EMagicResistanceLevelType.LIGHT:
                    return tbsConfigRefObj.tbsMagicResistanceLightMultiplier;
                case EMagicResistanceLevelType.MEDIUM:
                    return tbsConfigRefObj.tbsMagicResistanceMediumMultiplier;
                case EMagicResistanceLevelType.HEAVY:
                    return tbsConfigRefObj.tbsMagicResistanceHeavyMultiplier;
                case EMagicResistanceLevelType.HERO:
                    return tbsConfigRefObj.tbsMagicResistanceHeroMultiplier;
                default:
                    return 0;
            }

        }

        /// <summary>
        /// 获得魔法克制倍率
        /// </summary>
        /// <param name="_srcAttribute"></param>
        /// <param name="_tarAttribute"></param>
        /// <returns></returns>
        private static float getMagicWeakRate(EMagicAttributeType _srcAttribute,EMagicAttributeType _tarAttribute)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
                return 0;
            switch (_srcAttribute)
            {
                case EMagicAttributeType.FIRE:
                    switch(_tarAttribute)
                    {
                        case EMagicAttributeType.FIRE:
                            return tbsConfigRefObj.tbsMagicNormalMultiplier;
                        case EMagicAttributeType.WATER:
                            return tbsConfigRefObj.tbsMagicResistMultiplier;
                        case EMagicAttributeType.WOOD:
                            return tbsConfigRefObj.tbsMagicWeakMultiplier;
                        default:
                            return 0;
                    }
                case EMagicAttributeType.WATER:
                    switch (_tarAttribute)
                    {
                        case EMagicAttributeType.FIRE:
                            return tbsConfigRefObj.tbsMagicWeakMultiplier;
                        case EMagicAttributeType.WATER:
                            return tbsConfigRefObj.tbsMagicNormalMultiplier;
                        case EMagicAttributeType.WOOD:
                            return tbsConfigRefObj.tbsMagicResistMultiplier;
                        default:
                            return 0;
                    }
                case EMagicAttributeType.WOOD:
                    switch (_tarAttribute)
                    {
                        case EMagicAttributeType.FIRE:
                            return tbsConfigRefObj.tbsMagicResistMultiplier;
                        case EMagicAttributeType.WATER:
                            return tbsConfigRefObj.tbsMagicWeakMultiplier;
                        case EMagicAttributeType.WOOD:
                            return tbsConfigRefObj.tbsMagicNormalMultiplier;
                        default:
                            return 0;
                    }
                default:
                    return 0;
            }
        }



        /// <summary>
        /// 获得魔法克制倍率
        /// </summary>
        /// <param name="_srcAttribute"></param>
        /// <param name="_tarAttribute"></param>
        /// <returns></returns>
        private static float getMagicWeakRate(EMagicAttributeType _srcAttribute, TBSActorInfo _attackInfo)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
                return 0;
            if (_attackInfo.weakAttributeList.Contains(_srcAttribute))
                return tbsConfigRefObj.tbsMagicWeakMultiplier;
            if (_attackInfo.resistentAttributeList.Contains(_srcAttribute))
                return tbsConfigRefObj.tbsMagicResistMultiplier;
            if (_attackInfo.normalAttributeList.Contains(_srcAttribute))
                return tbsConfigRefObj.tbsMagicNormalMultiplier;
            return 0;

        }



        /// <summary>
        /// 魔法是否无效的判断
        /// </summary>
        /// <returns></returns>
        private static bool magicInvalidJudge(EMagicAttributeType _srcAttribute, TBSActorInfo _attackInfo)
        {
            if (_attackInfo == null)
                return false;
            if (_attackInfo.invilidAttributeList.Contains(_srcAttribute))
                return true;
            return false;

        }
        /// <summary>
        /// 魔法是否反弹的判断
        /// </summary>
        /// <returns></returns>
        private static bool magicBounceJudge(EMagicAttributeType _srcAttribute, TBSActorInfo _attackInfo)
        {
            if (_attackInfo == null)
                return false;
            if (_attackInfo.bounceAttributeList.Contains(_srcAttribute))
                return true;
            return false;
        }
        /// <summary>
        /// 魔法是否吸血的判断
        /// </summary>
        /// <returns></returns>
        private static bool magicSuckJudge(EMagicAttributeType _srcAttribute, TBSActorInfo _attackInfo)
        {
            if (_attackInfo == null)
                return false;
            if (_attackInfo.suckAttributeList.Contains(_srcAttribute))
                return true;
            return false;
        }
    }
}
