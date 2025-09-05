using System.Collections.Generic;

namespace SCFrame.UI
{
    /// <summary>
    /// UI管理器 队列控制 开启隐藏关闭...
    /// </summary>
    public class UICoreMgr : ACoreMgrBase
    {
        public override ECoreMgrType coreMgrType => ECoreMgrType.UI;

        private Stack<_ASCUINodeBase> _m_nodeStack;

        public override void OnInitialize()
        {
            _m_nodeStack = new Stack<_ASCUINodeBase>();
        }
        public override void OnDiscard()
        {
            _m_nodeStack.Clear();
            _m_nodeStack = null;
        }

        public override void OnResume()
        {
        }

        public override void OnSuspend()
        {
        }

        #region 功能
        public void AddNode(_ASCUINodeBase _node,bool _needShow = true)
        {
            if (_m_nodeStack == null)
                return;

            if (_node.EnterNode())
                _m_nodeStack.Push(_node);

            if (_needShow)
                _node.ShowNode();
        }

        public void CloseCurNode()
        {
            if (_m_nodeStack == null || _m_nodeStack.Count == 0)
                return;

            _ASCUINodeBase node = _m_nodeStack.Peek();
            if(node != null)
            {
                node.HideNode();
                node.QuitNode();
            }
            _m_nodeStack.Pop();
        }



        #endregion
    }
}
