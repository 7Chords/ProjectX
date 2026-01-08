using GameCore.RefData;
using SCFrame;
using System;

namespace GameCore.TBS
{
    public static class TBSBuffFactory
    {

        public static TBSGameBuffInfo CreateBuffInfo(long _buffId,int _continueCount,TBSActorBase _targetActor)
        {
            TBSBuffRefObj buffRefObj = SCRefDataMgr.instance.tbsBuffRefList.refDataList.Find(x => x.id == _buffId);
            if(buffRefObj == null)
            {
                SCDebugHelper.LogError("找不到id为" + _buffId + "的buff配表数据！！！");
                return null;
            }

            TBSGameBuffInfo gameBuffInfo = new TBSGameBuffInfo(_targetActor,buffRefObj, _continueCount);
            processingBuffInfo(gameBuffInfo, _targetActor);

            return gameBuffInfo;
        }

        private static void processingBuffInfo(TBSGameBuffInfo _buffInfo, TBSActorBase _targetActor)
        {
            if (_buffInfo == null)
                return;
            switch (_buffInfo.buffRefObj.callBackPointType)
            {
                case EBuffCallBackPointType.NONE:
                    break;
                case EBuffCallBackPointType.ADD:
                    {
                        _buffInfo.onBuffAdd += getOnBuffAction(_buffInfo.buffRefObj, _targetActor);

                        //特殊处理 如攻击力提升等 在buff结束时要移除效果
                        _buffInfo.onBuffRemove += getOnBuffFinish(_buffInfo.buffRefObj, _targetActor);
                    }
                    break;
                case EBuffCallBackPointType.REMOVE:
                    {
                        _buffInfo.onBuffRemove += getOnBuffAction(_buffInfo.buffRefObj, _targetActor);
                    }
                    break;
                case EBuffCallBackPointType.ATTACK:
                    {
                        _buffInfo.onAttack += getOnBuffAction(_buffInfo.buffRefObj, _targetActor);
                    }
                    break;
                case EBuffCallBackPointType.GET_HIT:
                    {
                        _buffInfo.onGetHit += getOnBuffAction(_buffInfo.buffRefObj, _targetActor);
                    }
                    break;
                case EBuffCallBackPointType.DIE:
                    {
                        _buffInfo.onActorDie += getOnBuffAction(_buffInfo.buffRefObj, _targetActor);
                    }
                    break;
                case EBuffCallBackPointType.ACTION:
                    {
                        {
                            _buffInfo.onActorAction += getOnBuffAction(_buffInfo.buffRefObj, _targetActor);
                        }
                    }
                    break;
            }
        }
        /// <summary>
        /// 获取buff的效果回调
        /// </summary>
        /// <param name="_buffRefObj"></param>
        /// <param name="_targetActor"></param>
        /// <returns></returns>
        private static Action getOnBuffAction(TBSBuffRefObj _buffRefObj, TBSActorBase _targetActor)
        {
            if (_buffRefObj == null)
                return null;
            switch (_buffRefObj.buffType)
            {
                case EBuffEffectType.NONE:
                    return null;
                case EBuffEffectType.ATTRIBUTE_CHG:
                    {
                        switch (_buffRefObj.affectAttribute)
                        {
                            case EBasicAttribute.ATTACK:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.AddDealItem(EBasicAttribute.ATTACK, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);;
                                    };
                                }
                            case EBasicAttribute.DEFEND:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.AddDealItem(EBasicAttribute.DEFEND, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.MISS:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.AddDealItem(EBasicAttribute.MISS, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.CRITICAL_CHANCE:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.AddDealItem(EBasicAttribute.CRITICAL_CHANCE, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.ARMOR:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.AddDealItem(EBasicAttribute.ARMOR, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.MAGIC_RESISTENCE:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.AddDealItem(EBasicAttribute.MAGIC_RESISTENCE, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.PHYSICAL_LEVEL:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.AddDealItem(EBasicAttribute.PHYSICAL_LEVEL, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.MAGIC_ATTRIBUTE:
                                break;
                        }
                    }
                    return null;
                case EBuffEffectType.DAMAGE:
                    return null;
                case EBuffEffectType.SPECIAL:
                    return null;
            }
            return null;
        }

        /// <summary>
        /// 获取buff结束的效果回调
        /// </summary>
        /// <param name="_buffRefObj"></param>
        /// <param name="_targetActor"></param>
        /// <returns></returns>
        private static Action getOnBuffFinish(TBSBuffRefObj _buffRefObj, TBSActorBase _targetActor)
        {
            if (_buffRefObj == null)
                return null;
            switch (_buffRefObj.buffType)
            {
                case EBuffEffectType.NONE:
                    return null;
                case EBuffEffectType.ATTRIBUTE_CHG:
                    {
                        switch (_buffRefObj.affectAttribute)
                        {
                            case EBasicAttribute.ATTACK:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.RemoveDealItem(EBasicAttribute.ATTACK, _buffRefObj.dealSymbolType, _buffRefObj.buffValue); ;
                                    };
                                }
                            case EBasicAttribute.DEFEND:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.RemoveDealItem(EBasicAttribute.DEFEND, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.MISS:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.RemoveDealItem(EBasicAttribute.MISS, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.CRITICAL_CHANCE:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.RemoveDealItem(EBasicAttribute.CRITICAL_CHANCE, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.ARMOR:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.RemoveDealItem(EBasicAttribute.ARMOR, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.MAGIC_RESISTENCE:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.RemoveDealItem(EBasicAttribute.MAGIC_RESISTENCE, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.PHYSICAL_LEVEL:
                                {
                                    return () =>
                                    {
                                        _targetActor.propertyDealer.RemoveDealItem(EBasicAttribute.PHYSICAL_LEVEL, _buffRefObj.dealSymbolType, _buffRefObj.buffValue);
                                    };
                                }
                            case EBasicAttribute.MAGIC_ATTRIBUTE:
                                break;
                        }
                    }
                    return null;
                case EBuffEffectType.DAMAGE:
                    return null;
                case EBuffEffectType.SPECIAL:
                    return null;
            }
            return null;
        }
    }
} 