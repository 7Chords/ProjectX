using SCFrame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSDetailBuffContainerItem : _ASCUIMonoBase
    {
        [Header("buff图标")]
        public Image imgBuffIcon;
        [Header("剩余回合数文本")]
        public Text txtRemainTurn;
    }
}
