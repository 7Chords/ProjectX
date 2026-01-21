using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class GameConst
    {
        #region UIName
        public const string TBS_MAIN_PANEL = "tbs_main_panel";
        public const string SELECTION_CURSOR = "selection_cursor";
        public const string TBS_SKILL_PANEL = "tbs_skill_panel";
        public const string TBS_SKILL_ITEM_PREFAB = "tbs_skill_item_prefab";
        public const string TBS_INFO_ITEM_PREFAB = "tbs_info_item_prefab";
        public const string TBS_INFO_PANEL = "tbs_info_panel";
        public const string TBS_CONFIM_PANEL = "tbs_confim_panel";
        public const string TBS_ENEMY_HUD_PANEL = "tbs_enemy_hud_panel";
        public const string TBS_ENEMY_HUD_ITEM_PREFAB = "tbs_enemy_hud_item_prefab";
        public const string TBS_DAMAGE_NUM_PREFAB = "tbs_damage_num_prefab";
        public const string TBS_TURN_CHG_PANEL = "tbs_turn_chg_panel";
        public const string TBS_WIN_PANEL = "tbs_win_panel";
        public const string TBS_LOSE_PANEL = "tbs_lose_panel";
        public const string TBS_ATTACK_STATE_PREFAB = "tbs_attack_state_prefab";
        public const string TBS_ITEM_PANEL = "tbs_item_panel";
        public const string TBS_ITEM_ITEM_PREFAB = "tbs_item_item_prefab";
        public const string COMMON_TIP_PREFAB = "common_tip_prefab";
        public const string TBS_PLAYER_HUD_PANEL = "tbs_player_hud_panel";
        public const string TBS_PLAYER_HUD_ITEM_PREFAB = "tbs_player_hud_item_prefab";
        public const string TBS_SKILL_NAME_TIP = "tbs_skill_name_tip";
        public const string TBS_DETAIL_PANEL = "tbs_detail_panel";
        public const string DIALOGUE_PANEL = "dialogue_panel";
        public const string OPTION_PANEL = "option_panel";
        public const string MAIN_PANEL = "main_panel";
        #endregion


        #region Layer
        public const string LAYER_CHARACTER = "Character";

        #endregion

        #region Tag
        public const string TAG_ENEMY = "Enemy";
        public const string TAG_PLAYER = "Player";
        #endregion

        public const float MOUSE_RAY_MAX_DISTANCE = 99;

        public const float CAMERA_OFFSET_TRANSITION_DURATION = 0.75f;
        public const float CAMERA_FOLLOW_CHANGE_DURATION = 0.75f;

        public const long ENEMY_NORMAL_ATTACK_ID = 0;

        #region AnimEvent or SignalEvent

        public const string SPAWN_DAMAGE_AREA_EVENT = "SpawnDamageArea";
        public const string COMMON_DEAL_SKILL_EVENT = "CommonDealSkill";
        public const string SPAWN_PARTICLE_EFFECT_EVENT = "SpawnParticleEffect";
        public const string SPAWN_FLY_OBJ_EVENT = "SpawnFlyObj";

        public const string PLAYER_ATTACK_OVER_EVENT = "PlayerAttackOver";
        #endregion

        #region OW Anim
        public const string PLAYER_IDLE_ANIM_NAME = "idle";
        public const string PLAYER_WALK_ANIM_NAME = "walk";
        public const string PLAYER_RUN_ANIM_NAME = "run";
        public const string PLAYER_ATTACK_ANIM_NAME = "attack";
        #endregion


    }
}
