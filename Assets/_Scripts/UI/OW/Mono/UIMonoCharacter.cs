using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoCharacter : _ASCUIMonoBase
    {
        [Header("带等级的名字")]
        public Text txtNameWithLv;
        [Header("角色描述")]
        public Text txtCharacterDesc;
        [Header("血量文本")]
        public Text txtHp;
        [Header("魔量文本")]
        public Text txtMp;
        [Header("角色属性组mono")]
        public UIMonoCharacterProps monoCharacterPorps;
        [Header("角色头像容器mono")]
        public UIMonoCommonContainer monoHeaderContainer;
        [Header("角色头像")]
        public Image imgCharacter;
    }
}
