using SCFrame;
using UnityEngine;
using System.Collections.Generic;
using GameCore.TBS;

namespace GameCore.RefData
{
    public class GameGeneralRefObj : SCRefDataCore
    {
        public GameGeneralRefObj(string _assetPath, string _objName) : base(_assetPath, _objName)
        {
        }
        public List<ETBSCompType> generalCompList;

        protected override void _parseFromString()
        {
            generalCompList = getList<ETBSCompType>("generalCompList");
        }
        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "game_general";
    }
}
