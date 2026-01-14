using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSDetailHeaderContainerItem : _ASCUIMonoBase
    {
        [Header("头像图标")]
        public Image imgHeadIcon;
        [Header("选择当前item时显示的对象列表")]
        public List<GameObject> goSelectShowList;
    }

}