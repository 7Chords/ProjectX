public class SCMsgConst
{



    #region 玩家输入相关 10
    public const int TBS_ATTACK_INPUT = 1000;
    public const int TBS_DEFEND_INPUT = 1001;
    public const int TBS_SKILL_INPUT = 1002;
    public const int TBS_ITEM_INPUT = 1003;
    public const int TBS_SWITCH_TO_UP_INPUT = 1004;
    public const int TBS_SWITCH_TO_DOWN_INPUT = 1005;
    public const int TBS_SWITCH_TO_LEFT_INPUT = 1006;
    public const int TBS_SWITCH_TO_RIGHT_INPUT = 1007;
    public const int TBS_CONFIRM_INPUT = 1008;
    #endregion


    #region 回合制战斗相关 20
    public const int TBS_GAME_START = 2000;
    public const int TBS_GAME_FINISH = 2001;

    public const int TBS_TURN_CHG = 2002;//回合制战斗 - 回合轮转
    public const int TBS_ACTOR_ACTION_END = 2003;//回合制战斗 - 角色行动结束

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
    public const int TBS_ACTOR_SKILL = 2015;

    public const int TBS_ACTOR_SKILL_HIGHTLIGHT_UP = 2016;//回合制战斗 - 选择技能高光上移
    public const int TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN = 2017;//回合制战斗 - 选择技能高光下移
    public const int TBS_ACTOR_SKILL_MOUSE_HIGHLIGHT = 2018;//回合制战斗 - 鼠标选择技能高光

    public const int TBS_ACTOR_CHG = 2019;//回合制战斗 - 角色轮转
    public const int TBS_ACTOR_TARGET_HIGHLIGHT_LEFT = 2020;//回合制战斗 - 选择目标高光左移
    public const int TBS_ACTOR_TARGET_HIGHLIGHT_RIGHT = 2021;//回合制战斗 - 选择目标高光右移
    public const int TBS_ACTOR_TARGET_MOUSE_HIGHLIGHT = 2022;//回合制战斗 - 鼠标选择高光
    public const int TBS_ACTOR_SKILL_CONFIRM = 2023;//回合制战斗 - 技能确认
    public const int TBS_SELECT_SINGLE_ENEMY_TARGET_CHG = 2024;//回合制战斗 - 选择单个敌方目标 改变


    #endregion

    #region 系统相关 99
    public const int GAME_START = 9901;
    public const int GAME_END = 9902;


    #endregion
}
