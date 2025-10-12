using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RefData
{
    public class UIResPathRefObj : SCRefDataCore
    {
        public UIResPathRefObj()
        {

        }
        public UIResPathRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {
        }

        public long id;
        public string uiName;
        public string uiResObjName;

        protected override void _parseFromString()
        {
            id = getLong("id");
            uiName = getString("uiName");
            uiResObjName = getString("uiResObjName");
        }

        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "ui_res_path";
    }
}
