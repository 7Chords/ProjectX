using GameCore.Util;
using SCFrame.UI;

namespace GameCore.UI
{
    public class UIPanelSetting : _ASCUIPanelBase<UIMonoSetting>
    {
        public UIPanelSetting(UIMonoSetting _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
        }

        public override void BeforeDiscard()
        {
        }

        public override void OnHidePanel()
        {
            mono.sldBgm.onValueChanged.RemoveAllListeners();
            mono.sldSfx.onValueChanged.RemoveAllListeners();
        }

        public override void OnShowPanel()
        {
            refreshShow();

            mono.sldBgm.onValueChanged.AddListener(onSldBgmValueChg);
            mono.sldSfx.onValueChanged.AddListener(onSldSfxValueChg);
        }


        private void onSldSfxValueChg(float _arg)
        {
            AudioMgr.instance.ChangeSfxVolume(_arg);
        }

        private void onSldBgmValueChg(float _arg)
        {
            AudioMgr.instance.ChangeBgmVolume(_arg);
        }

        private void refreshShow()
        {
            mono.sldBgm.value = AudioMgr.instance.bgmVolumeFactor;
            mono.sldSfx.value = AudioMgr.instance.sfxVolumeFactor;
        }
    }
}
