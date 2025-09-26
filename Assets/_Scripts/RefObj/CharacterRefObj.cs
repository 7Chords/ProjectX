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
        public string assetGroupName;
        public string assetObjName;
        public List<ETBSCompType> extraCompList;

        protected override void _parseFromString()
        {
            id = getInt("id");
            characterName = getString("characterName");
            characterProfession = getLong("characterProfession");
            assetGroupName = getString("assetGroupName");
            assetObjName = getString("assetObjName");
            extraCompList = getList<ETBSCompType>("extraCompList");
        }

        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "character";
    }
}
