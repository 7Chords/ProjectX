using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
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
