using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSItemComp : TBSCompBase
    {
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ITEM_INPUT, onTBSItemInput);
            SCMsgCenter.RegisterMsg(SCMsgConst.UI_NODE_CHG, onUINodeChg);

        }

        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ITEM_INPUT, onTBSItemInput);
            SCMsgCenter.UnregisterMsg(SCMsgConst.UI_NODE_CHG, onUINodeChg);

        }

        private void onTBSItemInput()
        {
            _ASCUINodeBase node = GameCoreMgr.instance.uiCoreMgr.GetNodeByName(nameof(UINodeTBSMain));
            if (node == null || node.hasHideNode)
                return;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSItem(SCFrame.UI.SCUIShowType.FULL));
        }

        private void onUINodeChg(object[] _objs)
        {
            if (_objs == null || _objs.Length < 2)
                return;
            _ASCUINodeBase firstNode = _objs[0] as _ASCUINodeBase;
            _ASCUINodeBase secondNode = _objs[1] as _ASCUINodeBase;

            if (firstNode == null || secondNode == null)
                return;
            if ((firstNode is UINodeTBSMain) && (secondNode is UINodeTBSItem))
            {
                GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(SCModel.instance.tbsModel.GetCurActor().GetOpenSkillCameraPos(), true);
            }
            else if ((firstNode is UINodeTBSItem) && (secondNode is UINodeTBSMain))
            {
                GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(SCModel.instance.tbsModel.GetCurActor().GetActorCameraTran().position, true);
            }

            if (firstNode is UINodeTBSItem)
            {

                GameCoreMgr.instance.uiCoreMgr.ShowNodeButNotMove2Top(nameof(UINodeTBSEnemyHud));

                //重新设置光标
                List<Vector3> worldPosList = new List<Vector3>();
                if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                    worldPosList.Add(SCModel.instance.tbsModel.GetCurSelectSingleEnemyTargetActor().GetCursorPos());
                else if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
                {
                    worldPosList = SCModel.instance.tbsModel.GetPosList(false, ETargetAliveType.ALIVE);
                }
                TBSCursorMgr.instance.SetSelectionCursor(worldPosList);
            }
            else if (secondNode is UINodeTBSItem)
            {
                //隐藏敌人hud
                GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));

                //隐藏光标
                TBSCursorMgr.instance.HideSelectionCursor();
            }
        }
    }
}
