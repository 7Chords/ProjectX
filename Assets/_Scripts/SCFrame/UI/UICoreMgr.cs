using System.Collections.Generic;

namespace SCFrame.UI
{
    /// <summary>
    /// UI管理器 队列控制 开启隐藏关闭...
    /// </summary>
    public class UICoreMgr : ACoreMgrBase
    {
        public override ECoreMgrType coreMgrType => ECoreMgrType.UI;

        private List<_ASCUINodeBase> _m_nodeList;

        public override void OnInitialize()
        {
            _m_nodeList = new List<_ASCUINodeBase>();
        }
        public override void OnDiscard()
        {
            _m_nodeList.Clear();
            _m_nodeList = null;
        }

        public override void OnResume()
        {
        }

        public override void OnSuspend()
        {
        }

        #region 功能

        /// <summary>
        /// 添加一个节点
        /// </summary>
        /// <param name="_node"></param>
        /// <param name="_needShow"></param>
        public void AddNode(_ASCUINodeBase _node,bool _needShow = true)
        {
            if (_m_nodeList == null)
                return;

            if (_node.EnterNode())
                _m_nodeList.Add(_node);

            //上一个同类型的节点
            _ASCUINodeBase lastSameTypeNode = null;
            for (int i = _m_nodeList.Count - 2; i > -1; i--)
            {
                lastSameTypeNode = _m_nodeList[i];
                if (lastSameTypeNode == null)
                    continue;
                if (lastSameTypeNode.showType == _node.showType)
                {
                    if (lastSameTypeNode.needHideWhenEnterNewSameTypeNode)
                    {
                        lastSameTypeNode.HideNode();
                        break;
                    }
                }
            }


            if (_needShow)
                _node.ShowNode();
        }

        /// <summary>
        /// 关闭当前的节点
        /// </summary>
        public void CloseCurNode()
        {
            if (_m_nodeList == null || _m_nodeList.Count == 0)
                return;

            _ASCUINodeBase topNode = _m_nodeList[_m_nodeList.Count - 1];
            if(topNode != null)
            {
                topNode.HideNode();
                topNode.QuitNode();
            }
            _m_nodeList.RemoveAt(_m_nodeList.Count - 1 );

            //上一个同类型的节点
            _ASCUINodeBase lastSameTypeNode = null;

            for(int i = _m_nodeList.Count - 1;i>-1;i--)
            {
                lastSameTypeNode = _m_nodeList[i];
                if (lastSameTypeNode == null)
                    continue;
                if (lastSameTypeNode.showType == topNode.showType)
                {
                    if (lastSameTypeNode.needHideWhenEnterNewSameTypeNode)
                    {
                        lastSameTypeNode.ShowNode();
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// 通过ESC关闭当前的节点
        /// </summary>
        public void CloseNodeByEsc()
        {
            if (_m_nodeList == null || _m_nodeList.Count == 0)
                return;
            _ASCUINodeBase topNode = _m_nodeList[_m_nodeList.Count - 1];
            if (!topNode.canQuitByEsc)
                return;
            CloseCurNode();
        }

        /// <summary>
        /// 通过鼠标右键关闭当前的节点
        /// </summary>
        public void CloseNodeByMouseRight()
        {
            if (_m_nodeList == null || _m_nodeList.Count == 0)
                return;
            _ASCUINodeBase topNode = _m_nodeList[_m_nodeList.Count - 1];
            if (!topNode.canQuitByMouseRight)
                return;
            CloseCurNode();
        }


        #endregion
    }
}
