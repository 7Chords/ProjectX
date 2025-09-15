using SCFrame;
using UnityEngine;


namespace GameCore.RefData
{
    public class GameGeneralRefObj : SCRefDataCore
    {
        public GameGeneralRefObj(string _assetPath, string _objName) : base(_assetPath, _objName)
        {
        }
        public int test;
        public string test22;

        protected override void _parseFromString()
        {
            test = getInt("test");
            test22 = getString("test22");
        }
        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "game_general";
    }
}
