using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.UI
{
    /// <summary>
    /// 带Animator实现的可配置开启和关闭动画的UI面板抽象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class _ASCUIAnimPanelBase<T> : _ASCUIPanelBase<T> where T : _ASCUIMonoBase
    {
        protected _ASCUIAnimPanelBase(T _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        protected override void ShowPanelAnim(Action _onBeforeShow)
        {
            void fadeCanvas(Action _onComplete)
            {
                fadeCanvasContainer.RegDoTween(mono.canvasGroup.DOFade(1, mono.fadeInDuration)
                    .OnStart(() =>
                    {
                        _onBeforeShow?.Invoke();
                    })
                    .OnComplete(() =>
                    {
                        _onComplete?.Invoke();
                    }));
            }
            mono.canvasGroup.alpha = 0f;
            if (mono.uiAnimator != null && !string.IsNullOrEmpty(mono.showUIName))
            {

                mono.uiAnimator.Play(mono.showUIName, 0, 0f); //在0层播放动画并从0秒处开始（即第一帧）
                mono.uiAnimator.Update(0f); //立即强制更新一帧，确保状态应用

                fadeCanvas(() =>
                {
                    mono.uiAnimator.Play(mono.showUIName);
                });
            }
            else
            {
                fadeCanvas(null);
            }
        }

        protected override void HidePanelAnim(Action _onHideOver)
        {

            void fadeCanvas()
            {
                fadeCanvasContainer.RegDoTween(mono.canvasGroup.DOFade(0, mono.fadeOutDuration)
                    .OnComplete(() =>
                    {
                        _onHideOver?.Invoke();
                    }));
            }

            mono.canvasGroup.alpha = 1f;

            if (mono.uiAnimator != null && !string.IsNullOrEmpty(mono.hideUIName))
            {
                mono.animEventTrigger?.RemoveAnimationEvent("HideAnimOver");
                mono.animEventTrigger?.AddAnimationEvent("HideAnimOver", fadeCanvas);
                mono.uiAnimator.Play(mono.hideUIName);
            }
            else
            {
                fadeCanvas();
            }
        }


    }

}