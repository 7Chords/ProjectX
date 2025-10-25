using GameCore.TBS;
using SCFrame;
using UnityEngine;

namespace GameCore.RefData
{
    public class TBSActorSkillRefObj : SCRefDataCore
    {

        public TBSActorSkillRefObj()
        {

        }
        public TBSActorSkillRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {
        }
        public long id;
        public string skillName;
        public string skillIconObjName;
        public int skillNeedMp;
        public int skillNeedHp;
        public string skillDesc;
        public ETargetType damageTargetType;
        public EDamageType damageType;
        public EDamageAmountType damageAmountType;
        public EPhysicalLevelType physicsLevelType;
        public EMagicAttributeType magicAttributeType;
        public string skillPlayableAssetName;
        public bool needMove;
        protected override void _parseFromString()
        {
            id = getLong("id");
            skillName = getString("skillName");
            skillIconObjName = getString("skillIconObjName");
            skillNeedMp = getInt("skillNeedMp");
            skillNeedHp = getInt("skillNeedHp");
            skillDesc = getString("skillDesc");
            damageTargetType = (ETargetType)getEnum("damageTargetType", typeof(ETargetType));
            damageType = (EDamageType)getEnum("damageType", typeof(EDamageType));
            damageAmountType = (EDamageAmountType)getEnum("damageAmountType", typeof(EDamageAmountType));
            physicsLevelType = (EPhysicalLevelType)getEnum("physicsLevelType", typeof(EPhysicalLevelType));
            magicAttributeType = (EMagicAttributeType)getEnum("magicAttributeType", typeof(EMagicAttributeType));
            skillPlayableAssetName = getString("skillPlayableAssetName");
            needMove = getBool("needMove");
        }

        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "tbs_actor_skill";
    }

}
