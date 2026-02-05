using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeTBSWin : _ASCUINodeBase
    {
        public UINodeTBSWin(SCUIShowType _showType, List<bool> _hasLevelUpStateList,int _money) : base(_showType)
        {
            _m_hasLevelUpStateList = _hasLevelUpStateList;
            _m_money = _money;
        }

        public override bool needHideWhenEnterNewSameTypeNode => false;

        public override bool needShowWhenQuitNewSameTypeNode => false;
        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;
        public override bool ignoreOnUIList => false;
        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.TBS;
        public override bool needMoveToBottomWhenHide => false;

        private GameObject _m_panelGO;
        private UIPanelTBSWin _m_tbsWinPanel;
        private UIMonoTBSWin _m_tbsWinMono;

        private List<bool> _m_hasLevelUpStateList;//是否升级了的状态列表
        private int _m_money;
        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsWinMono = _m_panelGO.GetComponent<UIMonoTBSWin>();
            if (_m_tbsWinMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsWinPanel = new UIPanelTBSWin(_m_tbsWinMono, _m_showType);
            _m_tbsWinPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsWinPanel == null)
                return;
            _m_tbsWinPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_tbsWinPanel == null)
                return;
            _m_tbsWinPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_tbsWinPanel == null)
                return;
            _m_tbsWinPanel.ShowPanel();
            _m_tbsWinPanel.SetInfo(_m_hasLevelUpStateList,_m_money);
        }
        public override string GetNodeName()
        {
            return nameof(UINodeTBSWin);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.TBS_WIN_PANEL);

        }
    }
}
