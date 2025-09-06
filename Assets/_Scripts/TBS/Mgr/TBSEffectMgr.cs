using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
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
