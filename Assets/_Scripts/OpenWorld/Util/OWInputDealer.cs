using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.OW
{
    public class OWInputDealer : Singleton<OWInputDealer>
    {

        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_CONFIRM_INPUT, onOWConfirmInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_DOWN_INPUT, onOWSwitchToDownInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_UP_INPUT, onOWSwitchToUpInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_LEFT_INPUT, onOWSwitchToLeftInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_RIGHT_INPUT, onOWSwitchToRightInput);

        }
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_CONFIRM_INPUT, onOWConfirmInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_SWITCH_TO_DOWN_INPUT, onOWSwitchToUpInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_SWITCH_TO_LEFT_INPUT, onOWSwitchToLeftInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_SWITCH_TO_RIGHT_INPUT, onOWSwitchToRightInput);

        }
        private void onOWConfirmInput()
        {
            _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.FULL);
            _ASCUINodeBase topAdditionNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.ADDITION);
            if (topFullNode == null)
                return;
            switch(topFullNode.GetNodeName())
            {
                case nameof(UINodeItem):
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.OW_ITEM_CONFIRM);
                    }
                    break;
                case nameof(UINodeStore):
                    {
                        if(topAdditionNode != null && topAdditionNode.GetNodeName() == nameof(UINodeCommonTwoBtn))
                        {
                            SCMsgCenter.SendMsg(SCMsgConst.OW_COMMON_TWO_BTN_CONFIRM);
                        }
                        else
                        {
                            SCMsgCenter.SendMsg(SCMsgConst.OW_STORE_CONFIRM);
                        }
                    }
                    break;
                case nameof(UINodeOption):
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.OW_OPTION_CONFIRM);
                    }
                    break;
                case nameof(UINodeDialogue):
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.OW_DIALOG_CONFIRM);
                    }
                    break;
            }
        }

        private void onOWSwitchToRightInput()
        {
            _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.FULL);
            _ASCUINodeBase topAdditionNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.ADDITION);
            if (topFullNode == null)
                return;
            switch (topFullNode.GetNodeName())
            {
                case nameof(UINodeStore):
                    {
                        if (topAdditionNode != null && topAdditionNode.GetNodeName() == nameof(UINodeCommonTwoBtn))
                        {
                            SCMsgCenter.SendMsg(SCMsgConst.OW_COMMON_TWO_BTN_HIGHLIGHT_RIGHT);
                        }
                    }
                    break;
            }
        }

        private void onOWSwitchToLeftInput()
        {
            _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.FULL);
            _ASCUINodeBase topAdditionNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.ADDITION);
            if (topFullNode == null)
                return;
            switch (topFullNode.GetNodeName())
            {
                case nameof(UINodeStore):
                    {
                        if (topAdditionNode != null && topAdditionNode.GetNodeName() == nameof(UINodeCommonTwoBtn))
                        {
                            SCMsgCenter.SendMsg(SCMsgConst.OW_COMMON_TWO_BTN_HIGHLIGHT_LEFT);
                        }
                    }
                    break;
            }
        }

        private void onOWSwitchToUpInput()
        {
            _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.FULL);
            _ASCUINodeBase topAdditionNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.ADDITION);
            if (topFullNode == null)
                return;
            switch (topFullNode.GetNodeName())
            {
                case nameof(UINodeItem):
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.OW_ITEM_HIGHLIGHT_UP);
                    }
                    break;
                case nameof(UINodeStore):
                    {
                        if (topAdditionNode != null && topAdditionNode.GetNodeName() == nameof(UINodeCommonTwoBtn))
                        {
                            return;
                        }
                        else
                        {
                            SCMsgCenter.SendMsg(SCMsgConst.OW_STORE_HIGHLIGHT_UP);
                        }
                    }
                    break;
                case nameof(UINodeOption):
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.OW_OPTION_HIGHLIGHT_UP);
                    }
                    break;
                case nameof(UINodeCharacter):
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.OW_CHARACTER_HIGHLIGHT_UP);
                    }
                    break;
            }
        }

        private void onOWSwitchToDownInput()
        {
            _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.FULL);
            _ASCUINodeBase topAdditionNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.ADDITION);
            if (topFullNode == null)
                return;
            switch (topFullNode.GetNodeName())
            {
                case nameof(UINodeItem):
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.OW_ITEM_HIGHLIGHT_DOWN);
                    }
                    break;
                case nameof(UINodeStore):
                    {
                        if (topAdditionNode != null && topAdditionNode.GetNodeName() == nameof(UINodeCommonTwoBtn))
                        {
                            return;
                        }
                        else
                        {
                            SCMsgCenter.SendMsg(SCMsgConst.OW_STORE_HIGHLIGHT_DOWN);
                        }
                    }
                    break;
                case nameof(UINodeOption):
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.OW_OPTION_HIGHLIGHT_DOWN);
                    }
                    break;
                case nameof(UINodeCharacter):
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.OW_CHARACTER_HIGHLIGHT_DOWN);
                    }
                    break;
            }
        }

    }

}
