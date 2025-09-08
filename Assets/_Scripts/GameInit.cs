using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore
{
    public class GameInit : MonoBehaviour
    {

        public void Start()
        {
            SCSystem.instance.Initialize();
            SCPlayer.instance.Initialize();
            SCModel.instance.Initialize();

            SCMsgCenter.SendMsg(SCMsgConst.GAME_START);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_GAME_START);
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
