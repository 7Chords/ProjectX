using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    public class SCInputListener : Singleton<SCInputListener>
    {

        private int _m_tbsFrameChecker;

        private int _m_tbsFrameInterval;
        public override void OnInitialize()
        {
            SCTaskHelper.instance.AddUpdateListener(update);
            _m_tbsFrameInterval = SCRefDataMgr.instance.gameGeneralRefObj.tbsInputFrameInterval;
        }

        public override void OnDiscard()
        {
            SCTaskHelper.instance.RemoveUpdateListener(update);
        }


        private void update()
        {
            if (GameCoreMgr.instance.tbsCoreMgr.tbsGameHasStarted)
            {
                if (_m_tbsFrameChecker < _m_tbsFrameInterval)
                {
                    _m_tbsFrameChecker += 1;
                    return;
                }
                if (Input.anyKeyDown)
                    _m_tbsFrameChecker = 0;

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
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSwitchToLeftKeyCode))
                    SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SWITCH_TO_LEFT_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSwitchToRightKeyCode))
                    SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SWITCH_TO_RIGHT_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsConfirmKeyCode))
                    SCMsgCenter.SendMsgAct(SCMsgConst.TBS_CONFIRM_INPUT);
            }

        }
    }
}
