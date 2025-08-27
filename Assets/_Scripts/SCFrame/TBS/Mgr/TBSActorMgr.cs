using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.TBS
{
    public class TBSActorMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.ACTOR;

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
