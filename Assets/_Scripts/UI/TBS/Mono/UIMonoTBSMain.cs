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
        [Header("按钮被点击后的缩放大小")]
        public float scaleClickBtn;
        [Header("按钮被点击后的缩放时长")]
        public float durationBtnScaleChg;
        [Header("按钮默认的缩放")]
        public float scaleBtnDefault;
    }
}
