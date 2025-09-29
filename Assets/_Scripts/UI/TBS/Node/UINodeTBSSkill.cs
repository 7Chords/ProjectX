using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeTBSSkill : _ASCUINodeBase
    {
        public UINodeTBSSkill(SCUIShowType _showType) : base(_showType)
        {
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;


        private GameObject _m_panelGO;
        private UIPanelTBSSkill _m_tbsSkillPanel;
        private UIMonoTBSSkill _m_tbsSkillMono;

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsSkillMono = _m_panelGO.GetComponent<UIMonoTBSSkill>();
            if (_m_tbsSkillMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsSkillPanel = new UIPanelTBSSkill(_m_tbsSkillMono, _m_showType);
            _m_tbsSkillPanel.Initialize();
        }

        public override void OnHideNode()
        {
            if (_m_tbsSkillPanel == null)
                return;
            _m_tbsSkillPanel.HidePanel();
            SCCommon.SetGameObjectEnable(_m_panelGO, false);
        }

        public override void OnQuitNode()
        {
            if (_m_tbsSkillPanel == null)
                return;
            _m_tbsSkillPanel.Discard();
            SCCommon.DestoryGameObject(_m_panelGO);
        }

        public override void OnShowNode()
        {
            if (_m_tbsSkillPanel == null)
                return;
            SCCommon.SetGameObjectEnable(_m_panelGO, true);
            _m_tbsSkillPanel.ShowPanel();
        }

        public override string GetNodeName()
        {
            return nameof(UINodeTBSSkill);
        }

        public override string GetResName()
        {
            return "panel_tbs_skill";
        }
    }
}
