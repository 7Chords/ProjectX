using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.RefData
{
    public class StoreItemEffectObj : _AEffectObjBase
    {
        public long itemId;
        public int itemAmount;
        protected override void OnDeserialize(string _str)
        {
            string[] strArr = _str.Split(":");
            if (strArr == null || strArr.Length < 2)
                return;
            itemId = SCCommon.ParseLong(strArr[0]);
            itemAmount = SCCommon.ParseInt(strArr[1]);
        }

        protected override string OnSerialise()
        {
            string str = itemId + ":" + itemAmount;
            return str;
        }
    }
}
