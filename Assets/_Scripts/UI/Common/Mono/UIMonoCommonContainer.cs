using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    /// <summary>
    /// 通用Container的Mono脚本
    /// </summary>
    public class UIMonoCommonContainer : _ASCUIMonoBase
    {
        [Header("Item模板")]
        public GameObject prefabItem;
        [Header("布局组件")]
        public LayoutGroup layoutGroup;
    }
}
