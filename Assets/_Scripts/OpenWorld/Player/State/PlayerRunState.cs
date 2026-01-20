using SCFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.OW
{
    public class PlayerRunState : PlayerStateBase
    {
        private SCAnimationCtl _m_animCtl;

        private float runTransition;


        private Vector3 _m_motion;
        private Vector3 _m_moveDir;
        public override void Init(StateMachine _stateMachine)
        {
            base.Init(_stateMachine);
            _m_animCtl = _m_playerController.animationCtl;
        }
        public override void OnEnter()
        {
            runTransition = 0;
            _m_playerController.PlayBlendAnimation(GameConst.PLAYER_WALK_ANIM_NAME, GameConst.PLAYER_RUN_ANIM_NAME);
            _m_animCtl.SetBlendWeight(1);
        }

        public override void OnExit()
        {
        }

        public override void OnFixedUpdate()
        {

            _m_playerController.Move(_m_motion + _m_playerController.playerMono.transform.position);

            // 处理旋转
            _m_playerController.SetRotation(Quaternion.Slerp(_m_playerController.playerMono.playerModel.transform.rotation,
                Quaternion.LookRotation(_m_moveDir), Time.deltaTime * _m_playerController.playerMono.controlCfg.rotateSpeed));
        }

        public override void OnLateUpdate()
        {
        }

        public override void OnUpdate()
        {
            if (!_m_playerController.canControl)
                return;
            if (SCInputListener.instance.GetKeyCodeDown(SCSettingMgr.instance.saveKeyInfo.owAttackKeyCode))
            {
                _m_playerController.ChangeState(PlayerStateType.ATTACK);
                return;
            }


            float h = SCInputListener.instance.GetHorizontalInput();
            float v = SCInputListener.instance.GetVerticalInput();

            if (h == 0 && v == 0)
            {
                // 切换状态
                _m_playerController.ChangeState(PlayerStateType.IDLE);
            }
            else
            {
                // 处理移动
                Vector3 input = new Vector3(h, 0, v);
                if (SCInputListener.instance.GetKeyCode(SCSettingMgr.instance.saveKeyInfo.owRunKeyCode))
                    runTransition = Mathf.Clamp(runTransition + Time.deltaTime * _m_playerController.playerMono.controlCfg.walk2RunTransitionSpeed, 0, 1);
                else
                    runTransition = Mathf.Clamp(runTransition - Time.deltaTime * _m_playerController.playerMono.controlCfg.walk2RunTransitionSpeed, 0, 1);

                _m_animCtl.SetBlendWeight(1 - runTransition);

                // 获取相机的y旋转值
                float y = Camera.main.transform.rotation.eulerAngles.y;
                // 让input也旋转y角度
                // 四元数和向量相乘：表示这个向量按照这个四元数进行旋转之后得到新的向量
                _m_moveDir = Quaternion.Euler(0, y, 0) * input;

                float speed = Mathf.Lerp(_m_playerController.playerMono.controlCfg.walkSpeed, _m_playerController.playerMono.controlCfg.runSpeed, runTransition);
                _m_motion = Time.deltaTime * speed * _m_moveDir;
            }
        }

    }
}
