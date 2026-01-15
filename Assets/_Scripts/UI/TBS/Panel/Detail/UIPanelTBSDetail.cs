using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCore.UI
{
    public class UIPanelTBSDetail : _ASCUIPanelBase<UIMonoTBSDetail>
    {
        private UIPanelTBSDetailProps _m_panelDetailProps;
        private UIPanelTBSDetailHeaderContainer _m_detailHeaderContainer;
        private UIPanelTBSDetailBuffContainer _m_detailBuffContainer;

        private int _m_curSelectHeaderIdx;
        public List<TBSActorInfo> _m_actorInfoList;
        public UIPanelTBSDetail(UIMonoTBSDetail _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            _m_panelDetailProps = new UIPanelTBSDetailProps(mono.monoDetailPorps,SCUIShowType.INTERNAL);
            _m_panelDetailProps.Initialize();
            _m_detailHeaderContainer = new UIPanelTBSDetailHeaderContainer(mono.monoHeaderContainer);
            _m_detailHeaderContainer.Initialize();
            _m_detailBuffContainer = new UIPanelTBSDetailBuffContainer(mono.monoBuffContainer);
            _m_detailBuffContainer.Initialize();
        }

        public override void BeforeDiscard()
        {
            _m_panelDetailProps?.Discard();
            _m_detailHeaderContainer?.Discard();
            _m_detailBuffContainer?.Discard();
        }

        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_DETAIL_SELECT_DOWN, onTBSDetailSelectDown);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_DETAIL_SELECT_UP, onTBSDetailSelectUp);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_DETAIL_SELECT_CLICK, onTBSDetailSelectClick);

            mono.btnClose.RemoveClickDown(onBtnCloseClickDown);

            _m_panelDetailProps?.HidePanel();
            _m_detailHeaderContainer?.HidePanel();
            _m_detailBuffContainer?.HidePanel();
        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_DETAIL_SELECT_DOWN, onTBSDetailSelectDown);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_DETAIL_SELECT_UP, onTBSDetailSelectUp);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_DETAIL_SELECT_CLICK, onTBSDetailSelectClick);

            mono.btnClose.AddMouseLeftClickDown(onBtnCloseClickDown);

            _m_actorInfoList = SCModel.instance.tbsModel.GetAllActorInfo();

            _m_panelDetailProps?.ShowPanel();
            _m_detailHeaderContainer?.ShowPanel();
            _m_detailBuffContainer?.ShowPanel();

            _m_detailHeaderContainer.SetListInfo(_m_actorInfoList, _m_curSelectHeaderIdx);
            refreshShow();
        }

        private void refreshShow()
        {
            if (_m_actorInfoList == null || _m_actorInfoList.Count == 0)
                return;
            if(_m_curSelectHeaderIdx >= 0 && _m_curSelectHeaderIdx< _m_actorInfoList.Count)
                _m_panelDetailProps.SetInfo(_m_actorInfoList[_m_curSelectHeaderIdx]);

            _m_detailHeaderContainer.RefreshContainerShow(_m_actorInfoList, _m_curSelectHeaderIdx);

            TBSActorBase actor = SCModel.instance.tbsModel.GetActorByRunningId(_m_actorInfoList[_m_curSelectHeaderIdx].runningId);
            if(actor != null)
                _m_detailBuffContainer.SetListInfo(actor.GetBuffInfoList());

            SCCommon.SetGameObjectEnable(mono.goIsPlayerActorShowList,!actor.actorInfo.isEnemy);

            mono.txtNameWithLv.text = GameCommon.GetCharacterNameWithLv(_m_actorInfoList[_m_curSelectHeaderIdx].characterLv,
                _m_actorInfoList[_m_curSelectHeaderIdx].characterRefObj.characterName);
            mono.imgHpBar.fillAmount = (float)_m_actorInfoList[_m_curSelectHeaderIdx].curHp / _m_actorInfoList[_m_curSelectHeaderIdx].maxHp;
            mono.txtHp.text = LanguageHelper.instance.GetTextTranslate("#2_{0}/{1}", _m_actorInfoList[_m_curSelectHeaderIdx].curHp, _m_actorInfoList[_m_curSelectHeaderIdx].maxHp);
            mono.imgMpBar.fillAmount = (float)_m_actorInfoList[_m_curSelectHeaderIdx].curMp / _m_actorInfoList[_m_curSelectHeaderIdx].maxMp;
            mono.txtMp.text = LanguageHelper.instance.GetTextTranslate("#2_{0}/{1}", _m_actorInfoList[_m_curSelectHeaderIdx].curMp, _m_actorInfoList[_m_curSelectHeaderIdx].maxMp);
            mono.txtCharacterDesc.text = LanguageHelper.instance.GetTextTranslate(_m_actorInfoList[_m_curSelectHeaderIdx].characterRefObj.characterDesc);

            GameCameraMgr.instance.SetDetailCamera(actor.GetDetailCameraPos(), !actor.actorInfo.isEnemy);
        }

        private void onTBSDetailSelectDown()
        {
            _m_curSelectHeaderIdx++;
            if (_m_curSelectHeaderIdx > _m_actorInfoList.Count - 1)
                _m_curSelectHeaderIdx = 0;
            refreshShow();
        }

        private void onTBSDetailSelectUp()
        {
            _m_curSelectHeaderIdx--;
            if (_m_curSelectHeaderIdx < 0)
                _m_curSelectHeaderIdx = _m_actorInfoList.Count - 1;
            refreshShow();
        }

        private void onTBSDetailSelectClick(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            TBSActorInfo info = _objs[0] as TBSActorInfo;
            if (info == null)
                return;
            int infoIdx = _m_actorInfoList.IndexOf(info);
            if (infoIdx < 0 || infoIdx >= _m_actorInfoList.Count)
                return;
            _m_curSelectHeaderIdx = infoIdx;
            refreshShow();
        }

        private void onBtnCloseClickDown(PointerEventData _data, object[] _objs)
        {
            GameCoreMgr.instance.uiCoreMgr.CloseTopNode();
        }
    }
}
