using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSDetailHeaderContainer : UIPanelContainerBase<UIMonoCommonContainer, UIPanelTBSDetailHeaderContainerItem, UIMonoTBSDetailHeaderContainerItem>
    {
        public UIPanelTBSDetailHeaderContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
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

        protected override GameObject creatItemGO()
        {
            throw new System.NotImplementedException();
        }

        protected override UIPanelTBSDetailHeaderContainerItem creatItemPanel(UIMonoTBSDetailHeaderContainerItem _mono)
        {
            throw new System.NotImplementedException();
        }
    }
}
