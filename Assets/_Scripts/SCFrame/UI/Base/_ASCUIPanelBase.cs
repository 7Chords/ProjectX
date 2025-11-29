using GameCore.Util;
using System;
using UnityEngine;

namespace SCFrame.UI
{
    /// <summary>
    /// UI面板抽象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class _ASCUIPanelBase<T> : _ASCLifeObjBase,ISCUIPanelBase where T:_ASCUIMonoBase
    {
        private bool _m_hasShowed;
        private bool _m_hasHided;

        public bool hasShowed { get => _m_hasShowed; }
        public bool hasHided { get => _m_hasHided; }


        protected T mono;
        protected SCUIShowType showType;

        protected TweenContainer fadeCanvasContainer;
        private _ASCUIPanelBase() { }

        public _ASCUIPanelBase(T _mono, SCUIShowType _showType)
        {
            mono = _mono;
            showType = _showType;

            //面板内部的子面板 不通过node初始化 所以在构造函数里面初始化
            if (showType == SCUIShowType.INTERNAL)
                Initialize();
        }

        public virtual GameObject GetGameObject()
        {
            if (mono != null)
                return mono.gameObject;
            return null;
        }

        public override void OnInitialize()
        {
            fadeCanvasContainer = new TweenContainer();
        }
        public override void OnDiscard()
        {
            fadeCanvasContainer?.KillAllDoTween();
            fadeCanvasContainer = null;
            SCCommon.DestoryGameObject(GetGameObject());
        }



        public void ShowPanel()
        {
            _m_hasShowed = true;
            _m_hasHided = false;
            if (showType == SCUIShowType.INTERNAL)
                SCCommon.SetGameObjectEnable(GetGameObject(),true);
            ShowPanelAnim();
            OnShowPanel();
        }

        protected virtual void ShowPanelAnim()
        {
        }

        public abstract void OnShowPanel();


        public void HidePanel()
        {
            _m_hasShowed = false;
            _m_hasHided = true;
            if (showType == SCUIShowType.INTERNAL)
                SCCommon.SetGameObjectEnable(GetGameObject(), false);
            OnHidePanel();
            HidePanelAnim(OnHideOver);
        }

        protected virtual void HidePanelAnim(Action _onHideOver)
        {
            _onHideOver?.Invoke();
        }

        protected virtual void OnHideOver()
        {

        }
        public abstract void OnHidePanel();



        //对于ui面板 隐藏和显示替代了挂起恢复的功能
        public sealed override void OnResume() { }
        public sealed override void OnSuspend() { }

    }
}
