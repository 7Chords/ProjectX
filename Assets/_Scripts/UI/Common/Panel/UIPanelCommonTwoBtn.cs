using SCFrame;
using SCFrame.UI;
using System;
using UnityEngine.EventSystems;

namespace GameCore.UI
{
    public class UIPanelCommonTwoBtn : _ASCUIPanelBase<UIMonoCommonTwoBtn>
    {
        private Action _m_onLeftBtnClick;
        private Action _m_onRightBtnClick;


        public UIPanelCommonTwoBtn(UIMonoCommonTwoBtn _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
        }

        public override void BeforeDiscard()
        {
        }

        public override void OnHidePanel()
        {
            mono.btnLeft.RemoveClickDown(onBtnLeftClickDown);
            mono.btnRight.RemoveClickDown(onBtnRightClickDown);
        }

        public override void OnShowPanel()
        {
            mono.btnLeft.AddMouseLeftClickDown(onBtnLeftClickDown);
            mono.btnRight.AddMouseLeftClickDown(onBtnRightClickDown);
        }

        public void SetInfo(string _content, string _leftContent, string _rightContent, Action _onLeftBtnClick, Action _onRightBtnClick)
        {
            mono.txtContent.text = _content;
            mono.txtLeft.text = _leftContent;
            mono.txtRight.text = _rightContent;

            _m_onLeftBtnClick = _onLeftBtnClick;
            _m_onRightBtnClick = _onRightBtnClick;
        }

        private void onBtnRightClickDown(PointerEventData _data, object[] _objs)
        {
            _m_onRightBtnClick?.Invoke();
        }

        private void onBtnLeftClickDown(PointerEventData _data, object[] _objs)
        {
            _m_onLeftBtnClick?.Invoke();
        }
    }

}
