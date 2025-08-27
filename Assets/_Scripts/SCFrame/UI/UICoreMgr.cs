using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.UI
{
    /// <summary>
    /// UI管理器 队列控制 开启隐藏关闭...
    /// </summary>
    public class UICoreMgr : ACoreMgrBase
    {
        public override ECoreMgrType coreMgrType => ECoreMgrType.UI;

        public override void OnDiscard()
        {
            throw new System.NotImplementedException();
        }

        public override void OnInitialize()
        {
            throw new System.NotImplementedException();
        }

        public override void OnResume()
        {
            throw new System.NotImplementedException();
        }

        public override void OnSuspend()
        {
            throw new System.NotImplementedException();
        }
    }
}
