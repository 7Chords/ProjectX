using GameCore.TBS;
using SCFrame;
using System.Collections.Generic;
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
        public bool isPlayerTarget;
        public int skillNeedMp;
        public int skillNeedHp;
        public string skillDesc;
        public ESkillEffectType skillEffectType;
        public ETargetType damageTargetType;
        public ETargetAliveType targetAliveType;
        public EDamageType damageType;
        public EDamageAmountType damageAmountType;
        public EPhysicalLevelType physicsLevelType;
        public EMagicAttributeType magicAttributeType;
        public List<BuffEffectObj> buffEffectList;
        public string skillPlayableAssetName;
        protected override void _parseFromString()
        {
            id = getLong("id");
            skillName = getString("skillName");
            skillIconObjName = getString("skillIconObjName");
            isPlayerTarget = getBool("isPlayerTarget");
            skillNeedMp = getInt("skillNeedMp");
            skillNeedHp = getInt("skillNeedHp");
            skillDesc = getString("skillDesc");
            skillEffectType = (ESkillEffectType)getEnum("skillEffectType", typeof(ESkillEffectType));
            damageTargetType = (ETargetType)getEnum("damageTargetType", typeof(ETargetType));
            targetAliveType = (ETargetAliveType)getEnum("targetAliveType", typeof(ETargetAliveType));
            damageType = (EDamageType)getEnum("damageType", typeof(EDamageType));
            damageAmountType = (EDamageAmountType)getEnum("damageAmountType", typeof(EDamageAmountType));
            physicsLevelType = (EPhysicalLevelType)getEnum("physicsLevelType", typeof(EPhysicalLevelType));
            magicAttributeType = (EMagicAttributeType)getEnum("magicAttributeType", typeof(EMagicAttributeType));
            buffEffectList = getList<BuffEffectObj>("buffEffectList");
            skillPlayableAssetName = getString("skillPlayableAssetName");
        }
        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "tbs_actor_skill";
    }

}
