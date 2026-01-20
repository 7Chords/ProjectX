using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RefData
{
    public class DialogueRefObj : SCRefDataCore
    {
        public DialogueRefObj()
        {

        }
        public DialogueRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {
        }
        public long id;
        public long group;
        public string characterName;
        public string content;

        protected override void _parseFromString()
        {
            id = getLong("id");
            group = getLong("group");
            characterName = getString("characterName");
            content = getString("content");
        }

        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "dialogue";
    }

}