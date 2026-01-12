using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSDetail : _ASCUIPanelBase<UIMonoTBSDetail>
    {
        private UIPanelTBSDetailProps _m_panelDetailProps;
        private UIPanelTBSDetailHeaderContainer _m_detailHeaderContainer;
        private UIPanelTBSDetailBuffContainer _m_detailBuffContainer;

        public UIPanelTBSDetail(UIMonoTBSDetail _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            _m_panelDetailProps = new UIPanelTBSDetailProps(mono.monoDetailPorps,SCUIShowType.INTERNAL);
            _m_panelDetailProps.Initialize();
            _m_detailHeaderContainer = new UIPanelTBSDetailHeaderContainer(mono.monoHeaderContainer);
            _m_detailHeaderContainer.Initialize();
            _m_detailBuffContainer = new UIPanelTBSDetailBuffContainer(mono.monoBuffContainer);
            _m_detailBuffContainer.Initialize();
        }

        public override void BeforeDiscard()
        {
            _m_panelDetailProps?.Discard();
            _m_detailHeaderContainer?.Discard();
            _m_detailBuffContainer?.Discard();
        }

        public override void OnHidePanel()
        {
            _m_panelDetailProps?.HidePanel();
            _m_detailHeaderContainer?.HidePanel();
            _m_detailBuffContainer?.HidePanel();
        }

        public override void OnShowPanel()
        {
        }
    }
}
