using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeCharacter : _ASCUINodeBase
    {
        public UINodeCharacter(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => false;

        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;

        public override bool ignoreOnUIList => false;

        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.OW;


        private GameObject _m_panelGO;
        private UIPanelCharacter _m_characterPanel;
        private UIMonoCharacter _m_characterMono;
        public override string GetNodeName()
        {
            return nameof(UINodeCharacter);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.CHARACTER_PANEL);
        }

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_characterMono = _m_panelGO.GetComponent<UIMonoCharacter>();
            if (_m_characterMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_characterPanel = new UIPanelCharacter(_m_characterMono, _m_showType);
            _m_characterPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_characterPanel == null)
                return;
            _m_characterPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_characterPanel == null)
                return;
            _m_characterPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_characterPanel == null)
                return;
            _m_characterPanel.ShowPanel();
        }
    }
}
