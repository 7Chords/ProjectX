namespace GameCore.TBS
{
    public enum ETBSSubMgrType
    {
        ACTOR,
        TURN,
        COMP,
        EFFECT,
    }
    public enum ETBSTurnType
    {
        PLAYER,
        ENEMY,
    }
    public enum ETBSEffectType
    {
        TURN_EVENT,
        BUFF
    }
    public enum ETBSCompType
    {
        NORMAL_ATTACK,//普通攻击
        SKILL,//技能
        ITEM,//道具
        DEFEND,//防御
               //NAVIGATE,//导航（光标）
        ANALYSE,//分析
    }

    public enum EDamageType
    {
        PHYSICAL,//物理
        MAGIC,//魔法
        REAL,//真实
    }
    //护甲等级
    public enum EArmorLevelType
    {
        LIGHT,//轻
        MEDIUM,//中
        HEAVY,//重
        HERO,//英雄
    }
    //法术抗性
    public enum EMagicResistanceLevelType
    {
        LIGHT,//轻
        MEDIUM,//中
        HEAVY,//重
        HERO,//英雄
    }
    //物理伤害等级
    public enum EPhysicalLevelType
    {
        LIGHT,//轻
        MEDIUM,//中
        HEAVY,//重
        HERO,//英雄
    }
    //法术类型
    public enum EMagicAttributeType
    {
        FIRE,//火
        WATER,//水
        WOOD,//木头
    }

}
