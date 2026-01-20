using SCFrame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoDialogue : _ASCUIMonoBase
    {
        [Header("内容文本")]
        public Text txtContent;
        [Header("名字文本")]
        public Text txtName;
        [Header("next标识物体")]
        public GameObject goNext;
    }
}
