using GameCore.TBS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RefData
{
    public class TBSGameBuffInfo
    {
        public TBSBuffRefObj buffRefObj;
        public EBuffStackType buffStackType;
        public int remainTurnCount;

        public TBSGameBuffInfo(TBSBuffRefObj buffRefObj, EBuffStackType buffStackType, int remainTurnCount)
        {
            this.buffRefObj = buffRefObj;
            this.buffStackType = buffStackType;
            this.remainTurnCount = remainTurnCount;
        }
    }
}
