using DG.Tweening;
using GameCore.OW;
using SCFrame;
using SCFrame.UI;
using System;
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
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_CONFIRM_INPUT, onConfirmInput);
        }


        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_CONFIRM_INPUT, onConfirmInput);

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
            if(_m_dialogueIndex >= _m_dialogueInfo.dialogueList.Count)
            {
                DialogueStarter.UnloadDialogue();
                return;
            }
            refreshShow();
        }
    }
}
