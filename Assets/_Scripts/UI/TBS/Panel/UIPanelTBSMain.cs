using SCFrame.UI;
using SCFrame;
using UnityEngine.EventSystems;
using System;
using UnityEngine;
using DG.Tweening;
using GameCore.Util;
using GameCore.TBS;

namespace GameCore.UI
{
    public class UIPanelTBSMain : _ASCUIPanelBase<UIMonoTBSMain>
    {
        private TweenContainer _m_tweenContainer;
        public UIPanelTBSMain(UIMonoTBSMain _mono, SCUIShowType _showType) : base(_mono, _showType)
        {

        }
        public override void OnInitialize()
        {

            _m_tweenContainer = new TweenContainer();
        }

        public override void OnDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
        }

        public override void OnHidePanel()
        {

            mono.btnNormalAttack.RemoveClickDown(onBtnNormalAttackClickDown);
            mono.btnSkill.RemoveClickDown(onBtnSkillClickDown);
            mono.btnItem.RemoveClickDown(onBtnItemClickDown);
            mono.btnDefence.RemoveClickDown(onBtnDefenceClickDown);



            Tween tween = mono.canvasGroup.DOFade(0, mono.fadeOutDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 1;
            });
            _m_tweenContainer?.RegDoTween(tween);
        }



        public override void OnShowPanel()
        {
            mono.btnNormalAttack.AddClickDown(onBtnNormalAttackClickDown);
            mono.btnSkill.AddClickDown(onBtnSkillClickDown);
            mono.btnItem.AddClickDown(onBtnItemClickDown);
            mono.btnDefence.AddClickDown(onBtnDefenceClickDown);

            Tween tween = mono.canvasGroup.DOFade(1, mono.fadeInDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 0;
            });
            _m_tweenContainer?.RegDoTween(tween);
            refreshPanelShow();
        }

        //普通攻击按下回调
        private void onBtnNormalAttackClickDown(PointerEventData _eventData, object[] _args)
        {
            Debug.Log("点击了普通攻击！");
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ATTACK_INPUT);

        }
        private void onBtnSkillClickDown(PointerEventData data, object[] arg2)
        {
            Debug.Log("点击了技能！");
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SKILL_INPUT);

        }
        private void onBtnItemClickDown(PointerEventData data, object[] arg2)
        {
            Debug.Log("点击了道具！");
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ITEM_INPUT);
        }
        private void onBtnDefenceClickDown(PointerEventData data, object[] arg2)
        {
            Debug.Log("点击了防御！");
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_DEFEND_INPUT);
        }

        private void refreshPanelShow()
        {
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.getCurActorInfo();
            if (actorInfo == null)
                return;
            mono.imgCharacterHead.sprite = ResourcesHelper.LoadAsset<Sprite>(actorInfo.assetHeadIconObjName);
        }

    }
}
