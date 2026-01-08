using GameCore.RefData;
using System;

namespace GameCore.TBS
{
    public class TBSGameBuffInfo
    {
        public TBSActorBase targetActor;
        public TBSBuffRefObj buffRefObj;
        public int remainTurnCount;
        public int totalTurnCount;
        public Action onBuffAdd;
        public Action onBuffRemove;
        public Action onTurnTick;
        public Action onAttack;
        public Action onGetHit;
        public Action onActorDie;
        public Action onActorAction;
        public TBSGameBuffInfo(TBSActorBase _actor,TBSBuffRefObj buffRefObj, int remainTurnCount)
        {
            targetActor = _actor;
            this.buffRefObj = buffRefObj;
            this.remainTurnCount = remainTurnCount;
            this.totalTurnCount = remainTurnCount;
        }
    }
}
