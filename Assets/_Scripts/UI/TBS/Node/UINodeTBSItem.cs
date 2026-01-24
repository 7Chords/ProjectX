using SCFrame;
using SCFrame.UI;
using UnityEngine;


namespace GameCore.UI
{
    public class UINodeTBSItem : _ASCUINodeBase
    {
        public UINodeTBSItem(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => true;
        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;
        public override bool ignoreOnUIList => false;
        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.TBS;
        public override bool needMoveToBottomWhenHide => false;

        private GameObject _m_panelGO;
        private UIPanelTBSItem _m_tbsItemPanel;
        private UIMonoTBSItem _m_tbsItemMono;
        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsItemMono = _m_panelGO.GetComponent<UIMonoTBSItem>();
            if (_m_tbsItemMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsItemPanel = new UIPanelTBSItem(_m_tbsItemMono, _m_showType);
            _m_tbsItemPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsItemPanel == null)
                return;
            _m_tbsItemPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_tbsItemPanel == null)
                return;
            _m_tbsItemPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_tbsItemPanel == null)
                return;
            _m_tbsItemPanel.ShowPanel();
        }

        public override string GetNodeName()
        {
            return nameof(UINodeTBSItem);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.TBS_ITEM_PANEL);
        }
    }
}
