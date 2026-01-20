using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeDialogue : _ASCUINodeBase
    {
        public UINodeDialogue(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => true;

        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;

        public override bool ignoreOnUIList => false;

        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.OW;


        private GameObject _m_panelGO;
        private UIPanelDialogue _m_dialoguePanel;
        private UIMonoDialogue _m_dialogueMono;
        public override string GetNodeName()
        {
            return nameof(UINodeDialogue);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.DIALOGUE_PANEL);
        }

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_dialogueMono = _m_panelGO.GetComponent<UIMonoDialogue>();
            if (_m_dialogueMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_dialoguePanel = new UIPanelDialogue(_m_dialogueMono, _m_showType);
            _m_dialoguePanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_dialoguePanel == null)
                return;
            _m_dialoguePanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_dialoguePanel == null)
                return;
            _m_dialoguePanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_dialoguePanel == null)
                return;
            _m_dialoguePanel.ShowPanel();
        }
    }
}
