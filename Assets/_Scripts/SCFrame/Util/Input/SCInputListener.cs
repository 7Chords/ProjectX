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
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SKILL_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsAttackKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ATTACK_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsDefendKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_DEFEND_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsItemKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ITEM_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSwitchToUpKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SWITCH_TO_UP_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSwitchToDownKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SWITCH_TO_DOWN_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSwitchToLeftKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SWITCH_TO_LEFT_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsSwitchToRightKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SWITCH_TO_RIGHT_INPUT);
                if (Input.GetKeyDown(SCSaveSys.instance.saveKeyInfo.tbsConfirmKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_CONFIRM_INPUT);
            }

        }
    }
}
