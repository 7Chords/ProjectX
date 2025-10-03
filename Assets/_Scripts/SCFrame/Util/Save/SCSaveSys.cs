using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    /// <summary>
    /// SCFrame持久化系统
    /// </summary>
    public class SCSaveSys : Singleton<SCSaveSys>
    {
        public SCSaveKeyInfo saveKeyInfo;
        public ELanguageType languageType;
        public override void OnInitialize()
        {
            saveKeyInfo = new SCSaveKeyInfo();
            languageType = ELanguageType.zh_CN;
        }

        public void Save()
        {

        }

        public void Load()
        {

        }
    }
}
