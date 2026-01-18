using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.OW
{
    [CreateAssetMenu(fileName = "new PlayerControlCfg",menuName = "MBC配置/OW/PlayerControlCfg")]
    public class PlayerControlCfg : ScriptableObject
    {
        [Header("idle动画片段")]
        public AnimationClip animClipIdle;
        [Header("walk动画片段")]
        public AnimationClip animClipWalk;
        [Header("run动画片段")]
        public AnimationClip animClipRun;
        [Header("attack动画片段")]
        public AnimationClip animClipAttack;
        [Header("移动速度")]
        public float walkSpeed;
        [Header("奔跑速度")]
        public float runSpeed;
        [Header("walk到run的过渡速度")]
        public float walk2RunTransitionSpeed;
        [Header("旋转速度")]
        public float rotateSpeed;
        public AnimationClip GetAnimClipByName(string _animName)
        {
            switch (_animName)
            {
                case GameConst.PLAYER_IDLE_ANIM_NAME:
                    return animClipIdle;
                case GameConst.PLAYER_WALK_ANIM_NAME:
                    return animClipWalk;
                case GameConst.PLAYER_RUN_ANIM_NAME:
                    return animClipRun;
                case GameConst.PLAYER_ATTACK_ANIM_NAME:
                    return animClipAttack;
                default:
                    return null;
            }
        }
    }
}
