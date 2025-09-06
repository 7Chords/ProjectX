using SCFrame;
using SCFrame.TBS;

namespace GameCore.TBS
{
    public abstract class TBSSubMgrBase : ASubMgrBase<TBSModuleBase>
    {
        public abstract ETBSSubMgrType tbsSubMgrType { get; }
    }
}
