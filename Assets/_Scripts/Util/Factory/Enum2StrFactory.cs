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
    }
}
