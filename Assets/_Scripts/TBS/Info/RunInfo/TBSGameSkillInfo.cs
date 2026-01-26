using GameCore.RefData;
using System.Collections.Generic;

namespace GameCore.TBS
{
    public class TBSGameSkillInfo
    {
        public List<TBSActorBase> srcActorList;
        public List<TBSActorBase> targetActorList;
        public List<int> srcUseHpList;
        public List<int> srcUseMpList;
        public int baseDamage;
        public ESkillEffectType skillEffectType;
        public EDamageType damageType;
        public EDamageAmountType damageAmountType;
        public EPhysicalLevelType physicsLevelType;
        public EMagicAttributeType magicAttributeType;
        public EDamageCauseType damageCauseType;
        public List<BuffEffectObj> buffEffectList;

    }
}
