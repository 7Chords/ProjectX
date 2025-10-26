using GameCore.TBS;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RefData
{
    public class TBSBuffRefObj : SCRefDataCore
    {
        public TBSBuffRefObj()
        {

        }
        public TBSBuffRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {
        }
        public long id;
        public EBuffType buffType;
        public string buffName;
        public bool isGood;
        public string buffDesc;
        public string buffIconObjName;
        protected override void _parseFromString()
        {
            id = getLong("id");
            buffType = (EBuffType)getEnum("buffType", typeof(EBuffType));
            buffName = getString("buffName");
            isGood = getBool("isGood");
            buffDesc = getString("buffDesc");
            buffIconObjName = getString("buffIconObjName");
        }

        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "tbs_buff";
    }
}
