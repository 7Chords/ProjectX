using GameCore;
using UnityEngine;


namespace SCFrame
{
    public class SCPlayer : Singleton<SCPlayer>
    {
        public override void OnInitialize()
        {
            GameCoreMgr.instance.Initialize();
            SCSettingMgr.instance.Initialize();
            SCDataMgr.instance.Initialize();
            //etc
        }

        public override void OnDiscard()
        {
            GameCoreMgr.instance.Discard();
            SCSettingMgr.instance.Discard();
            SCDataMgr.instance.Discard();
        }

        public override void OnResume()
        {
            GameCoreMgr.instance.Resume();
            SCSettingMgr.instance.Resume();
            SCDataMgr.instance.Resume();
        }

        public override void OnSuspend()
        {
            GameCoreMgr.instance.Suspend();
            SCSettingMgr.instance.Suspend();
            SCDataMgr.instance.Suspend();
        }
    }
}
