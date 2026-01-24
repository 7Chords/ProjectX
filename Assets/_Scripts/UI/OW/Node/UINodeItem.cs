using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeItem : _ASCUINodeBase
    {
        public UINodeItem(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => false;

        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;

        public override bool ignoreOnUIList => false;

        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.OW;

        public override bool needMoveToBottomWhenHide => false;

        private GameObject _m_panelGO;
        private UIPanelItem _m_itemPanel;
        private UIMonoItem _m_itemMono;

        public override string GetNodeName()
        {
            return nameof(UINodeItem);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.ITEM_PANEL);
        }

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_itemMono = _m_panelGO.GetComponent<UIMonoItem>();
            if (_m_itemMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_itemPanel = new UIPanelItem(_m_itemMono, _m_showType);
            _m_itemPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_itemPanel == null)
                return;
            _m_itemPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_itemPanel == null)
                return;
            _m_itemPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_itemPanel == null)
                return;
            _m_itemPanel.ShowPanel();
        }
    }
}
