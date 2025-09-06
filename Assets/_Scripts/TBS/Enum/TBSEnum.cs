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
    //public enum ETBSTeamType
    //{
    //    PLAYER,
    //    ENEMY,
    //}
}
