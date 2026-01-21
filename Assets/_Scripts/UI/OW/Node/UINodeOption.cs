using GameCore.OW;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeOption : _ASCUINodeBase
    {
        public UINodeOption(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => true;

        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;

        public override bool ignoreOnUIList => false;

        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.OW;

        private GameObject _m_panelGO;
        private UIPanelOption _m_optionPanel;
        private UIMonoOption _m_optionMono;
        public override string GetNodeName()
        {
            return nameof(UINodeOption);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.OPTION_PANEL);
        }

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_optionMono = _m_panelGO.GetComponent<UIMonoOption>();
            if (_m_optionMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_optionPanel = new UIPanelOption(_m_optionMono, _m_showType);
            _m_optionPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_optionPanel == null)
                return;
            _m_optionPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_optionPanel == null)
                return;
            _m_optionPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_optionPanel == null)
                return;
            _m_optionPanel.ShowPanel();
        }
    }
}
