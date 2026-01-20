using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeTBSPlayerHud : _ASCUINodeBase
    {

        public override bool needHideWhenEnterNewSameTypeNode => false;

        public override bool needShowWhenQuitNewSameTypeNode => false;

        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;

        public override bool ignoreOnUIList => true;
        public override SCUINodeFuncType nodeFuncType => SCUINodeFuncType.TBS;

        private GameObject _m_panelGO;
        private UIPanelTBSPlayerHud _m_tbsPlayerHudPanel;
        private UIMonoTBSPlayerHud _m_tbsPlayerHudMono;
        private List<TBSActorBase> _m_actorList;
        public UINodeTBSPlayerHud(SCUIShowType _showType, List<TBSActorBase> _actorList) : base(_showType)
        {
            _m_actorList = _actorList;
        }
        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsPlayerHudMono = _m_panelGO.GetComponent<UIMonoTBSPlayerHud>();
            if (_m_tbsPlayerHudMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsPlayerHudPanel = new UIPanelTBSPlayerHud(_m_tbsPlayerHudMono, _m_showType);
            _m_tbsPlayerHudPanel.Initialize();
            _m_tbsPlayerHudPanel.SetInfo(_m_actorList);
        }

        public override void OnHideNode()
        {
            if (_m_tbsPlayerHudPanel == null)
                return;
            _m_tbsPlayerHudPanel.HidePanel();
        }

        public override void OnQuitNode()
        {
            if (_m_tbsPlayerHudPanel == null)
                return;
            _m_tbsPlayerHudPanel.Discard();
        }

        public override void OnShowNode()
        {
            if (_m_tbsPlayerHudPanel == null)
                return;
            _m_tbsPlayerHudPanel.ShowPanel();
        }

        public override string GetNodeName()
        {
            return nameof(UINodeTBSPlayerHud);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.TBS_PLAYER_HUD_PANEL);
        }
    }
}
