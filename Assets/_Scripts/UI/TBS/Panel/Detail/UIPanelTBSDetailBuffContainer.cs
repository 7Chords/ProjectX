using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSDetailBuffContainer : UIPanelContainerBase<UIMonoCommonContainer, UIPanelTBSDetailBuffContainerItem, UIMonoTBSDetailBuffContainerItem>
    {
        public UIPanelTBSDetailBuffContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
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
            return ResourcesHelper.LoadGameObject(mono.prefabItemObjName);
        }

        protected override UIPanelTBSDetailBuffContainerItem creatItemPanel(UIMonoTBSDetailBuffContainerItem _mono)
        {
            return new UIPanelTBSDetailBuffContainerItem(_mono, SCUIShowType.INTERNAL);
        }
    }
}
