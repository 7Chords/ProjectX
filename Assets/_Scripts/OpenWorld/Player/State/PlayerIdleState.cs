using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.OW
{
    public class PlayerIdleState : PlayerStateBase
    {
        public override void OnEnter()
        {
            _m_playerController.PlayAnimation(GameConst.PLAYER_IDLE_ANIM_NAME);
        }
        public override void OnUpdate()
        {
            if (SCInputListener.instance.GetKeyCodeDown(SCSettingMgr.instance.saveKeyInfo.owAttackKeyCode))
            {
                _m_playerController.ChangeState(PlayerStateType.ATTACK);
                return;
            }

            // ¼ì²âÍæ¼ÒµÄÊäÈë
            float h = SCInputListener.instance.GetHorizontalInput();
            float v = SCInputListener.instance.GetVerticalInput();
            if (h != 0 || v != 0)
            {
                // ÇÐ»»×´Ì¬
                _m_playerController.ChangeState(PlayerStateType.RUN);
            }
        }
        public override void OnExit()
        {
        }

        public override void OnFixedUpdate()
        {
        }

        public override void OnLateUpdate()
        {
        }

    }
}
