using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.TBS
{
    public class TBSEventMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.EFFECT;

        public override void OnInitialize()
        {

        }
        public override void OnDiscard()
        {

        }
        public override void OnResume() { }

        public override void OnSuspend() { }
    }
}
