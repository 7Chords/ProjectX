using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SCFrame
{
    public class TextAutoLocalized : MonoBehaviour
    {
        private Text _m_text;

        private void Awake()
        {
            _m_text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            if (_m_text == null)
                return;
            //框架规定“#1_”为不需要代码控制的翻译key形式
            if (!_m_text.text.StartsWith("#1_"))
                return;
            _m_text.text = LanguageHelper.instance.GetTextTranslate(_m_text.text);
        }
    }
}
