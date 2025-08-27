using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.TBS
{
    public abstract class TBSSubMgrBase : ASubMgrBase<TBSModuleBase>
    {
        public abstract ETBSSubMgrType tbsSubMgrType { get; }
    }
}
