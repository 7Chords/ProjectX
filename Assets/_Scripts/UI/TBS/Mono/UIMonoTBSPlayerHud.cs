using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSPlayerHud : _ASCUIMonoBase
    {
        [Header("玩家hudItem资源obj名")]
        public string playerHudItemObjName;

        [Header("玩家huditem与玩家位置的ui坐标偏移")]
        public Vector3 playerHudItemOffset;
    }
}
