using GameCore.UI;
using SCFrame;
using UnityEngine;

namespace GameCore.OW
{
    public static class DialogueStarter
    {
        public static void LoadDialogue(DialogueInfo _dialogueInfo)
        {
            Cursor.visible = true;
            SCGame.instance.owCamera.m_YAxis.m_MaxSpeed = 0;
            SCGame.instance.owCamera.m_XAxis.m_MaxSpeed = 0;
            PlayerController.instance.SetCanControl(false);
            PlayerController.instance.ChangeState(PlayerStateType.IDLE);
            SCModel.instance.owModel.dialogueInfo = _dialogueInfo;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeDialogue(SCFrame.UI.SCUIShowType.FULL));

        }

        public static void UnloadDialogue()
        {
            Cursor.visible = false;
            SCGame.instance.owCamera.m_YAxis.m_MaxSpeed = 3;
            SCGame.instance.owCamera.m_XAxis.m_MaxSpeed = 400;
            GameCoreMgr.instance.uiCoreMgr.CloseTopNode();
            PlayerController.instance.SetCanControl(true);
        }
    }
}

