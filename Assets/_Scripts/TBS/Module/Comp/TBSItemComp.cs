using GameCore.UI;
using SCFrame;
using SCFrame.UI;


namespace GameCore.TBS
{
    public class TBSItemComp : TBSCompBase
    {
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ITEM_INPUT, onTBSItemInput);
        }

        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ITEM_INPUT, onTBSItemInput);
        }

        private void onTBSItemInput()
        {
            _ASCUINodeBase node = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSMain));
            if (node == null || node.hasHideNode)
                return;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSItem(SCFrame.UI.SCUIShowType.FULL));
        }

    }
}
