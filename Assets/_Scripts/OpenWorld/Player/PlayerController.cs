using SCFrame;
using UnityEngine;

namespace GameCore.OW
{
    public class PlayerController : Singleton<PlayerController>,IStateMachineOwner
    {
        private SCAnimationCtl _m_animationCtl;//动画控制器
        public SCAnimationCtl animationCtl => _m_animationCtl;

        private PlayerMono _m_playerMono;
        public PlayerMono playerMono => _m_playerMono;

        private StateMachine _m_playerStateMachine;
        public override void OnInitialize()
        {
            _m_playerMono = SCGame.instance.playerMono;

            _m_animationCtl = new SCAnimationCtl();
            _m_animationCtl.SetAnimator(_m_playerMono.playerAnim);
            _m_animationCtl.Initialize();

            _m_playerStateMachine = new StateMachine();
            _m_playerStateMachine.Initialize();
            _m_playerStateMachine.SetOwner(this);


            ChangeState(PlayerStateType.IDLE);
        }

        public override void OnDiscard()
        {
        }

        public void SetMono(PlayerMono _mono)
        {
            _m_playerMono = _mono;
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="_newState"></param>
        public void ChangeState(PlayerStateType _newState)
        {
            switch(_newState)
            {
                case PlayerStateType.IDLE:
                    _m_playerStateMachine.ChangeState<PlayerIdleState>((int)_newState);
                    break;
                case PlayerStateType.RUN:
                    _m_playerStateMachine.ChangeState<PlayerRunState>((int)_newState);
                    break;
                case PlayerStateType.ATTACK:
                    _m_playerStateMachine.ChangeState<PlayerAttackState>((int)_newState);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 播放动画
        /// </summary>
        public void PlayAnimation(string _animationClipName, float _speed = 1, bool _refreshAnimation = false, float _transitionFixedTime = 0.25f)
        {
            AnimationClip clip = _m_playerMono.controlCfg.GetAnimClipByName(_animationClipName);
            if (clip == null)
            {
                SCDebugHelper.LogError("找不到动画片段！！！");
                return;
            }
            _m_animationCtl.PlaySingleAniamtion(clip, _speed, _refreshAnimation, _transitionFixedTime);
        }

        /// <summary>
        /// 播放混合动画
        /// </summary>
        public void PlayBlendAnimation(string _clip1Name, string _clip2Name, float _speed = 1, float _transitionFixedTime = 0.25f)
        {
            AnimationClip clip1 = _m_playerMono.controlCfg.GetAnimClipByName(_clip1Name);
            AnimationClip clip2 = _m_playerMono.controlCfg.GetAnimClipByName(_clip2Name);
            if (clip1 == null || clip2 == null)
            {
                SCDebugHelper.LogError("找不到动画片段！！！");
                return;
            }
            _m_animationCtl.PlayBlendAnimation(clip1, clip2, _speed, _transitionFixedTime);
        }

        public void Move(Vector3 _motion)
        { 
            _m_playerMono.playerGO.transform.position += _motion;
        }

        public void SetRotation(Quaternion _rotation)
        {
            _m_playerMono.playerModel.transform.rotation = _rotation;
        }
    }
}
