using SCFrame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public enum EOptionType
    {
        NONE,
        CHARACTER,
        ITEM,
        SETTING,
        EXIT
    }
    [Serializable]
    public class OptionItem
    {
        public EOptionType optionType;
        public Button btnOption;
        public List<GameObject> goSelectShowList;
    }
    public class UIMonoOption : _ASCUIMonoBase
    {
        public List<OptionItem> optionList;
    }
}
