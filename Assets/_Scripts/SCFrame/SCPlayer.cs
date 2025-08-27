using UnityEngine;


namespace SCFrame
{
    public class SCPlayer : Singleton<SCPlayer>
    {
        private GameCoreMgr _m_gameCoreMgr;
        public override void OnInitialize()
        {
            _m_gameCoreMgr = new GameCoreMgr();
            _m_gameCoreMgr.Initialize();

            //etc
        }

        public override void OnDiscard()
        {
            if (_m_gameCoreMgr != null)
                _m_gameCoreMgr.Discard();
        }

        public override void OnResume()
        {
            if (_m_gameCoreMgr != null)
                _m_gameCoreMgr.Resume();
        }

        public override void OnSuspend()
        {
            if (_m_gameCoreMgr != null)
                _m_gameCoreMgr.Suspend();
        }
    }
}
