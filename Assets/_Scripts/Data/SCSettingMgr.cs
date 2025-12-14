using SCFrame;

namespace GameCore
{
    public class SCSettingMgr : Singleton<SCSettingMgr>
    {
        public SCSaveKeyInfo saveKeyInfo;
        public ELanguageType languageType;

        public override void OnInitialize()
        {
            saveKeyInfo = SCSaveSys.instance.settingData.saveKeyInfo;
            languageType = SCSaveSys.instance.settingData.languageType;
        }

        public override void OnDiscard()
        {

        }
    }
}
