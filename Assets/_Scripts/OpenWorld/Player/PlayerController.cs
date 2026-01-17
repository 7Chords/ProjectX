using SCFrame;

namespace GameCore.OW
{
    public class PlayerController : Singleton<PlayerController>
    {
        protected SCAnimationCtl _m_animationCtl;//¶¯»­¿ØÖÆÆ÷
        protected PlayerMono _m_playerMono;

        public override void OnInitialize()
        {
            _m_animationCtl = new SCAnimationCtl();
            _m_animationCtl.SetAnimator(_m_playerMono.playerAnim);
            _m_animationCtl.Initialize();
        }

        public override void OnDiscard()
        {
        }

        public void SetMono(PlayerMono _mono)
        {
            _m_playerMono = _mono;
        }
    }
}
