using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSItem : _ASCUIMonoBase
    {
        [Header("道具Container")]
        public UIMonoCommonContainer monoContainer;
        [Header("道具描述文本")]
        public Text txtItemDesc;
        [Header("有道具时显示的物体")]
        public List<GameObject> goHasItemShowList;
        [Header("没有道具时显示的物体")]
        public List<GameObject> goNoItemShowList;
    }

}