using GameCore.TBS;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSInfoBuffContainerItem : _ASCUIPanelBase<UIMonoTBSInfoBuffContainerItem>
    {
        private TBSGameBuffInfo _m_buffInfo;

        public UIPanelTBSInfoBuffContainerItem(UIMonoTBSInfoBuffContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
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
            mono.imgIcon.sprite = Resources.Load<Sprite>(_m_buffInfo.buffRefObj.buffIconObjName);
        }
    }



}
