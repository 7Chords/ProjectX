using GameCore.TBS;
using SCFrame.UI;

namespace GameCore.UI
{
    public class UIPanelTBSCharacterActionItem : _ASCUIPanelBase<UIMonoTBSCharacterActionItem>
    {
        private TBSActorInfo _m_actorInfo;
        public UIPanelTBSCharacterActionItem(UIMonoTBSCharacterActionItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void OnInitialize()
        {
        }
        public override void OnDiscard()
        {
        }

        public override void OnShowPanel()
        {
        }
        public override void OnHidePanel()
        {
        }

        public void SetInfo(TBSActorInfo _actorInfo)
        {
            if (_actorInfo == null)
                return;
            _m_actorInfo = _actorInfo;
        }
    }
}
