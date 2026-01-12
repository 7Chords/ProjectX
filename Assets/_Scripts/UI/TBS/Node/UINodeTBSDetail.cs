using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeTBSDetail : _ASCUINodeBase
    {
        public UINodeTBSDetail(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => true;

        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;

        public override bool ignoreOnUIList => false;

        private GameObject _m_panelGO;
        private UIPanelTBSDetail _m_tbsDetailPanel;
        private UIMonoTBSDetail _m_tbsDetailMono;

        public override string GetNodeName()
        {
            return nameof(UINodeTBSDetail);
        }

        public override string GetResName()
        {
            return GameConst.TBS_DETAIL_PANEL;
        }

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsDetailMono = _m_panelGO.GetComponent<UIMonoTBSDetail>();
            if (_m_tbsDetailMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsDetailPanel = new UIPanelTBSDetail(_m_tbsDetailMono, _m_showType);
            _m_tbsDetailPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsDetailPanel == null)
                return;
            _m_tbsDetailPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_tbsDetailPanel == null)
                return;
            _m_tbsDetailPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_tbsDetailPanel == null)
                return;
            _m_tbsDetailPanel.ShowPanel();
        }
    }
}
