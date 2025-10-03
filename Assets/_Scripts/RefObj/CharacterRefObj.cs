using GameCore.TBS;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.RefData
{
    public class CharacterRefObj : SCRefDataCore
    {
        public CharacterRefObj()
        {

        }
        public CharacterRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {
        }

        public long id;
        public string characterName;
        public long characterProfession;
        public string assetModelObjName;
        public string assetHeadIconObjName;

        public List<ETBSCompType> extraCompList;
        public List<long> init_skill_list;
        public int initHp;
        public int initMp;
        public int initAttack;
        public int initDefend;
        public float initMiss;
        public EArmorLevelType initArmorLevel;
        public EMagicResistanceLevelType initMgicResistanceLevel;
        protected override void _parseFromString()
        {
            id = getInt("id");
            characterName = getString("characterName");
            characterProfession = getLong("characterProfession");
            assetModelObjName = getString("assetModelObjName");
            assetHeadIconObjName = getString("assetHeadIconObjName");
            extraCompList = getList<ETBSCompType>("extraCompList");
            init_skill_list = getList<long>("init_skill_list");
            initHp = getInt("initHp");
            initMp = getInt("initMp");
            initAttack = getInt("initAttack");
            initDefend = getInt("initDefend");
            initMiss = getFloat("initMiss");
            initArmorLevel = (EArmorLevelType)getEnum("initArmorLevel", typeof(EArmorLevelType));
            initMgicResistanceLevel = (EMagicResistanceLevelType)getEnum("initMgicResistanceLevel", typeof(EMagicResistanceLevelType));
        }

        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "character";
    }
}
