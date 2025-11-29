using SCFrame;
using SCFrame.UI;
using UnityEngine;


namespace GameCore.UI
{
    public class UINodeTBSTurnChg : _ASCUINodeBase
    {
        public UINodeTBSTurnChg(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => false;

        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;


        private GameObject _m_panelGO;
        private UIPanelTBSTurnChg _m_tbsTurnChgPanel;
        private UIMonoTBSTurnChg _m_tbsTurnChgMono;

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsTurnChgMono = _m_panelGO.GetComponent<UIMonoTBSTurnChg>();
            if (_m_tbsTurnChgMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsTurnChgPanel = new UIPanelTBSTurnChg(_m_tbsTurnChgMono, _m_showType);
            _m_tbsTurnChgPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsTurnChgPanel == null)
                return;
            _m_tbsTurnChgPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_tbsTurnChgPanel == null)
                return;
            _m_tbsTurnChgPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_tbsTurnChgPanel == null)
                return;
            _m_tbsTurnChgPanel.ShowPanel();
        }

        public override string GetNodeName()
        {
            return nameof(UINodeTBSTurnChg);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.TBS_TURN_CHG_PANEL);
        }
    }
}
