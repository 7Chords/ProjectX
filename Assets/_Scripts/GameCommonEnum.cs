namespace GameCore
{
    /// <summary>
    /// 游戏通用枚举
    /// </summary>
    

    public enum EBasicAttribute
    {
        HP,
        MP,
        ATTACK,
        DEFEND,
        MISS,
        CRITICAL_CHANCE,
        ARMOR,
        MAGIC_RESISTENCE,
        PHYSICAL_LEVEL,
        MAGIC_ATTRIBUTE,
    }


    public enum EProfessionType
    {
        WARRIOR,//战士
        ARCHER,//射手
        MAGE,//法师
        KNIGHT,//骑士
        ASSASSIN,//刺客
        MONSTER,//猛兽
        TROLL,//魔兽
        GIANT,//巨人
        ELF,//精灵
    }


    public enum EItemUseType
    {
        NONE,
        CAN_NOT_USE,
        BATTLE_USE,
        WORLD_USE
    }

    public enum EItemType
    {
        NONE,
        QUEST,
        GROW,
        BATTLE,
    }

    public enum EBattleItemEffectType
    {
        NONE,
        BASIC_CHG,
        BUFF,
    }


}
