using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelCharacter : _ASCUIPanelBase<UIMonoCharacter>
    {
        private UIPanelCharacterProps _m_panelDetailProps;
        private UIPanelCharacterHeaderContainer _m_detailHeaderContainer;


        private int _m_curSelectHeaderIdx;
        public List<TBSActorInfo> _m_actorInfoList;
        public UIPanelCharacter(UIMonoCharacter _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            _m_panelDetailProps = new UIPanelCharacterProps(mono.monoCharacterPorps, SCUIShowType.INTERNAL);
            _m_panelDetailProps.Initialize();
            _m_detailHeaderContainer = new UIPanelCharacterHeaderContainer(mono.monoHeaderContainer);
            _m_detailHeaderContainer.Initialize();
        }

        public override void BeforeDiscard()
        {
            _m_panelDetailProps?.Discard();
            _m_detailHeaderContainer?.Discard();
        }

        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_SWITCH_TO_DOWN_INPUT, onSwitchToDownInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_SWITCH_TO_UP_INPUT, onSwitchToUpInput);
            SCMsgCenter.UnregisterMsg(SCMsgConst.OW_CHARACTER_SELECT_CLICK, onCharacterSelectClick);

            _m_panelDetailProps?.HidePanel();
            _m_detailHeaderContainer?.HidePanel();
        }
        private void refreshShow()
        {
            if (_m_actorInfoList == null || _m_actorInfoList.Count == 0)
                return;
            if (_m_curSelectHeaderIdx >= 0 && _m_curSelectHeaderIdx < _m_actorInfoList.Count)
                _m_panelDetailProps.SetInfo(_m_actorInfoList[_m_curSelectHeaderIdx]);

            _m_detailHeaderContainer.RefreshContainerShow(_m_actorInfoList, _m_curSelectHeaderIdx);


            mono.imgCharacter.sprite = ResourcesHelper.LoadAsset<Sprite>(_m_actorInfoList[_m_curSelectHeaderIdx].characterRefObj.assetHeadIconObjName);
            mono.txtNameWithLv.text = GameCommon.GetCharacterNameWithLv(_m_actorInfoList[_m_curSelectHeaderIdx].characterLv,
                _m_actorInfoList[_m_curSelectHeaderIdx].characterRefObj.characterName);
            mono.txtHp.text = LanguageHelper.instance.GetTextTranslate("#2_hp_value",_m_actorInfoList[_m_curSelectHeaderIdx].maxHp);
            mono.txtMp.text = LanguageHelper.instance.GetTextTranslate("#2_mp_value",_m_actorInfoList[_m_curSelectHeaderIdx].maxMp);
            mono.txtCharacterDesc.text = LanguageHelper.instance.GetTextTranslate(_m_actorInfoList[_m_curSelectHeaderIdx].characterRefObj.characterDesc);
        }
        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_DOWN_INPUT, onSwitchToDownInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_UP_INPUT, onSwitchToUpInput);
            SCMsgCenter.RegisterMsg(SCMsgConst.OW_CHARACTER_SELECT_CLICK, onCharacterSelectClick);
            //todo
            _m_actorInfoList = SCDataMgr.instance.playerActorInfoList;

            _m_panelDetailProps?.ShowPanel();
            _m_detailHeaderContainer?.ShowPanel();

            _m_detailHeaderContainer.SetListInfo(_m_actorInfoList, _m_curSelectHeaderIdx);
            refreshShow();
        }

        private void onCharacterSelectClick(object[] _objs)
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

        private void onSwitchToUpInput()
        {
            _m_curSelectHeaderIdx--;
            if (_m_curSelectHeaderIdx < 0)
                _m_curSelectHeaderIdx = _m_actorInfoList.Count - 1;
            refreshShow();
        }

        private void onSwitchToDownInput()
        {
            _m_curSelectHeaderIdx++;
            if (_m_curSelectHeaderIdx > _m_actorInfoList.Count - 1)
                _m_curSelectHeaderIdx = 0;
            refreshShow();
        }
    }
}
