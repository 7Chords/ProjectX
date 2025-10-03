using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    public class SCInputListener : Singleton<SCInputListener>
    {
        public override void OnInitialize()
        {
            SCTaskHelper.instance.AddUpdateListener(update);

        }

        public override void OnDiscard()
        {
            SCTaskHelper.instance.RemoveUpdateListener(update);
        }


        private void update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                GameCoreMgr.instance.uiCoreMgr.CloseNodeByEsc();
            if (Input.GetMouseButtonDown(1))
                GameCoreMgr.instance.uiCoreMgr.CloseNodeByMouseRight();
            if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSkillKeyCode))
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SKILL_INPUT);
            if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsAttackKeyCode))
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ATTACK_INPUT);
            if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsDefendKeyCode))
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_DEFEND_INPUT);
            if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsItemKeyCode))
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ITEM_INPUT);
            if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSwitchToUpKeyCode))
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SWITCH_TO_UP_INPUT);
            if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSwitchToDownKeyCode))
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SWITCH_TO_DOWN_INPUT);
            //if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSwitchToLeftKeyCode))
            //    SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SKILL_INPUT);
        }
    }
}
