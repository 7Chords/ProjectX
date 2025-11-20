using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.UI
{
    public class UIMonoTBSInfo : _ASCUIMonoBase
    {
        [Header("信息Container")]
        public UIMonoCommonContainer monoContainer;

        [Header("角色切换头像Container")]
        public UIMonoTBSCharacterActionContainer monoCharacterActionContainer;
    }
}
