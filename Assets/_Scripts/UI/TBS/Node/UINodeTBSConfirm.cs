using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeTBSConfirm : _ASCUINodeBase
    {
        public UINodeTBSConfirm(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;

        private GameObject _m_panelGO;
        private UIPanelTBSConfirm _m_tbsConfirmPanel;
        private UIMonoTBSConfirm _m_tbsConfirmMono;
        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsConfirmMono = _m_panelGO.GetComponent<UIMonoTBSConfirm>();
            if (_m_tbsConfirmMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsConfirmPanel = new UIPanelTBSConfirm(_m_tbsConfirmMono, _m_showType);
            _m_tbsConfirmPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsConfirmPanel == null)
                return;
            _m_tbsConfirmPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_tbsConfirmPanel == null)
                return;
            _m_tbsConfirmPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_tbsConfirmPanel == null)
                return;
            _m_tbsConfirmPanel.ShowPanel();
        }

        public override string GetNodeName()
        {
            return nameof(UINodeTBSConfirm);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.TBS_CONFIM_PANEL);
        }
    }
}
