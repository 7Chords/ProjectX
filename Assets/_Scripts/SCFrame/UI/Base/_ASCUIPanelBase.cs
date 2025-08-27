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

        private _ASCUIPanelBase() { }

        public _ASCUIPanelBase(T _mono, SCUIShowType _showType)
        {
            mono = _mono;
            showType = _showType;
        }

        public virtual GameObject GetGameObject()
        {
            if (mono != null)
                return mono.gameObject;
            return null;
        }

        public void ShowPanel()
        {
            _m_hasShowed = true;
            _m_hasHided = false;
            OnShowPanel();
        }

        public abstract void OnShowPanel();
        public void ResetPanel()
        {
            OnResetPanel();
        }
        public abstract void OnResetPanel();
        public void HidePanel()
        {
            _m_hasShowed = false;
            _m_hasHided = true;
            OnHidePanel();
        }

        public abstract void OnHidePanel();


    }
}
