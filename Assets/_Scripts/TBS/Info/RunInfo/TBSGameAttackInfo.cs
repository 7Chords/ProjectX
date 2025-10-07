using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.TBS
{
    public class TBSGameAttackInfo
    {
        public List<TBSActorBase> srcActorList;
        public List<TBSActorBase> targetActorList;
        public List<int> srcUseHpList;
        public List<int> srcUseMpList;
        public List<int> damageList;
        public EDamageType damageType;
        public EPhysicalLevelType physicsLevelType;
        public EMagicAttributeType magicAttributeType;
        public EDamageCauseType damageCauseType;
    } 
}
