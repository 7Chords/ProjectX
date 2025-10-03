using GameCore.RefData;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameCore.UI
{
    public class UIPanelTBSSkillContainerItem : _ASCUIPanelBase<UIMonoTBSSkillContainerItem>
    {

        private TBSActorSkillRefObj _m_skillRefObj;
        public UIPanelTBSSkillContainerItem(UIMonoTBSSkillContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
            
        }

        public override void OnInitialize()
        {
            mono.btnSkillClick.AddClickDown(onBtnSkillClickDown);
        }

        public override void OnDiscard()
        {
            mono.btnSkillClick.RemoveClickDown(onBtnSkillClickDown);

        }

        public override void OnShowPanel()
        {
        }

        public override void OnHidePanel()
        {

        }

        public void SetInfo(long _skillId)
        {
            _m_skillRefObj = SCRefDataMgr.instance.tbsActorSkillRefList.refDataList.Find(x => x.id == _skillId);
            if (_m_skillRefObj == null)
                return;
            mono.imgSkillIcon.sprite = ResourcesHelper.LoadAsset<Sprite>(_m_skillRefObj.skillIconObjName);
            mono.txtSkillName.text = _m_skillRefObj.skillName;
            string skillNeedStr = "", skillNeedHp = "", skillNeedMp = "";
            if (_m_skillRefObj.skillNeedHp != 0)
                skillNeedHp = _m_skillRefObj.skillNeedHp+"HP";
            if (!string.IsNullOrEmpty(skillNeedHp))
                skillNeedHp = SCUICommon.AddColorForRichText(skillNeedHp, mono.colorSkillHp);
            if (_m_skillRefObj.skillNeedMp != 0)
                skillNeedMp = _m_skillRefObj.skillNeedMp+"MP";
            if (!string.IsNullOrEmpty(skillNeedMp))
                skillNeedMp = SCUICommon.AddColorForRichText(skillNeedMp, mono.colorSkillMp);
            skillNeedStr = skillNeedHp + " " + skillNeedMp;//todo£∫ «∑Ò–¥À¿ø’∏Ò
            mono.txtSkillNeed.text = skillNeedStr;
        }

        public void SetSelect(bool _isSelect)
        {
            mono.imgSkill.color = _isSelect ? mono.colorSkillSelect : mono.colorSkillUnSelect;
        }


        private void onBtnSkillClickDown(PointerEventData _eventData, object[] _args)
        {
            //SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL, _m_skillRefObj.id);
            GameCoreMgr.instance.uiCoreMgr.HideCurNode();
        }
    }
}
