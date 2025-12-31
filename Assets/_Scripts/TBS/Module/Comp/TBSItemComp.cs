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
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_CONFIRM_INPUT, onTBSConfirmInput);

        }

        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ITEM_INPUT, onTBSItemInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_CONFIRM_INPUT, onTBSConfirmInput);

        }

        private void onTBSItemInput()
        {
            _ASCUINodeBase node = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSMain));
            if (node == null || node.hasHideNode)
                return;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSItem(SCFrame.UI.SCUIShowType.FULL));
        }
        private void onTBSConfirmInput()
        {
            _ASCUINodeBase topNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
            if (topNode == null || topNode.hasHideNode)
                return;
            switch (topNode.GetNodeName())
            {
                case nameof(UINodeTBSItem):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ITEM_CONFIRM);
                    break;
                default:
                    break;

            }
        }
    }
}
