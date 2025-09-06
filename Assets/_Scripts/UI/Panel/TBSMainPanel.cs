using SCFrame.UI;
using SCFrame;
using UnityEngine.EventSystems;
using System;

namespace GameCore.UI
{
    public class TBSMainPanel : _ASCUIPanelBase<TBSMainMono>
    {
        public TBSMainPanel(TBSMainMono _mono, SCUIShowType _showType) : base(_mono, _showType)
        {

        }
        public override void OnInitialize()
        {
            mono.btnNormalAttack.AddClickDown(_onBtnNormalAttackClickDown);
        }


        public override void OnDiscard()
        {
            mono.btnNormalAttack.RemoveClickDown(_onBtnNormalAttackClickDown);
        }

        public override void OnHidePanel()
        {
        }



        public override void OnShowPanel()
        {
        }

        //普通攻击按下回调
        private void _onBtnNormalAttackClickDown(PointerEventData _eventData, object[] _args)
        {

        }
    }
}
