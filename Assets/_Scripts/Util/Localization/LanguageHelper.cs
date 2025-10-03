using GameCore.RefData;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    public class LanguageHelper:Singleton<LanguageHelper>
    {
        private Dictionary<string, TextLanguageRefObj> _m_textLanguageRefDict = new Dictionary<string, TextLanguageRefObj>();

        public override void OnInitialize()
        {
            _m_textLanguageRefDict = new Dictionary<string, TextLanguageRefObj>();
            //保存一份 避免每次翻译遍历
            foreach(var refObj in SCRefDataMgr.instance.textLanguageRefList.refDataList)
            {
                _m_textLanguageRefDict.Add(refObj.translateKey, refObj);
            }

        }
        public string GetTextTranslate(string _translateKey,params object[] _objs)
        {
            if(!_m_textLanguageRefDict.TryGetValue(_translateKey,out TextLanguageRefObj refObj))
            {
                return "translate is not find in dict!!!";
            }
            string localStr = "";
            switch (SCSaveSys.instance.languageType)
            {
                case ELanguageType.zh_CN:
                    localStr = refObj.zh_CN;
                    break;
                case ELanguageType.en_US:
                    localStr = refObj.en_US;
                    break;
                default:
                    return "text with language does not config!!!";
            }

            try
            {
                // 使用 string.Format 来替换 {0}、{1} 等占位符
                return string.Format(localStr, _objs);
            }
            catch
            {
                // 如果格式化失败，返回原始字符串
                Debug.LogWarning($"Format failed for translation key: {_translateKey}");
                return localStr;
            }

        }
    }
}
