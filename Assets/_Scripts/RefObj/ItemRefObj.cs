using GameCore.TBS;
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
        public string itemIconObjName;
        public bool isPlayerTarget;
        public EItemType itemType;
        public EItemUseType itemUseType;
        public ETargetType itemTargetType;
        public ETargetAliveType itemTargetAliveType;
        public string itemDesc;
        public long itemEffectRefObjId;

        protected override void _parseFromString()
        {
            id = getLong("id");
            itemName = getString("itemName");
            itemIconObjName = getString("itemIconObjName");
            isPlayerTarget = getBool("isPlayerTarget");
            itemType = (EItemType)getEnum("itemType", typeof(EItemType));
            itemUseType = (EItemUseType)getEnum("itemUseType", typeof(EItemUseType));
            itemTargetType = (ETargetType)getEnum("itemTargetType", typeof(ETargetType));
            itemTargetAliveType = (ETargetAliveType)getEnum("itemTargetAliveType", typeof(ETargetAliveType));
            itemDesc = getString("itemDesc");
            itemEffectRefObjId = getLong("itemEffectRefObjId");
        }

        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "item";
    }
}

