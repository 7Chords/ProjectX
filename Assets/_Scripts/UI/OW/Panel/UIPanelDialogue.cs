using DG.Tweening;
using GameCore.OW;
using GameCore.RefData;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelDialogue : _ASCUIPanelBase<UIMonoDialogue>
    {
        private DialogueInfo _m_dialogueInfo;
        private int _m_dialogueIndex;
        public UIPanelDialogue(UIMonoDialogue _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
        }

        public override void BeforeDiscard()
        {
        }

        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_DIALOG_CONFIRM, onConfirmInput);
        }


        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_DIALOG_CONFIRM, onConfirmInput);

            _m_dialogueInfo = SCModel.instance.owModel.dialogueInfo;
            _m_dialogueIndex = 0;
            refreshShow();
        }


        private void refreshShow()
        {
            if (_m_dialogueInfo == null)
                return;
            if (_m_dialogueIndex < 0 || _m_dialogueIndex >= _m_dialogueInfo.dialogueList.Count)
                return;

            mono.txtName.text = _m_dialogueInfo.dialogueList[_m_dialogueIndex].characterName;
            mono.txtContent.text = _m_dialogueInfo.dialogueList[_m_dialogueIndex].content;
        }

        private void onConfirmInput()
        {

            _m_dialogueIndex++;
            if (_m_dialogueIndex >= _m_dialogueInfo.dialogueList.Count)
            {
                DialogueHandler.UnloadDialogue();
            }
            else
            {
                refreshShow();
            }
            //处理效果
            List<DialogueEffectObj> effectList = _m_dialogueInfo.dialogueList[_m_dialogueIndex - 1].dialogueEffectRefList;
            for (int i = 0; i < effectList.Count; i++)
            {
                DialogueHandler.DealDialogueEffect(effectList[i]);
            }
        }
    }
}
