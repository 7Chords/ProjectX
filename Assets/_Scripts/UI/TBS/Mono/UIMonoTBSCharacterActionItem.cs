using SCFrame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSCharacterActionItem : _ASCUIMonoBase
    {
        [Header("头像背景图片")]
        public Image imgHeadBg;

        [Header("头像图片")]
        public Image imgHead;

        [Header("是行动角色的颜色")]
        public Image colorIsAction;

        [Header("不是行动角色的颜色")]
        public Image colorIsNotAction;
    }
}
