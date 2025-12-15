using GameCore.TBS;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Util
{
    public static class Enum2StrFactory
    {
        public static string CreateLocalStrByDamageEnum(EDamageType _damageType)
        {
            string translateKey = "#1_";
            switch (_damageType)
            {
                case EDamageType.MAGIC:
                    translateKey += "magic";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EDamageType.PHYSICAL:
                    translateKey += "physical";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EDamageType.REAL:
                    translateKey += "real";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                default:
                    return "invalid enum";
            }
        }

        public static string CreateLocalStrByDamageAmountEnum(EDamageAmountType _damageAmountType)
        {
            string translateKey = "#1_";
            switch (_damageAmountType)
            {
                case EDamageAmountType.LITTLE:
                    translateKey += "little";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EDamageAmountType.MIDDLE:
                    translateKey += "middle";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EDamageAmountType.LARGE:
                    translateKey += "large";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                default:
                    return "invalid enum";
            }
        }

        public static string CreateLocalStrByDamageTargetEnum(ETargetType _damageTargetType)
        {
            string translateKey = "#1_";
            switch (_damageTargetType)
            {
                case ETargetType.SINGLE:
                    translateKey += "single";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case ETargetType.ALL:
                    translateKey += "all";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                default:
                    return "invalid enum";
            }
        }

        public static string CreateLocalStrByPhysicalLevelEnum(EPhysicalLevelType _physicalLevelType)
        {
            string translateKey = "#1_";
            switch (_physicalLevelType)
            {
                case EPhysicalLevelType.LIGHT:
                    translateKey += "light";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EPhysicalLevelType.MEDIUM:
                    translateKey += "medium";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EPhysicalLevelType.HEAVY:
                    translateKey += "heavy";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EPhysicalLevelType.HERO:
                    translateKey += "hero";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                default:
                    return "invalid enum";
            }
        }

        public static string CreateLocalStrByMagicAttributeEnum(EMagicAttributeType _magicAttributeType)
        {
            string translateKey = "#1_";
            switch (_magicAttributeType)
            {
                case EMagicAttributeType.FIRE:
                    translateKey += "fire";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EMagicAttributeType.WATER:
                    translateKey += "water";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EMagicAttributeType.WOOD:
                    translateKey += "wood";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                default:
                    return "invalid enum";
            }
        }

        public static string CreateLocalStrByAttackStateEnum(ETBSAttackState _attackState)
        {
            string translateKey = "#1_";
            switch (_attackState)
            {
                case ETBSAttackState.BOUNCE:
                    translateKey += "bounce";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case ETBSAttackState.INVALID:
                    translateKey += "invalid";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case ETBSAttackState.MISS:
                    translateKey += "miss";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case ETBSAttackState.SUCK:
                    translateKey += "suck";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case ETBSAttackState.NORMAL:
                    return "";
                default:
                    return "invalid enum";
            }
        }

        public static string CreateLoaclStrByBasicAttributeEnum(EBasicAttribute _basicAttributeType)
        {
            string translateKey = "#1_";
            switch (_basicAttributeType)
            {
                case EBasicAttribute.HP:
                    translateKey += "hp";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EBasicAttribute.MP:
                    translateKey += "mp";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EBasicAttribute.ATTACK:
                    translateKey += "attack_value";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EBasicAttribute.DEFEND:
                    translateKey += "defend_value";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EBasicAttribute.MISS:
                    translateKey += "miss_chance";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EBasicAttribute.CRITICAL_CHANCE:
                    translateKey += "critical_chance";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EBasicAttribute.ARMOR:
                    translateKey += "armor_level";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EBasicAttribute.MAGIC_RESISTENCE:
                    translateKey += "magic_resistence_level";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EBasicAttribute.PHYSICAL_LEVEL:
                    translateKey += "physical_level";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                case EBasicAttribute.MAGIC_ATTRIBUTE:
                    translateKey += "magic_attribute";
                    return LanguageHelper.instance.GetTextTranslate(translateKey);
                default:
                    return "invalid enum";
            }
        }
    }
}
