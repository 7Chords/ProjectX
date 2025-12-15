using GameCore.TBS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RefData
{
    public class TBSGameBuffInfo
    {
        public TBSBuffRefObj buffRefObj;
        public int remainTurnCount;

        public TBSGameBuffInfo(TBSBuffRefObj buffRefObj, int remainTurnCount)
        {
            this.buffRefObj = buffRefObj;
            this.remainTurnCount = remainTurnCount;
        }
    }
}
