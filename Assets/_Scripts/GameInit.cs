using GameCore.UI;
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
        public bool isNewGame = true;

        public bool isTestMode = true;
        public void Start()
        {
            SCSystem.instance.Initialize();
            SCPlayer.instance.Initialize();
            SCModel.instance.Initialize();

            SCMsgCenter.SendMsg(SCMsgConst.GAME_START);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_GAME_START);

            if(isNewGame)
            {
                if (isTestMode)
                    SCModel.instance.InitTempData();
                else
                    SCModel.instance.InitNewData();
            }
            else
                SCModel.instance.LoadData();
        }


        private void Update()
        {
            //test
            if(Input.GetKeyDown(KeyCode.T))
            {
                SCMsgCenter.SendMsg(SCMsgConst.TBS_GAME_START);
            }
        }

    }
}
