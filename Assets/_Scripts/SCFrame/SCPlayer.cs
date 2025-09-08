using UnityEngine;


namespace SCFrame
{
    public class SCPlayer : Singleton<SCPlayer>
    {
        public override void OnInitialize()
        {
            GameCoreMgr.instance.Initialize();

            //etc
        }

        public override void OnDiscard()
        {
            GameCoreMgr.instance.Discard();
        }

        public override void OnResume()
        {
            GameCoreMgr.instance.Resume();
        }

        public override void OnSuspend()
        {
            GameCoreMgr.instance.Suspend();
        }
    }
}
