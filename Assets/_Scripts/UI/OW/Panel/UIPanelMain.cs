using SCFrame;
using SCFrame.UI;
using System;

namespace GameCore.UI
{
    public class UIPanelMain : _ASCUIPanelBase<UIMonoMain>
    {
        public UIPanelMain(UIMonoMain _mono, SCUIShowType _showType) : base(_mono, _showType)
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
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_OPTION_INPUT, onOptionInput);
        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_OPTION_INPUT, onOptionInput);
        }

        private void onOptionInput()
        {
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeOption(SCUIShowType.FULL));
        }
    }

}