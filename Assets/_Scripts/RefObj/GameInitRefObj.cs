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
        public List<long> init_team_character_list;
        public string init_player_team_name;

        protected override void _parseFromString()
        {
            init_team_character_list = getList<long>("init_team_character_list");
            init_player_team_name = getString("init_player_team_name");
        }

        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "game_init";
    }
}

