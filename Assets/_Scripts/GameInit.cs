using GameCore.TBS;
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

        private void Awake()
        {
            SCSystem.instance.Initialize();
            SCPlayer.instance.Initialize();
            SCModel.instance.Initialize();
        }
        public void Start()
        {

            if (isNewGame)
                SCModel.instance.InitNewData();
            else
                SCModel.instance.LoadData();


            SCMsgCenter.SendMsg(SCMsgConst.GAME_START);
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_GAME_START);
        }


        private void Update()
        {
        }

        private void OnDisable()
        {
            SCModel.instance.Discard();
            SCPlayer.instance.Discard();
            SCSystem.instance.Discard();
        }


    }
}
