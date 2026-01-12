using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeTBSDetail : _ASCUINodeBase
    {
        public UINodeTBSDetail(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => true;

        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;

        public override bool ignoreOnUIList => false;

        public override string GetNodeName()
        {
            throw new System.NotImplementedException();
        }

        public override string GetResName()
        {
            throw new System.NotImplementedException();
        }

        public override void OnEnterNode()
        {
            throw new System.NotImplementedException();
        }

        public override void OnHideNode()
        {
            throw new System.NotImplementedException();
        }

        public override void OnQuitNode()
        {
            throw new System.NotImplementedException();
        }

        public override void OnShowNode()
        {
            throw new System.NotImplementedException();
        }
    }
}
