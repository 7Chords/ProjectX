using GameCore.RefData;
using SCFrame;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.OW
{
    public class CommonDialogueArea : _ASCLifeGameObjBase
    {
        [Header("对话组")]
        public long dialogueGroup;

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
                GameCommon.ShowInteractText("查看", transform);
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
            List<DialogueRefObj> dialogueRefList = SCRefDataMgr.instance.dialogueRefList.refDataList
                .FindAll(x => x.group == dialogueGroup);
            DialogueInfo dialogueInfo = new DialogueInfo(dialogueRefList);
            DialogueHandler.LoadDialogue(dialogueInfo);
            GameCommon.DiscardCurrentInteractText();
        }

        public override void OnInitialize()
        {
            this.AddTriggerEnter(onTriggerEnter);
            this.AddTriggerExit(onTriggerExit);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
            OWEntityMgr.instance.RegisterEntity(this);

        }

        public override void OnDiscard()
        {
            this.RemoveTriggerEnter(onTriggerEnter);
            this.RemoveTriggerExit(onTriggerExit);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
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
