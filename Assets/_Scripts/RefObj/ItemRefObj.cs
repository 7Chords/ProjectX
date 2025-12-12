using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.RefData
{
    public class ItemRefObj : SCRefDataCore
    {
        public ItemRefObj()
        {

        }
        public ItemRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {
        }
        public long id;
        public string itemName;
        public EItemType itemType;
        public EItemUseType itemUseType;
        public string itemDesc;
        public long itemEffectRefObjId;

        protected override void _parseFromString()
        {
            id = getLong("id");
            itemName = getString("itemName");
            itemType = (EItemType)getEnum("itemType", typeof(EItemType));
            itemUseType = (EItemUseType)getEnum("itemUseType", typeof(EItemUseType));
            itemDesc = getString("itemDesc");
            itemEffectRefObjId = getLong("itemEffectRefObjId");
        }

        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "item";
    }
}

