using GameCore.RefData;
using GameCore.TBS;
using SCFrame;
using System.Collections.Generic;

public class ItemData
{
    public long itemId;
    public long itemAmount;

    public ItemData(long _itemId, long _itemAmount)
    {
        this.itemId = _itemId;
        this.itemAmount = _itemAmount;
    }
}

public class ActorData
{
    public long characterId;
    public int characterLv;
    public int curExp;
    public List<long> skillList;
    public int maxHp;
    public int maxMp;
    public int attack;
    public int defend;
    public float missChance;
    public float criticalChance;
    public ETargetType attackTargetType;
    public EArmorLevelType armorLevel;
    public EMagicResistanceLevelType magicResistanceLevel;
    public EDamageType attackDamageType;
    public EPhysicalLevelType attackPhysicalLevel;
    public EMagicAttributeType attackMagicAttribute;
    public List<EMagicAttributeType> weakAttributeList;
    public List<EMagicAttributeType> normalAttributeList;
    public List<EMagicAttributeType> resistentAttributeList;
    public List<EMagicAttributeType> invilidAttributeList;
    public List<EMagicAttributeType> bounceAttributeList;
    public List<EMagicAttributeType> suckAttributeList;

    public void InitNew(long _id,int _level)
    {
        CharacterRefObj characterRefObj = SCRefDataMgr.instance.characterRefList.refDataList.Find(x => x.id == _id);
        if(characterRefObj == null)
        {
            SCDebugHelper.LogError("找不到id为" + _id + "的配表数据！！！");
            return;
        }
        LevelRefObj levelRefObj = SCRefDataMgr.instance.levelRefList.refDataList.Find(x => (x.characterId == _id && x.characterLevel == _level));
        if (levelRefObj == null)
            return;
        characterId = _id;
        characterLv = _level;
        curExp = 0;
        skillList = levelRefObj.skill_list;
        maxHp = levelRefObj.maxHp;
        maxMp = levelRefObj.maxMp;
        attack = levelRefObj.attack;
        defend = levelRefObj.defend;
        missChance = levelRefObj.missChance;
        criticalChance = levelRefObj.criticalChance;
        attackTargetType = levelRefObj.attackTargetType;
        armorLevel = levelRefObj.armorLevel;
        magicResistanceLevel = levelRefObj.magicResistanceLevel;
        attackDamageType = levelRefObj.attackDamageType;
        attackPhysicalLevel = levelRefObj.attackPhysicalLevel;
        attackMagicAttribute = levelRefObj.attackMagicAttribute;
        weakAttributeList = levelRefObj.weakAttributeList;
        normalAttributeList = levelRefObj.normalAttributeList;
        resistentAttributeList = levelRefObj.resistentAttributeList;
        invilidAttributeList = levelRefObj.invilidAttributeList;
        bounceAttributeList = levelRefObj.bounceAttributeList;
        suckAttributeList = levelRefObj.suckAttributeList;

    }

}

public class StoreData
{
    public long storeId;
    public List<ItemData> dataList;
}
