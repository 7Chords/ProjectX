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

        public override void Attack(TBSActorBase _target)
        {
        }

        public override void Defend()
        {
        }

        public override void GetHit()
        {
            throw new System.NotImplementedException();
        }

        public override void ReleaseSkill(long skillId, TBSActorBase _target)
        {
        }
    }
}
