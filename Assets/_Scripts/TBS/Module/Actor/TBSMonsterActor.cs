using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{

    public class TBSMonsterActor : TBSActorBase
    {
        public TBSMonsterActor(TBSActorMonoBase _mono) : base(_mono)
        {
        }

        public override void Attack_All(List<TBSActorBase> _targetList)
        {

        }

        public override void Attack_Single(TBSActorBase _target)
        {
        }

        public override void ReleaseSkill(long skillId, TBSActorBase _target)
        {
        }
    }
}
