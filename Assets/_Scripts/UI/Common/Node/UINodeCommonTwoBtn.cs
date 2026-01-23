using SCFrame;
using SCFrame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeCommonTwoBtn : _ASCUINodeBase
    {
        public UINodeCommonTwoBtn(SCUIShowType _showType,string _content,string _leftContent,string _rightContent,Action _onLeftBtnClick,Action _onRightBtnClick) : base(_showType)
        {
            _m_content = _content;
            _m_leftContent = _leftContent;
            _m_rightContent = _rightContent;
            _m_onLeftBtnClick = _onLeftBtnClick;
            _m_onRightBtnClick = _onRightBtnClick;
        }

        public override bool needHideWhenEnterNewSameTypeNode => false;

        public override bool needShowWhenQuitNewSameTypeNode => false;

        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;

        public override bool ignoreOnUIList => false;

        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.COMMON;



        private GameObject _m_panelGO;
        private UIPanelCommonTwoBtn _m_twoBtnPanel;
        private UIMonoCommonTwoBtn _m_twoBtnMono;

        public string _m_content;
        private string _m_leftContent;
        private string _m_rightContent;
        private Action _m_onLeftBtnClick;
        private Action _m_onRightBtnClick;

        public override string GetNodeName()
        {
            return nameof(UINodeCommonTwoBtn);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.COMMON_TWO_BTN_PANEL);
        }

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_twoBtnMono = _m_panelGO.GetComponent<UIMonoCommonTwoBtn>();
            if (_m_twoBtnMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_twoBtnPanel = new UIPanelCommonTwoBtn(_m_twoBtnMono, _m_showType);
            _m_twoBtnPanel.Initialize();
            _m_twoBtnPanel.SetInfo(_m_content, _m_leftContent, _m_rightContent, _m_onLeftBtnClick, _m_onRightBtnClick); 
        }

        public override void OnHideNode()
        {
            if (_m_twoBtnPanel == null)
                return;
            _m_twoBtnPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_twoBtnPanel == null)
                return;
            _m_twoBtnPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_twoBtnPanel == null)
                return;
            _m_twoBtnPanel.ShowPanel();
        }

        public override void CopyData(_ASCUINodeBase _anotherNode)
        {
            if (_anotherNode is UINodeCommonTwoBtn node)
            {
                _m_content = node._m_content;
                _m_leftContent = node._m_leftContent;
                _m_rightContent = node._m_rightContent;
                _m_onLeftBtnClick = node._m_onLeftBtnClick;
                _m_onRightBtnClick = node._m_onRightBtnClick;
            }
        }
    }
}
