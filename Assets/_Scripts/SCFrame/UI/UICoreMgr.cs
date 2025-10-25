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
            _ASCUINodeBase node = _m_nodeList.Find(x => x.GetNodeName() == _node.GetNodeName());
            if (node != null)
            {
                if (node.hasHideNode)
                {
                    //队列中原本就存在该node，重新展示并移动到尾部
                    node.ShowNode();
                    _m_nodeList.Remove(node);
                    _m_nodeList.Add(node);
                }
                else
                    return;
            }
            else
            {
                if (_node.EnterNode())
                    _m_nodeList.Add(_node);
            }

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


            if (node == null && _needShow)
                _node.ShowNode();
        }

        /// <summary>
        /// 关闭当前的节点
        /// </summary>
        public void CloseTopNode()
        {
            if (_m_nodeList == null || _m_nodeList.Count == 0)
                return;

            _ASCUINodeBase topNode = _m_nodeList[_m_nodeList.Count - 1];
            if(topNode != null)
            {
                topNode.HideNode();
            }
            //把该节点移动到头部
            _m_nodeList.Remove(topNode);
            _m_nodeList.Insert(0, topNode);

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
            CloseTopNode();
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
            CloseTopNode();
        }


        public void HideTopNode()
        {
            _ASCUINodeBase topNode = GetTopNode();
            if (topNode == null)
                return;
            topNode.HideNode();
        }

        public void ShowTopNode()
        {
            _ASCUINodeBase topNode = GetTopNode();
            if (topNode == null)
                return;
            topNode.ShowNode();
        }

        public void HideNode(string _nodeName)
        {
            for(int i = _m_nodeList.Count - 1; i>-1; i--)
            {
                if(_m_nodeList[i].GetNodeName()==_nodeName)
                {
                    _m_nodeList[i].HideNode();
                    return;
                }
            }
        }

        public void ShowNode(string _nodeName)
        {
            for (int i = _m_nodeList.Count - 1; i > -1; i--)
            {
                if (_m_nodeList[i].GetNodeName() == _nodeName)
                {
                    _m_nodeList[i].ShowNode();
                    return;
                }
            }
        }

        public void RemoveNode(string _nodeName)
        {
            _ASCUINodeBase node = GetNodeByName(_nodeName);
            if (node == null)
                return;
            node.HideNode();
            node.QuitNode();
            _m_nodeList.Remove(node);
        }

        public _ASCUINodeBase GetNodeByName(string _nodeName)
        {
            foreach(var node in _m_nodeList)
            {
                if (node.GetNodeName() == _nodeName)
                    return node;
            }
            return null;
        }

        public _ASCUINodeBase GetTopNode()
        {
            if (_m_nodeList == null)
                return null;
            _ASCUINodeBase topNode = _m_nodeList[_m_nodeList.Count - 1];
            return topNode;
        }
        #endregion
    }
}
