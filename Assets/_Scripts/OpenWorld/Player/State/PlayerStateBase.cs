using SCFrame;

namespace GameCore.OW
{
    public abstract class PlayerStateBase : StateBase
    {
        protected PlayerController _m_playerController;
        public override void Init(StateMachine _stateMachine)
        {
            base.Init(_stateMachine);
            _m_playerController = _stateMachine.GetOwner() as PlayerController;
        }
    }
}
