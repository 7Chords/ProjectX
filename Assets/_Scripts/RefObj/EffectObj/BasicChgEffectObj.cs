using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RefData
{
    /// <summary>
    /// 基础属性变化效果RefObj
    /// </summary>
    public class BasicChgEffectObj : _AEffectObjBase
    {

        public EBasicAttribute basicAttribute;
        public float changeValue;
        protected override void OnDeserialize(string _str)
        {
            string[] strArr = _str.Split(':');
            if (strArr == null || strArr.Length < 2)
                return;
            basicAttribute = (EBasicAttribute)SCCommon.ParseEnum(strArr[0], typeof(EBasicAttribute));
            changeValue = SCCommon.ParseFloat(strArr[1]);
        }

        protected override string OnSerialise()
        {
            string str = basicAttribute.ToString() + ":" + changeValue;
            return str;
        }
    }
}
