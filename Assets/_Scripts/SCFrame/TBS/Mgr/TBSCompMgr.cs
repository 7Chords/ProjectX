using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.TBS
{
    public class TBSCompMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.COMP;

        public override void OnDiscard()
        {

        }

        public override void OnInitialize()
        {

        }

        public override void OnResume() { }

        public override void OnSuspend() { }
    }
}
