using GameCore.RefData;
using GameCore.TBS;
using GameCore.Util;
using SCFrame;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    /// <summary>
    /// 游戏逻辑的一些通用功能
    /// </summary>
    public static class GameCommon
    {

        public static string GetUIResObjPath(string _uiName)
        {
            List<UIResPathRefObj> uiResRefList = SCRefDataMgr.instance.uiResPathRefList.refDataList;
            if (uiResRefList == null || uiResRefList.Count == 0)
                return null;
            return uiResRefList.Find(x => x.uiName == _uiName)?.uiResObjName??null;
        }

        public static Sprite GetSpriteByPhysicalArmor(EArmorLevelType _armorLevelType)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
            {
                Debug.LogError("TBSCommon GetSpriteByPhysicalArmor 无法获得TBSConfigRefObj！！！");
                return null;
            }
            switch (_armorLevelType)
            {
                case EArmorLevelType.LIGHT:
                    return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsRomanNumberOneSpriteObjName);
                case EArmorLevelType.MEDIUM:
                    return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsRomanNumberTwoSpriteObjName);
                case EArmorLevelType.HEAVY:
                    return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsRomanNumberThreeSpriteObjName);
                case EArmorLevelType.HERO:
                    return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsResistanceHeroSpriteObjName);
                default:
                    Debug.LogError("TBSCommon GetSpriteByPhysicalArmor 无效的枚举类型！！！");
                    return null;
            }
        }

        public static Sprite GetSpriteByMagicResistance(EMagicResistanceLevelType _resistanceLevel)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
            {
                Debug.LogError("GameCommon GetSpriteByMagicResistance 无法获得TBSConfigRefObj！！！");
                return null;
            }
            switch (_resistanceLevel)
            {
                case EMagicResistanceLevelType.LIGHT:
                    return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsRomanNumberOneSpriteObjName);
                case EMagicResistanceLevelType.MEDIUM:
                    return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsRomanNumberTwoSpriteObjName);
                case EMagicResistanceLevelType.HEAVY:
                    return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsRomanNumberThreeSpriteObjName);
                case EMagicResistanceLevelType.HERO:
                    return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsResistanceHeroSpriteObjName);
                default:
                    Debug.LogError("GameCommon GetSpriteByMagicResistance 无效的枚举类型！！！");
                    return null;
            }
        }

        public static Sprite GetSpriteByMagicAttributeWeak(EMagicAttributeType _magicAttribute,TBSActorInfo _actorInfo)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
            {
                Debug.LogError("GameCommon GetSpriteByMagicAttributeWeak 无法获得TBSConfigRefObj！！！");
                return null;
            }
            if(_actorInfo.weakAttributeList.Contains(_magicAttribute))
                return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsAttributeWeakSpriteObjName);
            else if (_actorInfo.normalAttributeList.Contains(_magicAttribute))
                return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsAttributeNormalSpriteObjName);
            else if (_actorInfo.resistentAttributeList.Contains(_magicAttribute))
                return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsAttributeResistanceSpriteObjName);
            else if (_actorInfo.invilidAttributeList.Contains(_magicAttribute))
                return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsAttributeInvalidSpriteObjName);
            else if (_actorInfo.bounceAttributeList.Contains(_magicAttribute))
                return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsAttributeBounceSpriteObjName);
            else if (_actorInfo.suckAttributeList.Contains(_magicAttribute))
                return ResourcesHelper.LoadAsset<Sprite>(tbsConfigRefObj.tbsAttributeSuckSpriteObjName);
            else
            {
                Debug.Log("GameCommon GetSpriteByMagicAttributeWeak 该魔法属性找不到关系！！！");
                return null;
            }
        }

        /// <summary>
        /// 展示伤害飘字
        /// </summary>
        public static void ShowDamageFloatText(int _damage,Vector3 _worldPos,string _extraStr)
        {
            GameObject damageGO = ResourcesHelper.LoadGameObject(
                GetUIResObjPath(GameConst.TBS_DAMAGE_NUM_PREFAB), 
                SCGame.instance.topLayerRoot.transform);
            damageGO.GetRectTransform().localPosition = SCUICommon.WorldPointToUIPoint(
                SCGame.instance.topLayerRoot.GetRectTransform(),
                _worldPos);
            damageGO.GetComponent<DamageFloatText>().Initialize(_damage, _extraStr,true);
        }
        /// <summary>
        /// 展示治疗量飘字
        /// </summary>
        public static void ShowHealFloatText(int _healAmount, Vector3 _worldPos, string _extraStr)
        {
            GameObject damageGO = ResourcesHelper.LoadGameObject(
                GetUIResObjPath(GameConst.TBS_DAMAGE_NUM_PREFAB),
                SCGame.instance.topLayerRoot.transform);
            damageGO.GetRectTransform().localPosition = SCUICommon.WorldPointToUIPoint(
                SCGame.instance.topLayerRoot.GetRectTransform(),
                _worldPos);
            damageGO.GetComponent<DamageFloatText>().Initialize(_healAmount, _extraStr,false);
        }

        /// <summary>
        /// 展示攻击状态飘字
        /// </summary>
        /// <param name="_attackState"></param>
        /// <param name="_worldPos"></param>
        public static void ShowAttackStateText(ETBSAttackState _attackState,Vector3 _worldPos)
        {
            GameObject attackStateGO = ResourcesHelper.LoadGameObject(
                GetUIResObjPath(GameConst.TBS_ATTACK_STATE_PREFAB),
                SCGame.instance.topLayerRoot.transform);
            attackStateGO.GetRectTransform().localPosition = SCUICommon.WorldPointToUIPoint(
                SCGame.instance.topLayerRoot.GetRectTransform(),
                _worldPos);
            attackStateGO.GetComponent<AttackStateText>().Initialize(Enum2StrFactory.CreateLocalStrByAttackStateEnum(_attackState));
        }

        public static void ShowCommonTopTip(string _content)
        {
            GameObject tipGO = ResourcesHelper.LoadGameObject(
                GetUIResObjPath(GameConst.COMMON_TIP_PREFAB),
                SCGame.instance.topLayerRoot.transform);
            tipGO.GetRectTransform().localPosition = SCGame.instance.tranTipPoint.localPosition;
            tipGO.GetComponent<TipFloatText>().Initialize(_content);
        }

        public static void ShowTip(string _content,Vector3 _worldPos)
        {
            GameObject tipGO = ResourcesHelper.LoadGameObject(
                GetUIResObjPath(GameConst.COMMON_TIP_PREFAB),
                SCGame.instance.topLayerRoot.transform);
            tipGO.GetRectTransform().localPosition = SCUICommon.WorldPointToUIPoint(SCGame.instance.topLayerRoot.GetRectTransform(), _worldPos);
            tipGO.GetComponent<TipFloatText>().Initialize(_content);
        }
        public static void ShowSkillNameTip(string _content)
        {
            GameObject tipGO = ResourcesHelper.LoadGameObject(
                GetUIResObjPath(GameConst.TBS_SKILL_NAME_TIP),
                SCGame.instance.topLayerRoot.transform);
            tipGO.GetRectTransform().localPosition = SCGame.instance.tranSkillNamePoint.localPosition;
            tipGO.GetComponent<FadeTipText>().Initialize(_content);
        }

        public static string GetCharacterNameWithLv(int _level, string _characterNameKey)
        {
            string characterName = LanguageHelper.instance.GetTextTranslate(_characterNameKey);
            return LanguageHelper.instance.GetTextTranslate("#2_lv_name", _level, characterName);
        }

        /// <summary>
        /// 解析配表效果obj
        /// </summary>
        /// <param name="_str"></param>
        /// <param name="_type"></param>
        /// <returns></returns>
        public static _AEffectObjBase ParseEffectObj(string _str,Type _type)
        {
            if (string.IsNullOrEmpty(_str))
                return null;
            _AEffectObjBase effectObj = null;
            if(_type == typeof(BasicChgEffectObj))
            {
                effectObj = new BasicChgEffectObj();
                effectObj.Deserialize(_str);
            }
            else if(_type == typeof(BuffEffectObj))
            {
                effectObj = new BuffEffectObj();
                effectObj.Deserialize(_str);
            }

            return effectObj;

        }

        /// <summary>
        /// 获得技能描述翻译
        /// </summary>
        /// <param name="_skillId"></param>
        /// <returns></returns>
        public static string GetSkillDescTranslate(long _skillId)
        {
            string resStr = "";
            TBSActorSkillRefObj skillRefObj = SCRefDataMgr.instance.tbsActorSkillRefList.refDataList.Find(x => x.id == _skillId);
            if(skillRefObj == null)
            {
                SCDebugHelper.LogError("找不到id为" + _skillId + "的技能");
                return resStr;
            }
            //根据不同的伤害类型这里要做描述特殊处理
            string damageDescStr = "";
            switch (skillRefObj.damageType)
            {
                case EDamageType.MAGIC:
                    damageDescStr = Enum2StrFactory.CreateLocalStrByMagicAttributeEnum(skillRefObj.magicAttributeType);
                    break;
                case EDamageType.PHYSICAL:
                    damageDescStr = Enum2StrFactory.CreateLocalStrByPhysicalLevelEnum(skillRefObj.physicsLevelType);
                    break;
                default:
                    break;
            }

            resStr = LanguageHelper.instance.GetTextTranslate(
                skillRefObj.skillDesc,
                Enum2StrFactory.CreateLocalStrByDamageTargetEnum(skillRefObj.damageTargetType),
                Enum2StrFactory.CreateLocalStrByDamageAmountEnum(skillRefObj.damageAmountType),
                damageDescStr,
                Enum2StrFactory.CreateLocalStrByDamageEnum(skillRefObj.damageType));

            return resStr;
        }
        public static string GetItemDescTranslate(long _itemId)
        {
            string resStr = "";
            ItemRefObj itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == _itemId);
            if (itemRefObj == null)
            {
                SCDebugHelper.LogError("找不到id为" + _itemId + "的道具");
                return resStr;
            }
            switch (itemRefObj.itemType)
            {
                case EItemType.NONE:
                    break;
                case EItemType.QUEST:
                    break;
                case EItemType.GROW:
                    break;
                case EItemType.BATTLE:
                    {
                        resStr = getBattleItemEffectDesc(itemRefObj,itemRefObj.itemEffectRefObjId, itemRefObj.itemDesc);
                    }
                    break;
            }
            return resStr;
        }


        private static string getBattleItemEffectDesc(ItemRefObj _itemRefObj,long _effectRefObjId,string _translateKey)
        {
            string resStr = "";
            BattleItemEffectRefObj effectRefObj = SCRefDataMgr.instance.battleItemEffectRefList.refDataList.Find(x => x.id == _effectRefObjId);
            if(effectRefObj == null)
            {
                SCDebugHelper.LogError("找不到id为" + _effectRefObjId + "的道具效果obj");
                return resStr;
            }
            switch (effectRefObj.effectType)
            {
                case EBattleItemEffectType.NONE:
                    break;
                case EBattleItemEffectType.BASIC_CHG:
                    {
                        List<BasicChgEffectObj> basicChgEffectList = effectRefObj.basicChgEffectList;
                        object[] translateParams = new object[basicChgEffectList.Count * 2];
                        for(int i =0;i<basicChgEffectList.Count;i++)
                        {
                            translateParams[i * 2] = Enum2StrFactory.CreateLoaclStrByBasicAttributeEnum(basicChgEffectList[i].basicAttribute);
                            translateParams[i * 2 + 1] = basicChgEffectList[i].changeValue;
                        }
                        resStr = LanguageHelper.instance.GetTextTranslate(_translateKey, translateParams);
                    }
                    break;
                case EBattleItemEffectType.BUFF:
                    {
                        List<BuffEffectObj> buffEffectList = effectRefObj.buffEffectList;
                        List<object> translateParamList = new List<object>();
                        TBSBuffRefObj buffRefObj;
                        for (int i = 0; i < buffEffectList.Count; i++)
                        {
                            translateParamList.Add(Enum2StrFactory.CreateLocalStrByDamageTargetEnum(_itemRefObj.itemTargetType));
                            translateParamList.Add(LanguageHelper.instance.GetTextTranslate(_itemRefObj.isPlayerTarget ? "#1_allied" : "#1_hostile"));
                            buffRefObj = SCRefDataMgr.instance.tbsBuffRefList.refDataList.Find(x => x.id == buffEffectList[i].buffRefObjId);

                            switch (buffRefObj.buffType)
                            {
                                case EBuffEffectType.NONE:
                                    break;
                                case EBuffEffectType.ATTRIBUTE_CHG:
                                    translateParamList.Add(Enum2StrFactory.CreateLoaclStrByBasicAttributeEnum(buffRefObj.affectAttribute));
                                    translateParamList.Add(buffEffectList[i].continueTurn);
                                    break;
                                case EBuffEffectType.DAMAGE:
                                    break;
                                case EBuffEffectType.SPECIAL:
                                    break;
                            }
                        }
                        resStr = LanguageHelper.instance.GetTextTranslate(_translateKey, translateParamList.ToArray());
                    }
                    break;
                case EBattleItemEffectType.SPECIAL:
                    resStr = LanguageHelper.instance.GetTextTranslate(_translateKey);

                    break;
            }
            return resStr;
        }
    }
}
