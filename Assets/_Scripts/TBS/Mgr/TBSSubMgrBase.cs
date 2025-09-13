using SCFrame;

namespace GameCore.TBS
{
    public abstract class TBSSubMgrBase : ASubMgrBase<TBSModuleBase>
    {
        public abstract ETBSSubMgrType tbsSubMgrType { get; }
    }
}
