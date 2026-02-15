using GameCore.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.OW
{
    public class PlayerAttackState : PlayerStateBase
    {
        public override void OnEnter()
        {
            AudioMgr.instance.PlaySfx("sfx_attack");
            _m_playerController.PlayAnimation(GameConst.PLAYER_ATTACK_ANIM_NAME);
            _m_playerController.playerMono.animEventTrigger.AddAnimationEvent(GameConst.PLAYER_ATTACK_OVER_EVENT, OnPlayerAttackOver);
        }


        public override void OnExit()
        {
            _m_playerController.playerMono.animEventTrigger.RemoveAnimationEvent(GameConst.PLAYER_ATTACK_OVER_EVENT);
        }

        public override void OnFixedUpdate()
        {
        }

        public override void OnLateUpdate()
        {
        }

        public override void OnUpdate()
        {
        }
        private void OnPlayerAttackOver()
        {
            _m_playerController.ChangeState(PlayerStateType.IDLE);
        }
    }
}
