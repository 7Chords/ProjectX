using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RefData
{
    public class StoreRefObj : SCRefDataCore
    {
        public StoreRefObj()
        {

        }
        public StoreRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {
        }

        public long id;
        public string storeName;
        public List<StoreItemEffectObj> itemList;
        protected override void _parseFromString()
        {
            id = getLong("id");
            storeName = getString("storeName");
            itemList = getList<StoreItemEffectObj>("itemList");
        }
        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "store";
    }
}

