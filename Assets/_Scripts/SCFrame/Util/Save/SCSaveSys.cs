using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{

    public class SCSettingData
    {
        public SCSaveKeyInfo saveKeyInfo;
        public ELanguageType languageType;
    }

    public class SCGameData
    {
        public List<ItemData> itemDataList;
    }
    /// <summary>
    /// SCFrame持久化系统
    /// </summary>
    public class SCSaveSys : Singleton<SCSaveSys>
    {

        public SCSettingData settingData;
        public SCGameData gameData;


        private string _m_savePath = Application.streamingAssetsPath + "/Save/";
        private string _m_gameDataFileName = "gameData";
        private string _m_settingFileName = "setting";

        public override void OnInitialize()
        {
            settingData = new SCSettingData();
            settingData.saveKeyInfo = new SCSaveKeyInfo();
            settingData.languageType = ELanguageType.zh_CN;

            //------------
            gameData = new SCGameData();
            gameData.itemDataList = new List<ItemData>();



            loadOrCreate();
        }

        private void save()
        {

        }

        private void load()
        {

        }

        private void loadOrCreate()
        {

        }
    }
}
