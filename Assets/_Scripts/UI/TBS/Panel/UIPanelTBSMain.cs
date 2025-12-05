using SCFrame.UI;
using SCFrame;
using UnityEngine.EventSystems;
using System;
using UnityEngine;
using DG.Tweening;
using GameCore.Util;
using GameCore.TBS;
using System.Collections.Generic;

namespace GameCore.UI
{
    public class UIPanelTBSMain : _ASCUIAnimPanelBase<UIMonoTBSMain>
    {
        private TweenContainer _m_tweenContainer;
        public UIPanelTBSMain(UIMonoTBSMain _mono, SCUIShowType _showType) : base(_mono, _showType)
        {

        }

        public override void AfterInitialize()
        {
            _m_tweenContainer = new TweenContainer();
        }
        public override void BeforeDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }

        public override void OnHidePanel()
        {
            mono.btnNormalAttack.RemoveClickDown(onBtnNormalAttackClickDown);
            mono.btnSkill.RemoveClickDown(onBtnSkillClickDown);
            mono.btnItem.RemoveClickDown(onBtnItemClickDown);
            mono.btnDefence.RemoveClickDown(onBtnDefenceClickDown);

            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ATTACK_INPUT, onTBSAttackInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_DEFEND_INPUT, onTBSDefendInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SKILL_INPUT, onTBSSkillInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ITEM_INPUT, onTBSItemInput);
        }



        public override void OnShowPanel()
        {
            mono.btnNormalAttack.AddClickDown(onBtnNormalAttackClickDown);
            mono.btnSkill.AddClickDown(onBtnSkillClickDown);
            mono.btnItem.AddClickDown(onBtnItemClickDown);
            mono.btnDefence.AddClickDown(onBtnDefenceClickDown);

            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ATTACK_INPUT,onTBSAttackInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_DEFEND_INPUT, onTBSDefendInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SKILL_INPUT, onTBSSkillInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ITEM_INPUT, onTBSItemInput);

            mono.btnNormalAttack.transform.localScale = mono.scaleBtnDefault * Vector3.one;
            mono.btnDefence.transform.localScale = mono.scaleBtnDefault * Vector3.one;
            mono.btnSkill.transform.localScale = mono.scaleBtnDefault * Vector3.one;
            mono.btnItem.transform.localScale = mono.scaleBtnDefault * Vector3.one;

            //todo
            SCModel.instance.tbsModel.selectTargetType = SCModel.instance.tbsModel.GetCurActorInfo().attackTargetType;

            refreshPanelShow();
        }

        private void onBtnNormalAttackClickDown(PointerEventData _eventData, object[] _args)
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ATTACK_INPUT);
        }
        private void onBtnSkillClickDown(PointerEventData data, object[] arg2)
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SKILL_INPUT);
        }
        private void onBtnItemClickDown(PointerEventData data, object[] arg2)
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ITEM_INPUT);
        }
        private void onBtnDefenceClickDown(PointerEventData data, object[] arg2)
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_DEFEND_INPUT);
        }
        private void onTBSAttackInput()
        {
            _m_tweenContainer.RegDoTween(mono.btnNormalAttack.transform
                .DOScale(mono.scaleClickBtn, mono.durationBtnScaleChg));
        }

        private void onTBSDefendInput()
        {
            _m_tweenContainer.RegDoTween(mono.btnDefence.transform
                .DOScale(mono.scaleClickBtn, mono.durationBtnScaleChg));
        }

        private void onTBSSkillInput()
        {
            _m_tweenContainer.RegDoTween(mono.btnSkill.transform
                .DOScale(mono.scaleClickBtn, mono.durationBtnScaleChg));
        }

        private void onTBSItemInput()
        {
            _m_tweenContainer.RegDoTween(mono.btnItem.transform
                .DOScale(mono.scaleClickBtn, mono.durationBtnScaleChg));
        }

        private void refreshPanelShow()
        {
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
            if (actorInfo == null)
                return;
            mono.imgCharacterHead.sprite = ResourcesHelper.LoadAsset<Sprite>(actorInfo.characterRefObj.assetHeadIconObjName);
        }

    }
}
