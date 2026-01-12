using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSItemContainerItem : _ASCUIMonoBase
    {
        [Header("道具image")]
        public Image imgItem;

        [Header("道具图标")]
        public Image imgItemIcon;

        [Header("道具名称")]
        public Text txtItemName;

        [Header("道具剩余")]
        public Text txtItemRemain;

        [Header("道具选中颜色")]
        public Color colorItemSelect;

        [Header("道具未选中颜色")]
        public Color colorItemUnSelect;

        [Header("道具点击按钮")]
        public Button btnItemClick;
    }

}
