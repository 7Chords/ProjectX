using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSInfoContainerItem : _ASCUIMonoBase
    {
        [Header("血量Bar")]
        public Image imgHpBar;

        [Header("魔量Bar")]
        public Image imgMpBar;

        [Header("血量文本")]
        public Text txtHp;

        [Header("魔量文本")]
        public Text txtMp;

        [Header("角色头像")]
        public Image imgCharacterHead;


    }
}
