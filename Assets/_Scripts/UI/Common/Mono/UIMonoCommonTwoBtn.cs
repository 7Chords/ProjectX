using SCFrame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoCommonTwoBtn : _ASCUIMonoBase
    {
        [Header("内容文本")]
        public Text txtContent;
        [Header("左边按钮")]
        public Button btnLeft;
        [Header("右边按钮")]
        public Button btnRight;
        [Header("左边文本")]
        public Text txtLeft;
        [Header("右边文本")]
        public Text txtRight;
    }
}
