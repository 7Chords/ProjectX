using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCore.UI
{
    public class UIPanelCharacterHeaderContainerItem : _ASCUIPanelBase<UIMonoCharacterHeaderContainerItem>
    {
        private TBSActorInfo _m_actorInfo;

        private bool _m_hasSelected;
        public UIPanelCharacterHeaderContainerItem(UIMonoCharacterHeaderContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
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
            mono.btnSelect.RemoveClickDown(onBtnSelectClick);
        }

        public override void OnShowPanel()
        {
            mono.btnSelect.AddMouseLeftClickDown(onBtnSelectClick);
        }
        public void SetInfo(TBSActorInfo _info)
        {
            _m_actorInfo = _info;
            refreshShow();
        }

        private void refreshShow()
        {
            if (_m_actorInfo == null)
                return;
            refreshSelectShow();
            mono.imgHeadIcon.sprite = ResourcesHelper.LoadAsset<Sprite>(_m_actorInfo.characterRefObj.assetHeadIconObjName);

        }

        private void refreshSelectShow()
        {
            SCCommon.SetGameObjectEnable(mono.goSelectShowList, _m_hasSelected);
        }

        public void SetSelect(bool _isSelect)
        {
            _m_hasSelected = _isSelect;
            refreshSelectShow();
        }

        private void onBtnSelectClick(PointerEventData _data, object[] _objs)
        {
            if (_m_hasSelected)
                return;
            SCMsgCenter.SendMsg(SCMsgConst.TBS_DETAIL_SELECT_CLICK, _m_actorInfo);
        }
    }
}
