using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSCharacterActionContainer : UIPanelContainerBase<UIMonoTBSCharacterActionContainer, UIPanelTBSCharacterActionItem, UIMonoTBSCharacterActionItem>
    {
        public UIPanelTBSCharacterActionContainer(UIMonoTBSCharacterActionContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }

        public override void OnDiscard()
        {
            throw new System.NotImplementedException();
        }

        public override void OnHidePanel()
        {
            throw new System.NotImplementedException();
        }

        public override void OnInitialize()
        {
            throw new System.NotImplementedException();
        }

        public override void OnShowPanel()
        {
            throw new System.NotImplementedException();
        }

        protected override GameObject creatItemGO()
        {
            throw new System.NotImplementedException();
        }

        protected override UIPanelTBSCharacterActionItem creatItemPanel(UIMonoTBSCharacterActionItem _mono)
        {
            throw new System.NotImplementedException();
        }
    }
}
