using SCFrame;

namespace GameCore.RefData
{
    public class TBSConfigRefObj : SCRefDataCore
    {
        public TBSConfigRefObj(string _assetPath, string _objName) : base(_assetPath, _objName)
        {
        }


        public float tbsCrticalMultiplier;
        public float tbsMagicWeakMultiplier;
        public float tbsMagicNormalMultiplier;
        public float tbsMagicResistMultiplier;
        public float tbsPhysicalPenetrateSameMultiplier;
        public float tbsPhysicalPenetrateHigherOneMultiplier;
        public float tbsPhysicalPenetrateHigherTwoMultiplier;
        public float tbsPhysicalPenetrateHigherThreeMultiplier;
        public float tbsPhysicalPenetrateLowerOneMultiplier;
        public float tbsPhysicalPenetrateLowerTwoMultiplier;
        public float tbsPhysicalPenetrateLowerThreeMultiplier;
        public float tbsMagicResistanceLightMultiplier;
        public float tbsMagicResistanceMediumMultiplier;
        public float tbsMagicResistanceHeavyMultiplier;
        public float tbsMagicResistanceHeroMultiplier;
        public string tbsRomanNumberOneSpriteObjName;
        public string tbsRomanNumberTwoSpriteObjName;
        public string tbsRomanNumberThreeSpriteObjName;
        public string tbsAttributeNormalSpriteObjName;
        public string tbsAttributeWeakSpriteObjName;
        public string tbsAttributeResistanceSpriteObjName;
        public string tbsAttributeInvalidSpriteObjName;
        public string tbsAttributeBounceSpriteObjName;
        public string tbsAttributeSuckSpriteObjName;
        public string tbsAttributeUnknownSpriteObjName;
        public string tbsResistanceHeroSpriteObjName;
        public float tbsLittleAmountMultiplier;
        public float tbsMiddleAmountMultiplier;
        public float tbsLargeAmountMultiplier;
        public float tbsSuperAmountMultiplier;

        protected override void _parseFromString()
        {
            tbsCrticalMultiplier = getFloat("tbsCrticalMultiplier");
            tbsMagicWeakMultiplier = getFloat("tbsMagicWeakMultiplier");
            tbsMagicResistMultiplier = getFloat("tbsMagicResistMultiplier");
            tbsMagicNormalMultiplier = getFloat("tbsMagicNormalMultiplier");
            tbsPhysicalPenetrateSameMultiplier = getFloat("tbsPhysicalPenetrateSameMultiplier");
            tbsPhysicalPenetrateHigherOneMultiplier = getFloat("tbsPhysicalPenetrateHigherOneMultiplier");
            tbsPhysicalPenetrateHigherTwoMultiplier = getFloat("tbsPhysicalPenetrateHigherTwoMultiplier");
            tbsPhysicalPenetrateHigherThreeMultiplier = getFloat("tbsPhysicalPenetrateHigherThreeMultiplier");
            tbsPhysicalPenetrateLowerOneMultiplier = getFloat("tbsPhysicalPenetrateLowerOneMultiplier");
            tbsPhysicalPenetrateLowerTwoMultiplier = getFloat("tbsPhysicalPenetrateLowerTwoMultiplier");
            tbsPhysicalPenetrateLowerThreeMultiplier = getFloat("tbsPhysicalPenetrateLowerThreeMultiplier");
            tbsMagicResistanceLightMultiplier = getFloat("tbsMagicResistanceLightMultiplier");
            tbsMagicResistanceMediumMultiplier = getFloat("tbsMagicResistanceMediumMultiplier");
            tbsMagicResistanceHeavyMultiplier = getFloat("tbsMagicResistanceHeavyMultiplier");
            tbsMagicResistanceHeroMultiplier = getFloat("tbsMagicResistanceHeroMultiplier");
            tbsRomanNumberOneSpriteObjName = getString("tbsRomanNumberOneSpriteObjName");
            tbsRomanNumberTwoSpriteObjName = getString("tbsRomanNumberTwoSpriteObjName");
            tbsRomanNumberThreeSpriteObjName = getString("tbsRomanNumberThreeSpriteObjName");
            tbsAttributeNormalSpriteObjName = getString("tbsAttributeNormalSpriteObjName");
            tbsAttributeWeakSpriteObjName = getString("tbsAttributeWeakSpriteObjName");
            tbsAttributeResistanceSpriteObjName = getString("tbsAttributeResistanceSpriteObjName");
            tbsAttributeInvalidSpriteObjName = getString("tbsAttributeInvalidSpriteObjName");
            tbsAttributeBounceSpriteObjName = getString("tbsAttributeBounceSpriteObjName");
            tbsAttributeSuckSpriteObjName = getString("tbsAttributeSuckSpriteObjName");
            tbsAttributeUnknownSpriteObjName = getString("tbsAttributeUnknownSpriteObjName");
            tbsResistanceHeroSpriteObjName = getString("tbsResistanceHeroSpriteObjName");
            tbsLittleAmountMultiplier = getFloat("tbsLittleAmountMultiplier");
            tbsMiddleAmountMultiplier = getFloat("tbsMiddleAmountMultiplier");
            tbsLargeAmountMultiplier = getFloat("tbsLargeAmountMultiplier");
            tbsSuperAmountMultiplier = getFloat("tbsSuperAmountMultiplier");

        }

        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "tbs_config";
    }
}
