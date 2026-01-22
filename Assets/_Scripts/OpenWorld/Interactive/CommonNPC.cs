using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCFrame;
using System;
using GameCore.RefData;

namespace GameCore.OW
{
    public class CommonNPC : MonoBehaviour
    {
        [Header("角色动画机")]
        public Animator animator;
        [Header("对话组")]
        public long dialogueGroup;
        [Header("空闲动画名")]
        public string idleAnimName;

        private SCAnimationCtl _m_animCtl;
        private void Start()
        {
            _m_animCtl = new SCAnimationCtl();
            _m_animCtl.SetAnimator(animator);
            _m_animCtl.Initialize();

            if(!string.IsNullOrEmpty(idleAnimName))
                _m_animCtl.PlaySingleAniamtion(ResourcesHelper.LoadAsset<AnimationClip>(idleAnimName));

            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
            this.AddTriggerEnter(onTriggerEnter);
            this.AddTriggerExit(onTriggerExit);

        }

        private void OnDisable()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
            this.RemoveTriggerEnter(onTriggerEnter);
            this.RemoveTriggerExit(onTriggerExit);

        }

        private void onTriggerEnter(Collider _coll, object[] _objs)
        {
            if(_coll.gameObject.tag == GameConst.TAG_PLAYER)
                GameCommon.ShowInteractText("对话", transform);
        }
        private void onTriggerExit(Collider _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER)
                GameCommon.DiscardCurrentInteractText();
        }
        private void onInteractInput()
        {
            List<DialogueRefObj> dialogueRefList = SCRefDataMgr.instance.dialogueRefList.refDataList
                .FindAll(x => x.group == dialogueGroup);
            DialogueInfo dialogueInfo = new DialogueInfo(dialogueRefList);
            DialogueStarter.LoadDialogue(dialogueInfo);
            GameCommon.DiscardCurrentInteractText();
        }
    }
}
