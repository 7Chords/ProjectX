using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame.UI
{
    /// <summary>
    /// UI节点抽象基类
    /// </summary>
    public abstract class _ASCUINodeBase
    {
        private bool _m_hasEnterNode;
        private bool _m_hasHideNode;
        private bool _m_hasExitNode;

        protected SCUIShowType _m_showType;
        public bool hasEnterNode { get => _m_hasEnterNode; }
        public bool hasHideNode { get => _m_hasHideNode; }
        public bool hasExitNode { get => _m_hasExitNode; }
        public bool showType { get => _m_hasExitNode; }

        //进入同类型（normal/addition）节点时是否隐藏当前节点
        public abstract bool needHideWhenEnterNewSameTypeNode { get; }

        //该节点是否可以被esc关闭
        public abstract bool canQuitByEsc { get; }

        //该节点是否可以被鼠标右键关闭
        public abstract bool canQuitByMouseRight { get; }

        public _ASCUINodeBase(SCUIShowType _showType)
        {
            _m_showType = _showType;
        }

        public bool EnterNode()
        {
            if (_m_hasEnterNode)
            {
                Debug.Log(GetNodeName() + "已经Enter,无法再次Enter!!!");
                return false;
            }
            _m_hasEnterNode = true;
            _m_hasHideNode = true;
            OnEnterNode();
            return true;
        }
        public abstract void OnEnterNode();


        public void HideNode()
        {
            if (!_m_hasEnterNode)
            {
                Debug.Log(GetNodeName() + "没有Enter,无法Hide!!!");
                return;
            }
            _m_hasHideNode = true;
            OnHideNode();
        }
        public abstract void OnHideNode();


        public void ShowNode()
        {
            if (!_m_hasEnterNode)
            {
                Debug.Log(GetNodeName() + "没有Enter,无法Show!!!");
                return;
            }
            if(!_m_hasHideNode)
            {
                Debug.Log(GetNodeName() + "当前正在show,无法再次Show!!!");
                return;
            }

            _m_hasHideNode = false;
            OnShowNode();
        }

        public abstract void OnShowNode();


        public void QuitNode()
        {
            if (!_m_hasEnterNode)
            {
                Debug.Log(GetNodeName() + "没有Enter,无法Quit!!!");
                return;
            }
            _m_hasExitNode = true;
            OnQuitNode();
        }
        public abstract void OnQuitNode();


        //获取节点的名字 全局唯一
        public abstract string GetNodeName();

        public abstract string GetResName();
    }
}
