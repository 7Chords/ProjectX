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

        }

        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "tbs_config";
    }
}
