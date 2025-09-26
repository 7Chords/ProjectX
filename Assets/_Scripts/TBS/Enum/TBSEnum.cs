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
               //NAVIGATE,//导航（光标）
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
    //护甲等级
    public enum EArmorLevelType
    {
        NONE,
        LIGHT,//轻
        MEDIUM,//中
        HEAVY,//重
        HERO,//英雄
    }
    //法术抗性
    public enum EMagicResistanceLevelType
    {
        NONE,
        LIGHT,//轻
        MEDIUM,//中
        HEAVY,//重
        HERO,//英雄
    }
    //物理伤害等级
    public enum EPhysicalLevelType
    {
        NONE,
        LIGHT,//轻
        MEDIUM,//中
        HEAVY,//重
        HERO,//英雄
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

}
