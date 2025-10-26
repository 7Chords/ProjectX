using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSGameSkillInfo
    {
        public List<TBSActorBase> srcActorList;
        public List<TBSActorBase> targetActorList;
        public List<int> srcUseHpList;
        public List<int> srcUseMpList;
        public int baseDamage;
        public EDamageType damageType;
        public EDamageAmountType damageAmountType;
        public EPhysicalLevelType physicsLevelType;
        public EMagicAttributeType magicAttributeType;
        public EDamageCauseType damageCauseType;
        
    }
}
