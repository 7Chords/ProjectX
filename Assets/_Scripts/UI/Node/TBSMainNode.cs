using SCFrame;
using SCFrame.UI;

namespace GameCore.UI
{
    public class TBSMainNode : _ASCUINodeBase
    {
        public TBSMainNode(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool canQuitByEsc => throw new System.NotImplementedException();

        public override bool canQuitByMouseRight => throw new System.NotImplementedException();

        public override string GetNodeName()
        {
            return nameof(TBSMainNode);
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
