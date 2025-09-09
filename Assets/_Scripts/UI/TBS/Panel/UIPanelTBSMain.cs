using SCFrame.UI;
using SCFrame;
using UnityEngine.EventSystems;
using System;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSMain : _ASCUIPanelBase<UIMonoTBSMain>
    {
        public UIPanelTBSMain(UIMonoTBSMain _mono, SCUIShowType _showType) : base(_mono, _showType)
        {

        }
        public override void OnInitialize()
        {
            mono.btnNormalAttack.AddClickDown(_onBtnNormalAttackClickDown);
            mono.btnSkill.AddClickDown(_onBtnSkillClickDown);
            mono.btnItem.AddClickDown(_onBtnItemClickDown);
            mono.btnDefence.AddClickDown(_onBtnDefenceClickDown);

        }

        public override void OnDiscard()
        {
            mono.btnNormalAttack.RemoveClickDown(_onBtnNormalAttackClickDown);
            mono.btnSkill.RemoveClickDown(_onBtnSkillClickDown);
            mono.btnItem.RemoveClickDown(_onBtnItemClickDown);
            mono.btnDefence.RemoveClickDown(_onBtnDefenceClickDown);

        }

        public override void OnHidePanel()
        {
        }



        public override void OnShowPanel()
        {
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

        }


    }
}
