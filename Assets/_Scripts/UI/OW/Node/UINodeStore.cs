using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeStore : _ASCUINodeBase
    {

        public UINodeStore(SCUIShowType _showType,long _storeId) : base(_showType)
        {
            _m_storeId = _storeId;
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => false;

        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;

        public override bool ignoreOnUIList => false;

        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.OW;


        private GameObject _m_panelGO;
        private UIPanelStore _m_storePanel;
        private UIMonoStore _m_storeMono;

        private long _m_storeId;
        public override string GetNodeName()
        {
            return nameof(UINodeStore);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.STORE_PANEL);
        }

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_storeMono = _m_panelGO.GetComponent<UIMonoStore>();
            if (_m_storeMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_storePanel = new UIPanelStore(_m_storeMono, _m_showType);
            _m_storePanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_storePanel == null)
                return;
            _m_storePanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_storePanel == null)
                return;
            _m_storePanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_storePanel == null)
                return;
            _m_storePanel.SetInfo(_m_storeId);
            _m_storePanel.ShowPanel();
        }
        public override void CopyData(_ASCUINodeBase _anotherNode)
        {
            if (_anotherNode is UINodeStore node) 
            {
                _m_storeId = node._m_storeId;
            }
        }
    }

}
