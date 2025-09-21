using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    public class SCSystem:Singleton<SCSystem>
    {
        public override void OnInitialize()
        {
            SCRefDataMgr.instance.Initialize();
            SCMsgCenter.instance.Initialize();
            SCSaveSys.instance.Initialize();
            SCPoolMgr.instance.Initialize();
            GameCameraMgr.instance.Initialize();
        }
    }
}
