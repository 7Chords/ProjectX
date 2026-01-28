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

    public void InitNew(long _id)
    {
        CharacterRefObj characterRefObj = SCRefDataMgr.instance.characterRefList.refDataList.Find(x => x.id == _id);
        if(characterRefObj == null)
        {
            SCDebugHelper.LogError("找不到id为" + _id + "的配表数据！！！");
            return;
        }
        characterId = _id;
        characterLv = 1;
        curExp = 0;
        skillList = characterRefObj.init_skill_list;
        maxHp = characterRefObj.initHp;
        maxMp = characterRefObj.initMp;
        attack = characterRefObj.initAttack;
        defend = characterRefObj.initDefend;
        missChance = characterRefObj.initMiss;
        criticalChance = characterRefObj.initCritical;
        attackTargetType = characterRefObj.attackTargetType;
        armorLevel = characterRefObj.initArmorLevel;
        magicResistanceLevel = characterRefObj.initMgicResistanceLevel;
        attackDamageType = characterRefObj.attackDamageType;
        attackPhysicalLevel = characterRefObj.attackPhysicalLevel;
        attackMagicAttribute = characterRefObj.attackMagicAttribute;
        weakAttributeList = characterRefObj.weakAttributeList;
        normalAttributeList = characterRefObj.normalAttributeList;
        resistentAttributeList = characterRefObj.resistentAttributeList;
        invilidAttributeList = characterRefObj.invilidAttributeList;
        bounceAttributeList = characterRefObj.bounceAttributeList;
        suckAttributeList = characterRefObj.suckAttributeList;

    }

}

public class StoreData
{
    public long storeId;
    public List<ItemData> dataList;
}
