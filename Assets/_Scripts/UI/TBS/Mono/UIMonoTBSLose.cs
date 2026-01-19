using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSLose : _ASCUIMonoBase
    {
        [Header("显示时间")]
        public float showDuration;
        [Header("重试按钮")]
        public Button btnRetry;
        [Header("退出按钮")]
        public Button btnExit;
    }
}
