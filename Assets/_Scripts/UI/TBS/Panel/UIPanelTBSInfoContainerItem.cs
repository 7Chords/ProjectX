using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSInfoContainerItem : _ASCUIPanelBase<UIMonoTBSInfoContainerItem>
    {

        private TBSActorInfo _m_actorInfo;
        public UIPanelTBSInfoContainerItem(UIMonoTBSInfoContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void OnDiscard()
        {
        }

        public override void OnHidePanel()
        {
        }

        public override void OnInitialize()
        {
        }

        public override void OnShowPanel()
        {
        }

        public void SetInfo(TBSActorInfo _info)
        {
            _m_actorInfo = _info;
            refreshPanelShow();
        }

        private void refreshPanelShow()
        {
            if (_m_actorInfo == null)
                return;
            mono.imgCharacterHead.sprite = ResourcesHelper.LoadAsset<Sprite>(_m_actorInfo.assetHeadIconObjName);
            mono.txtHp.text = LanguageHelper.instance.GetTextTranslate("#2_{0}/{1}", _m_actorInfo.curHp, _m_actorInfo.maxHp);
            mono.txtMp.text = LanguageHelper.instance.GetTextTranslate("#2_{0}/{1}", _m_actorInfo.curMp, _m_actorInfo.maxMp);
            mono.imgHpBar.fillAmount = (float)_m_actorInfo.curHp / _m_actorInfo.maxHp;
            mono.imgMpBar.fillAmount = (float)_m_actorInfo.curMp / _m_actorInfo.maxMp;

        }
    }
}
