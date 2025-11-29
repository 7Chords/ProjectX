using DG.Tweening;
using GameCore.Util;
using System;
using UnityEngine;

namespace SCFrame.UI
{
    /// <summary>
    /// UI面板抽象基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class _ASCUIPanelBase<T> : _ASCUILifeObjBase, ISCUIPanelBase where T:_ASCUIMonoBase
    {
        private bool _m_hasShowed;
        private bool _m_hasHided;

        public bool hasShowed { get => _m_hasShowed; }
        public bool hasHided { get => _m_hasHided; }


        protected T mono;
        protected SCUIShowType showType;

        private TweenContainer _m_fadeCanvasContainer;
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


        public override void BeforeInitialize()
        {
            _m_fadeCanvasContainer = new TweenContainer();
            SCCommon.SetGameObjectEnable(GetGameObject(), false);
        }

        public override void AfterDiscard()
        {
            _m_fadeCanvasContainer?.KillAllDoTween();
            _m_fadeCanvasContainer = null;
            SCCommon.DestoryGameObject(GetGameObject());
        }



        public void ShowPanel()
        {
            _m_hasShowed = true;
            _m_hasHided = false;
            if (showType == SCUIShowType.INTERNAL)
                SCCommon.SetGameObjectEnable(GetGameObject(),true);
            else
                ShowPanelAnim(OnBeforeShow);
            OnShowPanel();
        }

        protected virtual void ShowPanelAnim(Action _onBeforeShow)
        {
            mono.canvasGroup.alpha = 0f;
            _m_fadeCanvasContainer.RegDoTween(mono.canvasGroup.DOFade(1, mono.fadeInDuration)
                .OnStart(() =>
                {
                    _onBeforeShow?.Invoke();
                }));
        }
        protected virtual void OnBeforeShow()
        {
            SCCommon.SetGameObjectEnable(GetGameObject(), true);
        }




        public abstract void OnShowPanel();


        public void HidePanel()
        {
            _m_hasShowed = false;
            _m_hasHided = true;
            OnHidePanel();
            if (showType == SCUIShowType.INTERNAL)
                SCCommon.SetGameObjectEnable(GetGameObject(), false);
            else
                HidePanelAnim(OnHideOver);
        }

        protected virtual void HidePanelAnim(Action _onHideOver)
        {
            mono.canvasGroup.alpha = 1f;
            _m_fadeCanvasContainer.RegDoTween(mono.canvasGroup.DOFade(0, mono.fadeOutDuration)
                .OnComplete(()=> 
                {
                    _onHideOver?.Invoke();
                }));
        }

        protected virtual void OnHideOver()
        {
            SCCommon.SetGameObjectEnable(GetGameObject(), false);
        }
        public abstract void OnHidePanel();

    }
}
