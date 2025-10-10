using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSEnemyHudItem : _ASCUIMonoBase
    {
        [Header("角色名字带等级文本")]
        public Text txtNameWithLv;

        [Header("血量bar")]
        public Image imgHpBar;

        [Header("血量文本")]
        public Text txtHp;

        [Header("物理抗性图标")]
        public Image imgPhysicalResistent;

        [Header("法术抗性图标")]
        public Image imgMagicResistent;

        [Header("火元素抗性图标")]
        public Image imgFireResistent;

        [Header("水元素抗性图标")]
        public Image imgWaterResistent;

        [Header("木元素抗性图标")]
        public Image imgWoodResistent;

    }
}
