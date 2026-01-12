using SCFrame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSSkillContainerItem : _ASCUIMonoBase
    {
        [Header("技能image")]
        public Image imgSkill;

        [Header("技能图标")]
        public Image imgSkillIcon;

        [Header("技能名称")]
        public Text txtSkillName;

        [Header("技能消耗")]
        public Text txtSkillNeed;

        [Header("技能选中颜色")]
        public Color colorSkillSelect;

        [Header("技能未选中颜色")]
        public Color colorSkillUnSelect;

        [Header("技能点击按钮")]
        public Button btnSkillClick;

        [Header("技能消耗HP颜色")]
        public Color colorSkillHp;

        [Header("技能消耗Mp颜色")]
        public Color colorSkillMp;
    }
}
