using GameCore.OW;
using GameCore.TBS;
using GameCore.UI;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore
{

    /// <summary>
    /// ÓÎÏ·³õÊ¼»¯Æ÷
    /// </summary>
    public class GameInit : MonoBehaviour
    {
        //test
        //public bool isNewGame = true;

        private void Awake()
        {
            Application.targetFrameRate = 90;
            SCModel.instance.Initialize();
            SCSystem.instance.Initialize();
            SCPlayer.instance.Initialize();
        }
        public void Start()
        {

            SCMsgCenter.SendMsg(SCMsgConst.GAME_START);
            PlayerController.instance.Initialize();
            Cursor.visible = false;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeMain(SCUIShowType.FULL));
            AudioMgr.instance.PlayBgm("bgm_main");
        }

        private void OnDisable()
        {
            SCPlayer.instance.Discard();
            SCSystem.instance.Discard();
            SCModel.instance.Discard();
        }


    }
}
