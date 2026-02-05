using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class UIMonoTBSWinExpItem : _ASCUIMonoBase
    {
        [Header("角色头像图标")]
        public Image imgCharacterHead;
        [Header("角色等级文本")]
        public Text txtCharacterLevel;
        [Header("角色经验条")]
        public Image imgCharacterExpBar;
        [Header("角色升级了显示的物体")]
        public List<GameObject> goLevelUpShowList;
    }
}
