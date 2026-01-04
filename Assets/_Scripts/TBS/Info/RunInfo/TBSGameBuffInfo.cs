using GameCore.RefData;
using System;

namespace GameCore.TBS
{
    public class TBSGameBuffInfo
    {
        public TBSBuffRefObj buffRefObj;
        public int remainTurnCount;
        public int totalTurnCount;
        public Action onBuffAdd;
        public Action onBuffRemove;
        public Action onTurnTick;
        public Action onAttack;
        public Action onGetHit;
        public Action onActorDie;

        public TBSGameBuffInfo(TBSBuffRefObj buffRefObj, int remainTurnCount)
        {
            this.buffRefObj = buffRefObj;
            this.remainTurnCount = remainTurnCount;
            this.totalTurnCount = remainTurnCount;
        }
    }
}
