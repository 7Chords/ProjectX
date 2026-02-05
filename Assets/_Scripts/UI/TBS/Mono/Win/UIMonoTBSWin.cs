using SCFrame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSWin : _ASCUIMonoBase
    {
        [Header("显示时间")]
        public float showDuration;
        [Header("离开按钮")]
        public Button btnExit;
        [Header("经验信息container")]
        public UIMonoCommonContainer monoExpContainer;
        [Header("获得金钱文本")]
        public Text txtMoney;
    }
}
