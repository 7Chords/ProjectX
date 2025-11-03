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

        public static TBSGameSkillInfo CreateTBSSkillInfo()
        {
            return new TBSGameSkillInfo();
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
                    _attackInfo.srcActorList[i].TakeDamage(_attackInfo.srcUseHpList[i],false);
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
                string extraStr = "";
                foreach(var actor in _attackInfo.targetActorList)
                {
                    if (actor.MissJudge())
                    {
                        actor.Miss();
                    }
                    else
                    {
                        tmpDamage = _attackInfo.baseDamage;
                        if(_attackInfo.damageType == EDamageType.PHYSICAL)
                        {
                            tmpDamage  *= getPhysicalPenetrateRate(_attackInfo.physicsLevelType, actor.actorInfo.armorLevel, out extraStr);
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
                                tmpDamage *= getMagicWeakRate(_attackInfo.magicAttributeType, actor.actorInfo,out extraStr);
                            }
                        }
                        else if(_attackInfo.damageType == EDamageType.REAL)
                        {
                            //真实伤害无处理
                        }
                        actor.TakeDamage(Mathf.RoundToInt(tmpDamage),true, extraStr);
                        Debug.Log("===TBS===" + actor.actorInfo.characterName + "受到了" + Mathf.RoundToInt(tmpDamage) + "点伤害");
                    }
                }
            }
        }

        /// <summary>
        /// 处理技能 相对于普攻 多一个量级伤害
        /// </summary>
        /// <param name="_skillInfo"></param>
        public static void DealSkill(TBSGameSkillInfo _skillInfo)
        {
            if (_skillInfo == null)
                return;
            if (_skillInfo.srcActorList == null)
                return;
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
                return;
            for (int i = 0; i < _skillInfo.srcActorList.Count; i++)
            {
                if (_skillInfo.srcUseHpList != null && _skillInfo.srcUseHpList.Count > i)
                    _skillInfo.srcActorList[i].TakeDamage(_skillInfo.srcUseHpList[i], false);
                if (_skillInfo.srcUseMpList != null && _skillInfo.srcUseMpList.Count > i)
                    _skillInfo.srcActorList[i].TakeMagic(_skillInfo.srcUseMpList[i]);
            }
            //List<int> damageList = new List<int>();
            //如果是多人合击技能 不计算闪避
            //todo：目前没有多人合击设计 略过
            if (_skillInfo.srcActorList.Count > 1)
            {

            }
            else
            {
                //闪避 --> 穿透 -->暴击
                //闪避 --> 抵抗 -->克制
                float tmpDamage = 0;
                string extraStr = "";
                foreach (var actor in _skillInfo.targetActorList)
                {
                    if (actor.MissJudge())
                    {
                        actor.Miss();
                    }
                    else
                    {
                        tmpDamage = _skillInfo.baseDamage;
                        if (_skillInfo.damageType == EDamageType.PHYSICAL)
                        {
                            tmpDamage *= getPhysicalPenetrateRate(_skillInfo.physicsLevelType, actor.actorInfo.armorLevel, out extraStr);
                            if (_skillInfo.srcActorList[0].CriticalJudge())
                                tmpDamage *= tbsConfigRefObj.tbsCrticalMultiplier;
                        }
                        else if (_skillInfo.damageType == EDamageType.MAGIC)
                        {
                            if (magicInvalidJudge(_skillInfo.magicAttributeType, actor.actorInfo))
                            {
                                actor.GetAttackInvalid();
                                return;
                            }
                            else if (magicBounceJudge(_skillInfo.magicAttributeType, actor.actorInfo))
                            {
                                actor.GetAttackBounce();
                                return;
                            }
                            else if (magicSuckJudge(_skillInfo.magicAttributeType, actor.actorInfo))
                            {
                                actor.GetAttackSuck();
                                return;
                            }
                            else
                            {
                                tmpDamage *= getMagicResistanceRate(actor.actorInfo.magicResistanceLevel);
                                tmpDamage *= getMagicWeakRate(_skillInfo.magicAttributeType, actor.actorInfo, out extraStr);
                            }
                        }
                        else if (_skillInfo.damageType == EDamageType.REAL)
                        {
                            //真实伤害无处理
                        }
                        actor.TakeDamage(Mathf.RoundToInt(tmpDamage), true, extraStr);
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
        private static float getPhysicalPenetrateRate(EPhysicalLevelType _physicalLevelType,EArmorLevelType _armorLevelType,out string _extraStr)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
            {
                _extraStr = "";
                return 0;
            }
            int gap = (int)_physicalLevelType - (int)_armorLevelType;
            switch (gap)
            {
                case -1:
                    {
                        _extraStr = "";
                        return tbsConfigRefObj.tbsPhysicalPenetrateLowerOneMultiplier;
                    }
                case -2:
                    {
                        _extraStr = "";
                        return tbsConfigRefObj.tbsPhysicalPenetrateLowerTwoMultiplier;
                    }
                case -3:
                    {
                        _extraStr = "";
                        return tbsConfigRefObj.tbsPhysicalPenetrateLowerThreeMultiplier;
                    }
                case 0:
                    {
                        _extraStr = "";
                        return tbsConfigRefObj.tbsPhysicalPenetrateSameMultiplier;
                    }
                case 1:
                    {
                        _extraStr = LanguageHelper.instance.GetTextTranslate("#1_penetrate_lv_one");
                        return tbsConfigRefObj.tbsPhysicalPenetrateHigherOneMultiplier;
                    }
                case 2:
                    {
                        _extraStr = LanguageHelper.instance.GetTextTranslate("#1_penetrate_lv_two");
                        return tbsConfigRefObj.tbsPhysicalPenetrateHigherOneMultiplier;
                    }
                case 3:
                    {
                        _extraStr = LanguageHelper.instance.GetTextTranslate("#1_penetrate_lv_three");
                        return tbsConfigRefObj.tbsPhysicalPenetrateHigherOneMultiplier;
                    }
                default:
                    {
                        _extraStr = "";
                        return 0;
                    }
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
        private static float getMagicWeakRate(EMagicAttributeType _srcAttribute, TBSActorInfo _attackInfo,out string _extraStr)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
            {
                _extraStr = "";
                return 0;
            }
            if (_attackInfo.weakAttributeList.Contains(_srcAttribute))
            {
                _extraStr = LanguageHelper.instance.GetTextTranslate("#1_weak");
                return tbsConfigRefObj.tbsMagicWeakMultiplier;
            }
            if (_attackInfo.resistentAttributeList.Contains(_srcAttribute))
            {
                _extraStr = LanguageHelper.instance.GetTextTranslate("#1_resist");
                return tbsConfigRefObj.tbsMagicResistMultiplier;
            }
            if (_attackInfo.normalAttributeList.Contains(_srcAttribute))
            {
                _extraStr = "";
                return tbsConfigRefObj.tbsMagicNormalMultiplier;
            }
            _extraStr = "";
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
