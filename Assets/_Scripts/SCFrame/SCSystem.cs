using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    public class SCSystem:Singleton<SCSystem>
    {
        public override void OnInitialize()
        {
            SCMsgCenter.instance.Initialize();
            SCSaveSys.instance.Initialize();
            SCPoolMgr.instance.Initialize();
        }
    }
}
