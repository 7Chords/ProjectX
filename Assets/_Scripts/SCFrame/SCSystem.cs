using GameCore;
using GameCore.TBS;

namespace SCFrame
{
    public class SCSystem:Singleton<SCSystem>
    {
        public override void OnInitialize()
        {
            SCRefDataMgr.instance.Initialize();
            SCMsgCenter.instance.Initialize();
            SCSaveSys.instance.Initialize();
            LanguageHelper.instance.Initialize();
            SCInputListener.instance.Initialize();
            SCPoolMgr.instance.Initialize();
            GameCameraMgr.instance.Initialize();
            TBSCursorMgr.instance.Initialize();
        }

        public override void OnDiscard()
        {
            SCRefDataMgr.instance.Discard();
            SCMsgCenter.instance.Discard();
            SCSaveSys.instance.Discard();
            LanguageHelper.instance.Discard();
            SCInputListener.instance.Discard();
            SCPoolMgr.instance.Discard();
            TBSCursorMgr.instance.Discard();
            GameCameraMgr.instance.Discard();
        }
    }
}
