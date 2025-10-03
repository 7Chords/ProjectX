using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.UI
{
    public class UINodeTBSInfo : _ASCUINodeBase
    {
        public UINodeTBSInfo(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => false;

        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;

        private GameObject _m_panelGO;
        private UIPanelTBSInfo _m_tbsInfoPanel;
        private UIMonoTBSInfo _m_tbsInfoMono;
        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsInfoMono = _m_panelGO.GetComponent<UIMonoTBSInfo>();
            if (_m_tbsInfoMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsInfoPanel = new UIPanelTBSInfo(_m_tbsInfoMono, _m_showType);
            _m_tbsInfoPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsInfoPanel == null)
                return;
            _m_tbsInfoPanel.HidePanel();
            SCCommon.SetGameObjectEnable(_m_panelGO, false);
        }

        public override void OnQuitNode()
        {
            if (_m_tbsInfoPanel == null)
                return;
            _m_tbsInfoPanel.Discard();
            SCCommon.DestoryGameObject(_m_panelGO);
        }

        public override void OnShowNode()
        {
            if (_m_tbsInfoPanel == null)
                return;
            SCCommon.SetGameObjectEnable(_m_panelGO, true);
            _m_tbsInfoPanel.ShowPanel();
        }

        public override string GetNodeName()
        {
            return nameof(UINodeTBSInfo);
        }

        public override string GetResName()
        {
            return "panel_tbs_info";
        }
    }
}
