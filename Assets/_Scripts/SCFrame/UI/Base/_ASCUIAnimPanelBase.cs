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

        }

        protected override void HidePanelAnim(Action _onHideOver)
        {
            
        }


    }

}