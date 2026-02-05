using GameCore.TBS;
using SCFrame;
using System.Collections.Generic;

public class LevelRefObj : SCRefDataCore
{
    public LevelRefObj()
    {

    }
    public LevelRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
    {
    }
    public long id;
    public long characterId;
    public int characterLevel;
    public List<ETBSCompType> extraCompList;
    public List<long> skill_list;
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
    public int needExpToNextLevel;
    public int dropExp;
    public int dropMoney;
    protected override void _parseFromString()
    {
        id = getInt("id");
        characterId = getLong("characterId");
        characterLevel = getInt("characterLevel");
        extraCompList = getList<ETBSCompType>("extraCompList");
        skill_list = getList<long>("skill_list");
        maxHp = getInt("maxHp");
        maxMp = getInt("maxMp");
        attack = getInt("attack");
        defend = getInt("defend");
        missChance = getFloat("missChance");
        criticalChance = getFloat("criticalChance");
        attackTargetType = (ETargetType)getEnum("attackTargetType", typeof(ETargetType));
        armorLevel = (EArmorLevelType)getEnum("armorLevel", typeof(EArmorLevelType));
        magicResistanceLevel = (EMagicResistanceLevelType)getEnum("magicResistanceLevel", typeof(EMagicResistanceLevelType));
        attackDamageType = (EDamageType)getEnum("attackDamageType", typeof(EDamageType));
        attackPhysicalLevel = (EPhysicalLevelType)getEnum("attackPhysicalLevel", typeof(EPhysicalLevelType));
        attackMagicAttribute = (EMagicAttributeType)getEnum("attackMagicAttribute", typeof(EMagicAttributeType));
        weakAttributeList = getList<EMagicAttributeType>("weakAttributeList");
        normalAttributeList = getList<EMagicAttributeType>("normalAttributeList");
        resistentAttributeList = getList<EMagicAttributeType>("resistentAttributeList");
        invilidAttributeList = getList<EMagicAttributeType>("invilidAttributeList");
        bounceAttributeList = getList<EMagicAttributeType>("bounceAttributeList");
        suckAttributeList = getList<EMagicAttributeType>("suckAttributeList");
        needExpToNextLevel = getInt("needExpToNextLevel");
        dropExp = getInt("dropExp");
        dropMoney = getInt("dropMoney");
    }
    public static string assetPath => "RefData/ExportTxt";

    public static string sheetName => "level";
}
