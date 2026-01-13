using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.TBS
{
    public class TBSDetailComp : TBSCompBase
    {
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_DETAIL_INPUT, onTBSDetailInput);
            SCMsgCenter.RegisterMsg(SCMsgConst.UI_NODE_CHG, onUINodeChg);

        }

        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_DETAIL_INPUT, onTBSDetailInput);
            SCMsgCenter.UnregisterMsg(SCMsgConst.UI_NODE_CHG, onUINodeChg);

        }

        public override void OnSuspend()
        {

        }

        public override void OnResume()
        {

        }

        private void onTBSDetailInput()
        {
            _ASCUINodeBase topShowNode = GameCoreMgr.instance.uiCoreMgr.GetTopShowNode(SCUIShowType.FULL);
            if(topShowNode != null && topShowNode.GetNodeName() == nameof(UINodeTBSMain))
            {
                GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSDetail(SCUIShowType.FULL));
            }
        }
        private void onUINodeChg(object[] _objs)
        {
            if (_objs == null || _objs.Length < 2)
                return;
            _ASCUINodeBase firstNode = _objs[0] as _ASCUINodeBase;
            _ASCUINodeBase secondNode = _objs[1] as _ASCUINodeBase;

            if (firstNode == null || secondNode == null)
                return;
            
            if ((firstNode is UINodeTBSMain) && (secondNode is UINodeTBSDetail))
            {
                GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
                TBSCursorMgr.instance.HideSelectionCursor();
            }
            else if ((firstNode is UINodeTBSDetail) && (secondNode is UINodeTBSMain))
            {
                GameCoreMgr.instance.uiCoreMgr.ShowNodeButNotMove2Top(nameof(UINodeTBSEnemyHud));
                TBSCursorMgr.instance.ShowSelectionCursor();
            }
        }
    }
}
