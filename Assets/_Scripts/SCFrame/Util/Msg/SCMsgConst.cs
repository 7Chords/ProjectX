public class SCMsgConst
{



    #region 玩家输入相关 10
    public const int TBS_ATTACK_INPUT = 1000;
    public const int TBS_DEFEND_INPUT = 1001;
    public const int TBS_SKILL_INPUT = 1002;
    public const int TBS_ITEM_INPUT = 1002;

    #endregion


    #region 回合制战斗相关 20
    public const int TBS_GAME_START = 2000;
    public const int TBS_GAME_FINISH = 2001;

    public const int TBS_TURN_CHG = 2002;//回合制战斗 - 回合轮转
    public const int TBS_ACTOR_CHG = 2003;//回合制战斗 - 角色轮转

    public const int TBS_TURN_MGR_WORK = 2004;
    public const int TBS_ACTOR_MGR_WORK = 2005;
    public const int TBS_EFFECT_MGR_WORK = 2006;
    public const int TBS_COMP_MGR_WORK = 2007;

    public const int TBS_TURN_MGR_REST = 2008;
    public const int TBS_ACTOR_MGR_REST = 2009;
    public const int TBS_EFFECT_MGR_REST = 2010;
    public const int TBS_COMP_MGR_REST = 2011;

    public const int TBS_GAME_RESULT = 2012;

    public const int TBS_ACTOR_ATTACK = 2013;
    public const int TBS_ACTOR_DEFENCE = 2014;


    #endregion

    #region 系统相关 99
    public const int GAME_START = 9901;
    public const int GAME_END = 9902;


    #endregion
}
