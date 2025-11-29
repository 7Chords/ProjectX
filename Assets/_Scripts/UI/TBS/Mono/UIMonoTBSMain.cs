using SCFrame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSMain : _ASCUIAnimMonoBase
    {
        [Header("技能按钮")]
        public Button btnSkill;
        [Header("攻击按钮")]
        public Button btnNormalAttack;
        [Header("道具按钮")]
        public Button btnItem;
        [Header("防御按钮")]
        public Button btnDefence;
        [Header("角色头像")]
        public Image imgCharacterHead;
    }
}
