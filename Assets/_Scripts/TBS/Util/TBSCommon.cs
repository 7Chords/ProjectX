using GameCore.RefData;
using SCFrame;
using UnityEngine;

namespace GameCore.TBS
{
    public static class TBSCommon
    {

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
                Debug.LogError("TBSCommon GetSpriteByMagicResistance 无法获得TBSConfigRefObj！！！");
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
                    Debug.LogError("TBSCommon GetSpriteByMagicResistance 无效的枚举类型！！！");
                    return null;
            }
        }

        public static Sprite GetSpriteByMagicAttributeWeak(EMagicAttributeType _magicAttribute,TBSActorInfo _actorInfo)
        {
            TBSConfigRefObj tbsConfigRefObj = SCRefDataMgr.instance.tbsConfigRefObj;
            if (tbsConfigRefObj == null)
            {
                Debug.LogError("TBSCommon GetSpriteByMagicAttributeWeak 无法获得TBSConfigRefObj！！！");
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
                Debug.Log("TBSCommon GetSpriteByMagicAttributeWeak 该魔法属性找不到关系！！！");
                return null;
            }
        }
    }
}
