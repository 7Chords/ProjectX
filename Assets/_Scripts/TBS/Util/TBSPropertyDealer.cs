using SCFrame;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.TBS
{
    public enum EPropertyDealSymbolType
    {
        NONE,
        MULTIPLY,
        ADD,
        LEVEL_UP,
    }
    public struct PropertyDealItem
    {
        public EBasicAttribute attribute;
        public EPropertyDealSymbolType dealSymbol;
        public float value;

        /// <summary>
        /// 属性处理项
        /// </summary>
        /// <param name="_attribute"></param>
        /// <param name="_dealSymbol"></param>
        /// <param name="_value"></param>
        public PropertyDealItem(EBasicAttribute _attribute, EPropertyDealSymbolType _dealSymbol, float _value)
        {
            attribute = _attribute;
            dealSymbol = _dealSymbol;
            value = _value;
        }

        //public bool Equals(PropertyDealItem x, PropertyDealItem y)
        //{
        //    return x.attribute == y.attribute && x.dealSymbol == y.dealSymbol && x.value == y.value;
        //}

        //public int GetHashCode(PropertyDealItem obj)
        //{
        //    return obj.GetHashCode();
        //}
    }
    /// <summary>
    /// 回合制战斗属性处理器 输出经过处理后的玩家的攻击力 防御力等属性
    /// </summary>
    public class TBSPropertyDealer : _ASCLifeObjBase
    {

        private TBSActorInfo _actorInfo;

        private List<PropertyDealItem> m_attackDealItemList;
        private List<PropertyDealItem> m_defendDealItemList;
        private List<PropertyDealItem> m_missDealItemList;
        private List<PropertyDealItem> m_criticalChanceDealItemList;
        private List<PropertyDealItem> m_armorDealItemList;
        private List<PropertyDealItem> m_magicResistenceDealItemList;
        private List<PropertyDealItem> m_physicsLevelDealItemList;
        private EMagicAttributeType _m_curNormalAttackAttributeType;//todo

        public override void OnInitialize()
        {
            m_attackDealItemList = new List<PropertyDealItem>();
            m_defendDealItemList = new List<PropertyDealItem>();
            m_missDealItemList = new List<PropertyDealItem>();
            m_criticalChanceDealItemList = new List<PropertyDealItem>();
            m_armorDealItemList = new List<PropertyDealItem>();
            m_magicResistenceDealItemList = new List<PropertyDealItem>();
            m_physicsLevelDealItemList = new List<PropertyDealItem>();
        }
        public override void OnDiscard()
        {
        }

        public override void OnResume()
        {
        }

        public override void OnSuspend()
        {
        }

        public void SetActorInfo(TBSActorInfo _info)
        {
            _actorInfo = _info;
        }


        /// <summary>
        /// 添加属性处理项
        /// </summary>
        /// <param name="_attribute"></param>
        /// <param name="_dealSymbol"></param>
        /// <param name="_value"></param>
        public void AddDealItem(EBasicAttribute _attribute, EPropertyDealSymbolType _dealSymbol, float _value)
        {
            PropertyDealItem item = new PropertyDealItem(_attribute, _dealSymbol,_value);
            switch (_attribute)
            {
                case EBasicAttribute.ATTACK:
                    {
                        m_attackDealItemList.Add(item);
                    }
                    break;
                case EBasicAttribute.DEFEND:
                    {
                        m_defendDealItemList.Add(item);
                    }
                    break;
                case EBasicAttribute.MISS:
                    {
                        m_missDealItemList.Add(item);
                    }
                    break;
                case EBasicAttribute.CRITICAL_CHANCE:
                    {
                        m_criticalChanceDealItemList.Add(item);
                    }
                    break;
                case EBasicAttribute.ARMOR:
                    {
                        m_armorDealItemList.Add(item);
                    }
                    break;
                case EBasicAttribute.MAGIC_RESISTENCE:
                    {
                        m_magicResistenceDealItemList.Add(item);
                    }
                    break;
                case EBasicAttribute.PHYSICAL_LEVEL:
                    {
                        m_physicsLevelDealItemList.Add(item);
                    }
                    break;
                default:
                    break;
            }
            refreshActorInfo();
        }

        /// <summary>
        /// 移除属性处理项
        /// </summary>
        /// <param name="_attribute"></param>
        /// <param name="_dealSymbol"></param>
        /// <param name="_value"></param>
        public void RemoveDealItem(EBasicAttribute _attribute, EPropertyDealSymbolType _dealSymbol, float _value)
        {
            PropertyDealItem item = new PropertyDealItem(_attribute, _dealSymbol, _value);
            switch (_attribute)
            {
                case EBasicAttribute.ATTACK:
                    {
                        if (m_attackDealItemList.Contains(item))
                            m_attackDealItemList.Remove(item);
                    }
                    break;
                case EBasicAttribute.DEFEND:
                    {
                        if (m_defendDealItemList.Contains(item))
                            m_defendDealItemList.Remove(item);
                    }
                    break;
                case EBasicAttribute.MISS:
                    {
                        if (m_missDealItemList.Contains(item))
                            m_missDealItemList.Remove(item);
                    }
                    break;
                case EBasicAttribute.CRITICAL_CHANCE:
                    {
                        if (m_criticalChanceDealItemList.Contains(item))
                            m_criticalChanceDealItemList.Remove(item);
                    }
                    break;
                case EBasicAttribute.ARMOR:
                    {
                        if (m_armorDealItemList.Contains(item))
                            m_armorDealItemList.Remove(item);
                    }
                    break;
                case EBasicAttribute.MAGIC_RESISTENCE:
                    {
                        if (m_magicResistenceDealItemList.Contains(item))
                            m_magicResistenceDealItemList.Remove(item);
                    }
                    break;
                case EBasicAttribute.PHYSICAL_LEVEL:
                    {
                        if (m_physicsLevelDealItemList.Contains(item))
                            m_physicsLevelDealItemList.Remove(item);
                    }
                    break;
                default:
                    break;
            }
            refreshActorInfo();
        }


        /// <summary>
        /// 刷新actorinfo数据
        /// </summary>
        private void refreshActorInfo()
        {
            if (_actorInfo == null)
                return;
            _actorInfo.attack = getResultAttack();
            _actorInfo.defend = getResultDefend();
            _actorInfo.missChance = getResultMiss();
            _actorInfo.criticalChance = getResultCriticalChance();
            _actorInfo.armorLevel = getResultArmorLevel();
            _actorInfo.attackPhysicalLevel = getResultPhysicsLevel();
            _actorInfo.magicResistanceLevel = getResultMagicResistenceLevel();
        }

        /// <summary>
        /// 获取处理后的攻击力
        /// </summary>
        /// <returns></returns>
        private int getResultAttack()
        {
            if (_actorInfo == null)
                return 0;
            int resAttack = _actorInfo.attack;

            for (int i = 0; i < m_attackDealItemList.Count; i++)
            {
                switch (m_attackDealItemList[i].dealSymbol)
                {
                    case EPropertyDealSymbolType.MULTIPLY:
                        resAttack = Mathf.RoundToInt(resAttack * m_attackDealItemList[i].value);
                        break;
                    case EPropertyDealSymbolType.ADD:
                        resAttack = Mathf.RoundToInt(resAttack + m_attackDealItemList[i].value);
                        break;
                    default:
                        break;
                }
            }
            return resAttack;
        }

        private int getResultDefend()
        {
            if (_actorInfo == null)
                return 0;
            int resDefend = _actorInfo.defend;

            for (int i = 0; i < m_defendDealItemList.Count; i++)
            {
                switch (m_defendDealItemList[i].dealSymbol)
                {
                    case EPropertyDealSymbolType.MULTIPLY:
                        resDefend = Mathf.RoundToInt(resDefend * m_defendDealItemList[i].value);
                        break;
                    case EPropertyDealSymbolType.ADD:
                        resDefend = Mathf.RoundToInt(resDefend + m_defendDealItemList[i].value);
                        break;
                    default:
                        break;
                }
            }
            return resDefend;
        }

        private float getResultMiss()
        {
            if (_actorInfo == null)
                return 0;
            float resMiss = _actorInfo.missChance;

            for (int i = 0; i < m_missDealItemList.Count; i++)
            {
                switch (m_missDealItemList[i].dealSymbol)
                {
                    case EPropertyDealSymbolType.MULTIPLY:
                        resMiss = Mathf.RoundToInt(resMiss * m_missDealItemList[i].value);
                        break;
                    case EPropertyDealSymbolType.ADD:
                        resMiss = Mathf.RoundToInt(resMiss + m_missDealItemList[i].value);
                        break;
                    default:
                        break;
                }
            }
            return resMiss;
        }
        private float getResultCriticalChance()
        {
            if (_actorInfo == null)
                return 0;
            float resCriticalChance = _actorInfo.criticalChance;

            for (int i = 0; i < m_criticalChanceDealItemList.Count; i++)
            {
                switch (m_criticalChanceDealItemList[i].dealSymbol)
                {
                    case EPropertyDealSymbolType.MULTIPLY:
                        resCriticalChance = Mathf.RoundToInt(resCriticalChance * m_criticalChanceDealItemList[i].value);
                        break;
                    case EPropertyDealSymbolType.ADD:
                        resCriticalChance = Mathf.RoundToInt(resCriticalChance + m_criticalChanceDealItemList[i].value);
                        break;
                    default:
                        break;
                }
            }
            return resCriticalChance;
        }

        private EArmorLevelType getResultArmorLevel()
        {
            if (_actorInfo == null)
                return EArmorLevelType.NONE;
            int resArmorLevel = (int)_actorInfo.armorLevel;

            for (int i = 0; i < m_armorDealItemList.Count; i++)
            {
                switch (m_armorDealItemList[i].dealSymbol)
                {
                    case EPropertyDealSymbolType.LEVEL_UP:
                        resArmorLevel = Mathf.Clamp(resArmorLevel + (int)m_armorDealItemList[i].value,
                            (int)EArmorLevelType.LIGHT, (int)EArmorLevelType.HERO);
                        break;
                    default:
                        break;
                }
            }
            return (EArmorLevelType)resArmorLevel;

        }

        private EMagicResistanceLevelType getResultMagicResistenceLevel()
        {
            if (_actorInfo == null)
                return EMagicResistanceLevelType.NONE;
            int resMagicResistence = (int)_actorInfo.magicResistanceLevel;

            for (int i = 0; i < m_magicResistenceDealItemList.Count; i++)
            {
                switch (m_magicResistenceDealItemList[i].dealSymbol)
                {
                    case EPropertyDealSymbolType.LEVEL_UP:
                        resMagicResistence = Mathf.Clamp(resMagicResistence + (int)m_magicResistenceDealItemList[i].value,
                            (int)EMagicResistanceLevelType.LIGHT, (int)EMagicResistanceLevelType.HERO);
                        break;
                    default:
                        break;
                }
            }
            return (EMagicResistanceLevelType)resMagicResistence;
        }

        private EPhysicalLevelType getResultPhysicsLevel()
        {
            if (_actorInfo == null)
                return EPhysicalLevelType.NONE;
            int resPhysicsLevel = (int)_actorInfo.attackPhysicalLevel;

            for (int i = 0; i < m_physicsLevelDealItemList.Count; i++)
            {
                switch (m_physicsLevelDealItemList[i].dealSymbol)
                {
                    case EPropertyDealSymbolType.LEVEL_UP:
                        resPhysicsLevel = Mathf.Clamp(resPhysicsLevel + (int)m_physicsLevelDealItemList[i].value,
                            (int)EPhysicalLevelType.LIGHT, (int)EPhysicalLevelType.HERO);
                        break;
                    default:
                        break;
                }
            }
            return (EPhysicalLevelType)resPhysicsLevel;
        }
    }

}