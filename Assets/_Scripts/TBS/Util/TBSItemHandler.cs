using GameCore.RefData;
using SCFrame;
using System.Collections.Generic;

namespace GameCore.TBS
{
    public static class TBSItemHandler
    {
        public static void DealItem(ItemRefObj _itemRefObj,List<TBSActorBase> _targetList)
        {
            if (_itemRefObj == null || _targetList == null)
                return;
            //只处理战斗中用的道具
            if (_itemRefObj.itemType != EItemType.BATTLE)
                return;
            BattleItemEffectRefObj itemEffectRefObj = SCRefDataMgr.instance.battleItemEffectRefList.refDataList.Find(x => x.id == _itemRefObj.itemEffectRefObjId);
            if(itemEffectRefObj == null)
            {
                SCDebugHelper.LogError("找不到id为" +_itemRefObj.itemEffectRefObjId + "的BattleItemEffectRefObj！！！");
                return;
            }
            switch (itemEffectRefObj.effectType)
            {
                case EBattleItemEffectType.BASIC_CHG:
                    {
                        for(int i =0;i< itemEffectRefObj.basicChgEffectList.Count;i++)
                        {
                            dealBasicChgEffect(itemEffectRefObj.basicChgEffectList[i], _targetList);
                        }
                    }
                    break;
                case EBattleItemEffectType.BUFF:
                    {
                        for (int i = 0; i < itemEffectRefObj.buffEffectList.Count; i++)
                        {
                            dealBuffEffect(itemEffectRefObj.buffEffectList[i], _targetList);
                        }
                    }
                    break;
                case EBattleItemEffectType.SPECIAL:
                    {
                        dealSpecialEffect(itemEffectRefObj, _targetList);
                    }
                    break;
                default:
                    break;
            }
        }

        private static void dealBasicChgEffect(BasicChgEffectObj _basicChgEffectObj,List<TBSActorBase> _targetList)
        {
            switch (_basicChgEffectObj.basicAttribute)
            {
                case EBasicAttribute.HP:
                    {
                        foreach (var actor in _targetList)
                        {
                            if (actor != null)
                            {
                                actor.HealHp((int)_basicChgEffectObj.changeValue);
                            }
                        }
                    }
                    break;
                case EBasicAttribute.MP:
                    {
                        foreach (var actor in _targetList)
                        {
                            if (actor != null)
                            {
                                actor.HealMp((int)_basicChgEffectObj.changeValue);
                            }
                        }
                    }
                    break;
                case EBasicAttribute.ATTACK:
                    break;
                case EBasicAttribute.DEFEND:
                    break;
                case EBasicAttribute.MISS:
                    break;
                case EBasicAttribute.CRITICAL_CHANCE:
                    break;
                case EBasicAttribute.ARMOR:
                    break;
                case EBasicAttribute.MAGIC_RESISTENCE:
                    break;
                case EBasicAttribute.PHYSICAL_LEVEL:
                    break;
                case EBasicAttribute.MAGIC_ATTRIBUTE:
                    break;
            }
        }

        private static void dealBuffEffect(BuffEffectObj _buffEffectObj, List<TBSActorBase> _targetList)
        {
            for(int i =0;i<_targetList.Count;i++)
            {
                _targetList[i].GetBuff(TBSBuffFactory.CreateBuffInfo(_buffEffectObj.buffRefObjId, _buffEffectObj.continueTurn, _targetList[i]));
            }
        }

        private static void dealSpecialEffect(BattleItemEffectRefObj _itemEffectRefObj, List<TBSActorBase> _targetList)
        {
            switch(_itemEffectRefObj.id)
            { 
                case 1003://复活效果 回复50%
                    {
                        foreach(var actor in _targetList)
                        {
                            actor.Rebirth(0.5f);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

    }
}
