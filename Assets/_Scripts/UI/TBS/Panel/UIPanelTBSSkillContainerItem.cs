using GameCore.RefData;
using GameCore.TBS;
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
        private bool _m_isSelect;
        public UIPanelTBSSkillContainerItem(UIMonoTBSSkillContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
            
        }

        public override void BeforeDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_CONFIRM, onTBSActorSkillConfirm);

            mono.btnSkillClick.RemoveClickDown(onBtnSkillClickDown);
            mono.btnSkillClick.RemoveMouseEnter(onBtnSkillMouseEnter);
        }

        public override void AfterInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_CONFIRM, onTBSActorSkillConfirm);

            mono.btnSkillClick.AddClickDown(onBtnSkillClickDown);
            mono.btnSkillClick.AddMouseEnter(onBtnSkillMouseEnter);
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
            skillNeedStr = skillNeedHp + " " + skillNeedMp;//todo：是否写死空格
            mono.txtSkillNeed.text = skillNeedStr;
        }

        public void SetSelect(bool _isSelect)
        {
            mono.imgSkill.color = _isSelect ? mono.colorSkillSelect : mono.colorSkillUnSelect;
            _m_isSelect = _isSelect;

            
        }


        private void onBtnSkillClickDown(PointerEventData _eventData, object[] _args)
        {

            if (!_m_isSelect)
                return;
            SCModel.instance.tbsModel.selectTargetType = _m_skillRefObj.damageTargetType;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSConfirm(SCUIShowType.FULL));
            //重新设置光标
            List<Vector3> worldPosList = new List<Vector3>();
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                worldPosList.Add(SCModel.instance.tbsModel.GetCurSingleSelectTargetActor().GetCursorPos());
            else if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
            {
                foreach (var module in SCModel.instance.tbsModel.enemyActorModuleList)
                {
                    worldPosList.Add(module.GetCursorPos());
                }
            }
            TBSCursorMgr.instance.SetSelectionCursor(worldPosList);

        }


        private void onBtnSkillMouseEnter(PointerEventData _eventData, object[] _args)
        {
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL_MOUSE_HIGHLIGHT, _m_skillRefObj.id);

        }

        private void onTBSActorSkillConfirm()
        {
            if (!_m_isSelect)
                return;

            SCModel.instance.tbsModel.selectTargetType = _m_skillRefObj.damageTargetType;



            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSConfirm(SCUIShowType.FULL));
            //重新设置光标
            //List<Vector3> worldPosList = new List<Vector3>();
            //if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
            //    worldPosList.Add(SCModel.instance.tbsModel.GetCurSingleSelectTargetActor().GetCursorPos());
            //else if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
            //{
            //    foreach (var module in SCModel.instance.tbsModel.enemyActorModuleList)
            //    {
            //        worldPosList.Add(module.GetCursorPos());
            //    }
            //}
            //TBSCursorMgr.instance.SetSelectionCursor(worldPosList);
        }
    }
}
