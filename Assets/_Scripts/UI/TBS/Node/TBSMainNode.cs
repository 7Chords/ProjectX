using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class TBSMainNode : _ASCUINodeBase
    {
        public TBSMainNode(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;

        public override string GetNodeName()
        {
            return nameof(TBSMainNode);
        }
        public override string GetResName()
        {
            return "panel_tbs_main";
        }
        private GameObject _m_panelGO;
        private TBSMainPanel _m_tbsMainPanel;
        private TBSMainMono _m_tbsMainMono;
        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), SCGame.instance.mainCanvas.transform,true);
            if(_m_panelGO == null)
            {
                Debug.Log("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsMainMono = _m_panelGO.GetComponent<TBSMainMono>();
            if(_m_tbsMainMono == null)
            {
                Debug.Log("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsMainPanel = new TBSMainPanel(_m_tbsMainMono, _m_showType);
            _m_tbsMainPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsMainPanel == null)
                return;
            _m_tbsMainPanel.HidePanel();
            SCCommon.SetGameObjectEnable(_m_panelGO,false);
        }

        public override void OnQuitNode()
        {
            if (_m_tbsMainPanel == null)
                return;
            _m_tbsMainPanel.Discard();
            SCCommon.DestoryGameObject(_m_panelGO);
        }

        public override void OnShowNode()
        {
            if (_m_tbsMainPanel == null)
                return;
            _m_tbsMainPanel.ShowPanel();
        }
    }
}
