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
        }

        protected override void ShowPanelAnim(Action _onBeforeShow)
        {
            Cursor.visible = true;
            SCInputListener.instance.SetCanInput(false);
            SCGame.instance.owCamera.m_YAxis.m_MaxSpeed = 0;
            SCGame.instance.owCamera.m_XAxis.m_MaxSpeed = 0;
            PlayerController.instance.ChangeState(PlayerStateType.IDLE);
            mono.canvasGroup.alpha = 0f;
            fadeCanvasContainer.KillAllDoTween();
            fadeCanvasContainer.RegDoTween(mono.canvasGroup.DOFade(1, mono.fadeInDuration)
                .OnStart(() =>
                {
                    _onBeforeShow?.Invoke();
                }));
        }

        protected override void OnHideOver()
        {
            base.OnHideOver();
            SCInputListener.instance.SetCanInput(true);
            Cursor.visible = false;
            SCGame.instance.owCamera.m_YAxis.m_MaxSpeed = 3;
            SCGame.instance.owCamera.m_XAxis.m_MaxSpeed = 400;
        }
        public override void OnShowPanel()
        {
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
    }
}
