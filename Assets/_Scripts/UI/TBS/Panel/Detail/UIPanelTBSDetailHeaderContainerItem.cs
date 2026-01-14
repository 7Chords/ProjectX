using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSDetailHeaderContainerItem : _ASCUIPanelBase<UIMonoTBSDetailHeaderContainerItem>
    {
        private TBSActorInfo _m_actorInfo;

        private bool _m_hasSelected;
        public UIPanelTBSDetailHeaderContainerItem(UIMonoTBSDetailHeaderContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
        }

        public override void BeforeDiscard()
        {
        }

        public override void OnHidePanel()
        {
        }

        public override void OnShowPanel()
        {
        }

        public void SetInfo(TBSActorInfo _info)
        {
            _m_actorInfo = _info;
            refreshShow();
        }

        private void refreshShow()
        {
            if (_m_actorInfo == null)
                return;
            refreshSelectShow();
            mono.imgHeadIcon.sprite = ResourcesHelper.LoadAsset<Sprite>(_m_actorInfo.characterRefObj.assetHeadIconObjName);

        }

        private void refreshSelectShow()
        {
            SCCommon.SetGameObjectEnable(mono.goSelectShowList, _m_hasSelected);
        }

        public void SetSelect(bool _isSelect)
        {
            _m_hasSelected = _isSelect;
            refreshSelectShow();
        }
    }
}
