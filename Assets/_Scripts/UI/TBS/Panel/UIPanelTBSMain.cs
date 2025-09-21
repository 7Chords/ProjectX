using SCFrame.UI;
using SCFrame;
using UnityEngine.EventSystems;
using System;
using UnityEngine;
using DG.Tweening;
using GameCore.Util;

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

            mono.btnNormalAttack.RemoveClickDown(_onBtnNormalAttackClickDown);
            mono.btnSkill.RemoveClickDown(_onBtnSkillClickDown);
            mono.btnItem.RemoveClickDown(_onBtnItemClickDown);
            mono.btnDefence.RemoveClickDown(_onBtnDefenceClickDown);



            Tween tween = mono.canvasGroup.DOFade(0, mono.fadeOutDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 1;
            });
            _m_tweenContainer?.RegDoTween(tween);
        }



        public override void OnShowPanel()
        {
            mono.btnNormalAttack.AddClickDown(_onBtnNormalAttackClickDown);
            mono.btnSkill.AddClickDown(_onBtnSkillClickDown);
            mono.btnItem.AddClickDown(_onBtnItemClickDown);
            mono.btnDefence.AddClickDown(_onBtnDefenceClickDown);

            Tween tween = mono.canvasGroup.DOFade(1, mono.fadeInDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 0;
            });
            _m_tweenContainer?.RegDoTween(tween);
        }

        //普通攻击按下回调
        private void _onBtnNormalAttackClickDown(PointerEventData _eventData, object[] _args)
        {
            Debug.Log("点击了普通攻击！");
        }
        private void _onBtnSkillClickDown(PointerEventData data, object[] arg2)
        {
            Debug.Log("点击了技能！");
        }
        private void _onBtnItemClickDown(PointerEventData data, object[] arg2)
        {
            Debug.Log("点击了道具！");
        }
        private void _onBtnDefenceClickDown(PointerEventData data, object[] arg2)
        {
            Debug.Log("点击了防御！");
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_CHG, 1);
        }


    }
}
