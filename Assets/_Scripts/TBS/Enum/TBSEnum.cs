namespace GameCore.TBS
{
    public enum ETBSSubMgrType
    {
        NONE,
        ACTOR,
        TURN,
        COMP,
        EFFECT,
    }
    public enum ETBSTurnType
    {
        NONE,
        PLAYER,
        ENEMY,
    }
    public enum ETBSEffectType
    {
        NONE,
        TURN_EVENT,
        BUFF
    }
    public enum ETBSCompType
    {
        NONE,
        NORMAL_ATTACK,//普通攻击
        SKILL,//技能
        ITEM,//道具
        DEFEND,//防御
        NAVIGATE,//导航（光标）
        ANALYSE,//分析
        SWITCH_BOW,//切换弓的形态
    }

    public enum EDamageType
    {
        NONE,
        PHYSICAL,//物理
        MAGIC,//魔法
        REAL,//真实
    }

    public enum EDamageAmountType
    {
        NONE,
        LITTLE,
        MIDDLE,
        LARGE,
        SUPER,
    }

    public enum ETargetType
    {
        NONE,
        SINGLE,//单体
        ALL,//所有
    }

    //护甲等级
    public enum EArmorLevelType
    {
        NONE = 0,
        LIGHT = 1,//轻
        MEDIUM = 2,//中
        HEAVY = 3,//重
        HERO = 4,//英雄
    }
    //法术抗性
    public enum EMagicResistanceLevelType
    {
        NONE = 0,
        LIGHT = 1,//轻
        MEDIUM = 2,//中
        HEAVY = 3,//重
        HERO = 4,//英雄
    }
    //物理伤害等级
    public enum EPhysicalLevelType
    {
        NONE = 0,
        LIGHT = 1,//轻
        MEDIUM = 2,//中
        HEAVY = 3,//重
        HERO = 4,//英雄
    }
    //法术类型
    public enum EMagicAttributeType
    {
        NONE,
        FIRE,//火
        WATER,//水
        WOOD,//木
    }

    //产生伤害的类型
    public enum EDamageCauseType
    {
        NONE,
        ATTACK,
        SKILL,
        BOUNCE,
        BUFF,
    }


    public enum EBuffType
    {
        ATTACK_DOWN,
        ATTACK_UP,
        DEFEND_DOWN,
        DEFEND_UP,
        MISS_DOWN,
        MISS_UP,
        CRITICAL_DOWN,
        CRITICAL_UP,
        PHYSICAL_LEVLE_DOWN,
        PHYSICAL_LEVEL_UP,
        ARMOR_LEVEL_UP,
        ARMOR_LEVEL_DOWN,
        MAGIC_RESISTANCE_LEVEL_UP,
        MAGIC_RESISTANCE_LEVEL_DOWN,
    }

    public enum EBuffStackType
    {
        ONE,
        TWO,
        THREE,
    }
}
