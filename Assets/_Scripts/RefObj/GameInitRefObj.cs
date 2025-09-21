using SCFrame;
using System.Collections.Generic;


namespace GameCore.RefData
{
    public class GameInitRefObj : SCRefDataCore
    {
        public GameInitRefObj()
        {

        }
        public GameInitRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {

        }
        public List<long> init_player_team_list;
        public string init_player_team_name;
        public List<long> init_enemy_team_list;
        public string init_enemy_team_name;
        protected override void _parseFromString()
        {
            init_player_team_list = getList<long>("init_player_team_list");
            init_player_team_name = getString("init_player_team_name");
            init_enemy_team_list = getList<long>("init_enemy_team_list");
            init_enemy_team_name = getString("init_enemy_team_name");
        }

        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "game_init";
    }
}

