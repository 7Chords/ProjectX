using GameCore.TBS;
using SCFrame;
using System.Collections.Generic;

namespace GameCore.RefData
{
    public class BattleItemEffectRefObj : SCRefDataCore
    {
        public BattleItemEffectRefObj()
        {

        }
        public BattleItemEffectRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {
        }

        public long id;
        public EBattleItemEffectType effectType;
        public List<BasicChgEffectObj> basicChgEffectList;
        public List<BuffEffectObj> buffEffectList;
        protected override void _parseFromString()
        {
            id = getLong("id");
            effectType = (EBattleItemEffectType)getEnum("effectType", typeof(EBattleItemEffectType));
            basicChgEffectList = getList<BasicChgEffectObj>("basicChgEffectList");
            buffEffectList = getList<BuffEffectObj>("buffEffectList");

        }
        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "battle_item_effect";
    }
}
