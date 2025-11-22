using SCFrame.UI;
using System.Collections.Generic;
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
        public Color colorIsAction;

        [Header("不是行动角色的颜色")]
        public Color colorIsNotAction;

        [Header("是行动角色显示的物体")]
        public List<GameObject> goIsActionShowList;

        [Header("是行动角色隐藏的物体")]
        public List<GameObject> goIsActionHideList;
    }
}
