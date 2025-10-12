using GameCore.RefData;

namespace SCFrame
{
    /// <summary>
    /// 配表数据管理器
    /// </summary>
    public class SCRefDataMgr : Singleton<SCRefDataMgr>
    {
        public GameGeneralRefObj gameGeneralRefObj = new GameGeneralRefObj(GameGeneralRefObj.assetPath, GameGeneralRefObj.sheetName);

        public GameInitRefObj gameInitRefObj = new GameInitRefObj(GameInitRefObj.assetPath, GameInitRefObj.sheetName);

        public SCRefDataList<CharacterRefObj> characterRefList = new SCRefDataList<CharacterRefObj>(CharacterRefObj.assetPath,CharacterRefObj.sheetName);

        public SCRefDataList<ProfessionRefObj> professionRefList = new SCRefDataList<ProfessionRefObj>(ProfessionRefObj.assetPath, ProfessionRefObj.sheetName);

        public SCRefDataList<TBSActorSkillRefObj> tbsActorSkillRefList = new SCRefDataList<TBSActorSkillRefObj>(TBSActorSkillRefObj.assetPath, TBSActorSkillRefObj.sheetName);

        public SCRefDataList<TextLanguageRefObj> textLanguageRefList = new SCRefDataList<TextLanguageRefObj>(TextLanguageRefObj.assetPath, TextLanguageRefObj.sheetName);

        public TBSConfigRefObj tbsConfigRefObj = new TBSConfigRefObj(TBSConfigRefObj.assetPath, TBSConfigRefObj.sheetName);

        public SCRefDataList<UIResPathRefObj> uiResPathRefList = new SCRefDataList<UIResPathRefObj>(UIResPathRefObj.assetPath, UIResPathRefObj.sheetName);
        public override void OnInitialize()
        {
            gameGeneralRefObj.readFromTxt();
            characterRefList.readFromTxt();
            professionRefList.readFromTxt();
            gameInitRefObj.readFromTxt();
            tbsActorSkillRefList.readFromTxt();
            textLanguageRefList.readFromTxt();
            tbsConfigRefObj.readFromTxt();
            uiResPathRefList.readFromTxt();
        }
    }
}
