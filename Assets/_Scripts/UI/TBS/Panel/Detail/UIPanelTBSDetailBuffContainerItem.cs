using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSDetailBuffContainerItem : _ASCUIPanelBase<UIMonoTBSDetailBuffContainerItem>
    {
        private TBSGameBuffInfo _m_buffInfo;

        public UIPanelTBSDetailBuffContainerItem(UIMonoTBSDetailBuffContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
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
        }

        public override void OnShowPanel()
        {
        }

        public void SetInfo(TBSGameBuffInfo _buffInfo)
        {
            _m_buffInfo = _buffInfo;
            refreshShow();
        }

        private void refreshShow()
        {
            if (_m_buffInfo == null)
                return;
            mono.imgBuffIcon.sprite = ResourcesHelper.LoadAsset<Sprite>(_m_buffInfo.buffRefObj.buffIconObjName);
            mono.txtRemainTurn.text = LanguageHelper.instance.GetTextTranslate("#2_remain_turn", _m_buffInfo.remainTurnCount.ToString());
        }
    }
}
