using SCFrame;

namespace GameCore.RefData
{
    public class ProfessionRefObj : SCRefDataCore
    {
        public ProfessionRefObj()
        {

        }
        public ProfessionRefObj(string _assetPath, string _sheetName) : base(_assetPath, _sheetName)
        {
        }


        public long id;
        public EProfessionType professionType;
        public string professionName;
        public string professionDesc;


        protected override void _parseFromString()
        {
            id = getLong("id");
            professionType = (EProfessionType)getEnum("professionType",typeof(EProfessionType));
            professionName = getString("professionName");
            professionDesc = getString("professionDesc");
        }

        public static string assetPath => "RefData/ExportTxt";
        public static string sheetName => "profession";
    }
}
