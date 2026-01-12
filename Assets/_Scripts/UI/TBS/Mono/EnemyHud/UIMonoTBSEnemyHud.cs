using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSEnemyHud : _ASCUIMonoBase
    {
        [Header("敌人hudItem资源obj名")]
        public string enemyHudItemObjName;

        [Header("敌人huditem与敌人位置的ui坐标偏移")]
        public Vector3 enemyHudItemOffset;
    }
}
