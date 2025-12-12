using SCFrame.UI;
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
    }

}