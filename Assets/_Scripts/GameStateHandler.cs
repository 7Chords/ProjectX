using GameCore.OW;
using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System;
using UnityEngine;

namespace GameCore
{
    public class GameStateHandler:Singleton<GameStateHandler>
    {
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_GAME_START,onTBSGameStart);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_GAME_FINISH, onTBSGameFinish);
            SCMsgCenter.RegisterMsg(SCMsgConst.UI_NODE_CHG, onUINodeChg);
        }

        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_GAME_START, onTBSGameStart);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_GAME_FINISH, onTBSGameFinish);
            SCMsgCenter.UnregisterMsg(SCMsgConst.UI_NODE_CHG, onUINodeChg);

        }

        private void onTBSGameFinish()
        {
            SCModel.instance.gameStateType = EGameStateType.TBS;
        }

        private void onTBSGameStart()
        {
            SCModel.instance.gameStateType = EGameStateType.OW;
        }
        private void onUINodeChg(object[] _objs)
        {
            if (_objs == null || _objs.Length < 2)
                return;
            _ASCUINodeBase firstNode = _objs[0] as _ASCUINodeBase;
            _ASCUINodeBase secondNode = _objs[1] as _ASCUINodeBase;

            if (firstNode == null || secondNode == null)
                return;
            if(firstNode is UINodeMain && secondNode is UINodeOption)
            {
                SCModel.instance.gameStateType = EGameStateType.PAUSE;
                Cursor.visible = true;
                SCGame.instance.owCamera.m_YAxis.m_MaxSpeed = 0;
                SCGame.instance.owCamera.m_XAxis.m_MaxSpeed = 0;
                PlayerController.instance.SetCanControl(false);
                PlayerController.instance.ChangeState(PlayerStateType.IDLE);

            }
            else if(firstNode is UINodeOption && secondNode is UINodeMain)
            {
                SCModel.instance.gameStateType = EGameStateType.OW;

                Cursor.visible = false;
                SCGame.instance.owCamera.m_YAxis.m_MaxSpeed = 3;
                SCGame.instance.owCamera.m_XAxis.m_MaxSpeed = 400;
                PlayerController.instance.SetCanControl(true);
            }
        }

    }
}
