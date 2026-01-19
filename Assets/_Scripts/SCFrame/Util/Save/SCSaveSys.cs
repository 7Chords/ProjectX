using GameCore.RefData;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SCFrame
{
    [Serializable]
    public class SCSettingData
    {
        public SCSaveKeyInfo saveKeyInfo;
        public ELanguageType languageType;
    }

    [Serializable]
    public class SCGameData
    {
        public List<ItemData> itemDataList;
        public List<ActorData> playerActorDataList;
    }

    /// <summary>
    /// SCFrame持久化系统
    /// </summary>
    public class SCSaveSys : Singleton<SCSaveSys>
    {
        public SCSettingData settingData;
        public SCGameData gameData;

        private string _m_savePath = Application.streamingAssetsPath + "/Save/";
        private string _m_gameDataFileName = "gameData.json";
        private string _m_settingFileName = "setting.json";

        public override void OnInitialize()
        {
            // 初始化默认配置数据
            settingData = new SCSettingData();
            settingData.saveKeyInfo = new SCSaveKeyInfo();
            settingData.languageType = ELanguageType.zh_CN;

            // 初始化默认游戏数据
            gameData = new SCGameData();
            gameData.itemDataList = new List<ItemData>();
            gameData.playerActorDataList = new List<ActorData>();
            GameInitRefObj initRefObj = SCRefDataMgr.instance.gameInitRefObj;
            if (initRefObj == null)
                return;
            for(int i = 0; i < initRefObj.init_player_team_list.Count; i++)
            {
                ActorData data = new ActorData();
                data.InitNew(initRefObj.init_player_team_list[i]);
                gameData.playerActorDataList.Add(data);
            }


            // 加载或创建存档文件
            loadOrCreate();
        }

        /// <summary>
        /// 保存当前所有数据到本地文件
        /// </summary>
        public void Save()
        {
            try
            {
                //确保保存目录存在，不存在则创建
                if (!Directory.Exists(_m_savePath))
                {
                    Directory.CreateDirectory(_m_savePath);
                }

                //序列化SCSettingData为Json字符串并写入文件
                string settingJson = JsonUtility.ToJson(settingData, true); //格式化Json，便于阅读
                string settingFilePath = Path.Combine(_m_savePath, _m_settingFileName);
                File.WriteAllText(settingFilePath, settingJson);

                //序列化SCGameData为Json字符串并写入文件
                string gameJson = JsonUtility.ToJson(gameData, true);
                string gameFilePath = Path.Combine(_m_savePath, _m_gameDataFileName);
                File.WriteAllText(gameFilePath, gameJson);

                SCDebugHelper.Log("存档保存成功，路径：" + _m_savePath);
            }
            catch (Exception e)
            {
                SCDebugHelper.LogError("存档保存失败：" + e.Message);
            }
        }

        /// <summary>
        /// 从本地文件加载数据（文件不存在则不修改当前数据）
        /// </summary>
        private void load()
        {
            try
            {
                //加载配置数据
                string settingFilePath = Path.Combine(_m_savePath, _m_settingFileName);
                if (File.Exists(settingFilePath))
                {
                    string settingJson = File.ReadAllText(settingFilePath);
                    //反序列化Json到已初始化的对象（保留对象引用，避免空指针）
                    JsonUtility.FromJsonOverwrite(settingJson, settingData);
                    SCDebugHelper.Log("配置数据加载成功");
                }
                else
                {
                    SCDebugHelper.LogWarning("配置文件不存在，将使用默认配置");
                }

                //加载游戏数据
                string gameFilePath = Path.Combine(_m_savePath, _m_gameDataFileName);
                if (File.Exists(gameFilePath))
                {
                    string gameJson = File.ReadAllText(gameFilePath);
                    //反序列化Json到已初始化的对象
                    JsonUtility.FromJsonOverwrite(gameJson, gameData);
                    SCDebugHelper.Log("游戏数据加载成功");
                }
                else
                {
                    SCDebugHelper.LogWarning("游戏存档文件不存在，将使用默认游戏数据");
                }
            }
            catch (Exception e)
            {
                SCDebugHelper.LogError("存档加载失败：" + e.Message);
            }
        }

        /// <summary>
        /// 加载本地存档，无存档则创建默认存档并保存
        /// </summary>
        private void loadOrCreate() //修正命名规范：首字母大写
        {
            //尝试加载本地存档
            load();

            //如果存档文件不存在，保存当前默认数据作为初始存档
            string settingFilePath = Path.Combine(_m_savePath, _m_settingFileName);
            string gameFilePath = Path.Combine(_m_savePath, _m_gameDataFileName);
            if (!File.Exists(settingFilePath) || !File.Exists(gameFilePath))
            {
                Save();
                SCDebugHelper.Log("已创建默认存档并保存");
            }
        }
    }
}
