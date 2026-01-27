using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCFrame;
using System;
using GameCore.RefData;

namespace GameCore.OW
{
    public class CommonNPC : _ASCLifeGameObjBase
    {
        [Header("角色动画机")]
        public Animator animator;
        [Header("对话组")]
        public long dialogueGroup;
        [Header("空闲动画名")]
        public string idleAnimName;

        private SCAnimationCtl _m_animCtl;

        private bool _m_hasEnterTalkArea;
        private void Start()
        {
            Initialize();
        }


        private void onTriggerEnter(Collider _coll, object[] _objs)
        {
            if(_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {
                _m_hasEnterTalkArea = true;
                GameCommon.ShowInteractText("对话", transform);
            }
        }
        private void onTriggerExit(Collider _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {
                _m_hasEnterTalkArea = false;
                GameCommon.DiscardCurrentInteractText();
            }
        }
        private void onInteractInput()
        {
            if (!_m_hasEnterTalkArea)
                return;
            _m_hasEnterTalkArea = false;
            List<DialogueRefObj> dialogueRefList = SCRefDataMgr.instance.dialogueRefList.refDataList
                .FindAll(x => x.group == dialogueGroup);
            DialogueInfo dialogueInfo = new DialogueInfo(dialogueRefList);
            DialogueHandler.LoadDialogue(dialogueInfo);
            GameCommon.DiscardCurrentInteractText();
        }
        public override void OnInitialize()
        {
            _m_animCtl = new SCAnimationCtl();
            _m_animCtl.SetAnimator(animator);
            _m_animCtl.Initialize();

            if (!string.IsNullOrEmpty(idleAnimName))
                _m_animCtl.PlaySingleAniamtion(ResourcesHelper.LoadAsset<AnimationClip>(idleAnimName));

            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
            this.AddTriggerEnter(onTriggerEnter);
            this.AddTriggerExit(onTriggerExit);
            OWEntityMgr.instance.RegisterEntity(this);

        }

        public override void OnDiscard()
        {
            _m_animCtl?.Discard();
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
            this.RemoveTriggerEnter(onTriggerEnter);
            this.RemoveTriggerExit(onTriggerExit);
            OWEntityMgr.instance.UnRegisterEntity(this);

        }

        public override void OnResume()
        {
        }

        public override void OnSuspend()
        {
        }
    }
}
