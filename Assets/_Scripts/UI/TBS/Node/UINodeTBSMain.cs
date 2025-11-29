using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeTBSMain : _ASCUINodeBase
    {

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;

        public UINodeTBSMain(SCUIShowType _showType) : base(_showType)
        {
        }

        private GameObject _m_panelGO;
        private UIPanelTBSMain _m_tbsMainPanel;
        private UIMonoTBSMain _m_tbsMainMono;
        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(),true);
            if(_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsMainMono = _m_panelGO.GetComponent<UIMonoTBSMain>();
            if(_m_tbsMainMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsMainPanel = new UIPanelTBSMain(_m_tbsMainMono, _m_showType);
            _m_tbsMainPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsMainPanel == null)
                return;
            _m_tbsMainPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_tbsMainPanel == null)
                return;
            _m_tbsMainPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_tbsMainPanel == null)
                return;
            _m_tbsMainPanel.ShowPanel();
        }


        public override string GetNodeName()
        {
            return nameof(UINodeTBSMain);
        }
        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.TBS_MAIN_PANEL);
        }

    }
}
