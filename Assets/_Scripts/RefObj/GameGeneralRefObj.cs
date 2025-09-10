using SCFrame;
using UnityEngine;


namespace GameCore.RefData
{
    public class GameGeneralRefObj : SCRefDataCore
    {
        public GameGeneralRefObj(string _assetPath, string _objName) : base(_assetPath, _objName)
        {
        }


        public string testStr;
        public int testInt;
        public float testFloat;
        public Vector2 testVec2;
        protected override void _parseFromString()
        {
            testStr = getString("testStr");
            testInt = getInt("testInt");
            testFloat = getFloat("testFloat");
            testVec2 = getVector2("testVec2");
        }
        protected override string _assetPath => "RefData/ExportTxt";

        protected override string _objName => "game_general_core;";
    }
}
