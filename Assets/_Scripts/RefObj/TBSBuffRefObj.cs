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
        public EBuffEffectType buffType;
        public EBasicAttribute affectAttribute;
        public EBuffCallBackPointType callBackPointType;
        public string buffName;
        public bool isPositive;
        public string buffDesc;
        public string buffIconObjName;
        public EPropertyDealSymbolType dealSymbolType;
        public float buffValue;
        protected override void _parseFromString()
        {
            id = getLong("id");
            buffType = (EBuffEffectType)getEnum("buffType", typeof(EBuffEffectType));
            affectAttribute = (EBasicAttribute)getEnum("affectAttribute", typeof(EBasicAttribute));
            callBackPointType = (EBuffCallBackPointType)getEnum("callBackPointType", typeof(EBuffCallBackPointType));
            buffName = getString("buffName");
            isPositive = getBool("isPositive");
            buffDesc = getString("buffDesc");
            buffIconObjName = getString("buffIconObjName");
            dealSymbolType = (EPropertyDealSymbolType)getEnum("dealSymbolType", typeof(EPropertyDealSymbolType));
            buffValue = getFloat("buffValue");
        }

        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "tbs_buff";
    }
}
