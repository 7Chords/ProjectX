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
            if (Input.GetKey(SCSettingMgr.instance.saveKeyInfo.owAttackKeyCode))
            {
                _m_playerController.ChangeState(PlayerStateType.ATTACK);
                return;
            }

            //_m_playerController.mono.Move(new Vector3(0, -9.8f * Time.deltaTime, 0));
            // ¼ì²âÍæ¼ÒµÄÊäÈë
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
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
