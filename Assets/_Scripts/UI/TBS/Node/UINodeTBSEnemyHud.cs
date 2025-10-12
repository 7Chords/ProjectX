using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeTBSEnemyHud : _ASCUINodeBase
    {

        public override bool needHideWhenEnterNewSameTypeNode => false;

        public override bool canQuitByEsc => false;

        public override bool canQuitByMouseRight => false;

        private GameObject _m_panelGO;
        private UIPanelTBSEnemyHud _m_tbsEnemyHudPanel;
        private UIMonoTBSEnemyHud _m_tbsEnemyHudMono;
        private List<TBSActorBase> _m_actorList;
        public UINodeTBSEnemyHud(SCUIShowType _showType, List<TBSActorBase> _actorList) : base(_showType)
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
            _m_tbsEnemyHudMono = _m_panelGO.GetComponent<UIMonoTBSEnemyHud>();
            if (_m_tbsEnemyHudMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsEnemyHudPanel = new UIPanelTBSEnemyHud(_m_tbsEnemyHudMono, _m_showType);
            _m_tbsEnemyHudPanel.Initialize();
            _m_tbsEnemyHudPanel.SetInfo(_m_actorList);
        }

        public override void OnHideNode()
        {
            if (_m_tbsEnemyHudPanel == null)
                return;
            _m_tbsEnemyHudPanel.HidePanel();
            SCCommon.SetGameObjectEnable(_m_panelGO, false);
        }

        public override void OnQuitNode()
        {
            if (_m_tbsEnemyHudPanel == null)
                return;
            _m_tbsEnemyHudPanel.Discard();
            SCCommon.DestoryGameObject(_m_panelGO);
        }

        public override void OnShowNode()
        {
            if (_m_tbsEnemyHudPanel == null)
                return;
            SCCommon.SetGameObjectEnable(_m_panelGO, true);
            _m_tbsEnemyHudPanel.ShowPanel();
        }

        public override string GetNodeName()
        {
            return nameof(UINodeTBSEnemyHud);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.TBS_ENEMY_HUD_PANEL);
        }
    }
}
