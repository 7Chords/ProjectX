using SCFrame;

namespace GameCore.OW
{
    public class PlayerController : Singleton<PlayerController>,IStateMachineOwner
    {
        private SCAnimationCtl _m_animationCtl;//¶¯»­¿ØÖÆÆ÷
        private PlayerMono _m_playerMono;
        private StateMachine _m_playerStateMachine;
        public override void OnInitialize()
        {
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
    }
}
