using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{

    public class UINodeMain : _ASCUINodeBase
    {
        public UINodeMain(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => true;

        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;

        public override bool ignoreOnUIList => false;

        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.OW;

        private GameObject _m_panelGO;
        private UIPanelMain _m_mainPanel;
        private UIMonoMain _m_mainMono;

        public override string GetNodeName()
        {
            return nameof(UINodeMain);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.MAIN_PANEL);
        }

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_mainMono = _m_panelGO.GetComponent<UIMonoMain>();
            if (_m_mainMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_mainPanel = new UIPanelMain(_m_mainMono, _m_showType);
            _m_mainPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_mainPanel == null)
                return;
            _m_mainPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_mainPanel == null)
                return;
            _m_mainPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_mainPanel == null)
                return;
            _m_mainPanel.ShowPanel();
        }
    }

}