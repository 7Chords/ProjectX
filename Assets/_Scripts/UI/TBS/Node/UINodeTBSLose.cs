using SCFrame;
using SCFrame.UI;
using UnityEngine;


namespace GameCore.UI
{
    public class UINodeTBSLose : _ASCUINodeBase
    {
        public UINodeTBSLose(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => false;

        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;

        private GameObject _m_panelGO;
        private UIPanelTBSLose _m_tbsLosePanel;
        private UIMonoTBSLose _m_tbsLoseMono;

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsLoseMono = _m_panelGO.GetComponent<UIMonoTBSLose>();
            if (_m_tbsLoseMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsLosePanel = new UIPanelTBSLose(_m_tbsLoseMono, _m_showType);
            _m_tbsLosePanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsLosePanel == null)
                return;
            _m_tbsLosePanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_tbsLosePanel == null)
                return;
            _m_tbsLosePanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_tbsLosePanel == null)
                return;
            _m_tbsLosePanel.ShowPanel();
        }
        public override string GetNodeName()
        {
            return nameof(UINodeTBSLose);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.TBS_LOSE_PANEL);

        }
    }
}
