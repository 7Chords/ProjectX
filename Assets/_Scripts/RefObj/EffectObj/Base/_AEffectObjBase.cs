using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RefData
{
    public abstract class _AEffectObjBase
    {

        public virtual string Serialise()
        {
            return OnSerialise();
        }

        public virtual void Deserialize(string _str)
        {
            OnDeserialize(_str);
        }
        protected abstract string OnSerialise();
        protected abstract void OnDeserialize(string _str);
    }
}
