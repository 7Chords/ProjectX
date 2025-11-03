using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Util
{
    public class SignalEventTrigger : MonoBehaviour
    {

        private Dictionary<string, Action> _m_eventDic = new Dictionary<string, Action>();

        /// <summary>
        /// 动画事件触发
        /// </summary>
        /// <param name="_eventName">事件名</param>
        public void SignalEvent(string _eventName)
        {
            if (_m_eventDic.TryGetValue(_eventName, out Action action))
                action?.Invoke();
        }
        public void AddSignalEvent(string _eventName, Action _action)
        {
            if (_m_eventDic.TryGetValue(_eventName, out Action action))
                action += _action;
            else
                _m_eventDic.Add(_eventName, _action);
        }

        public void RemoveSignalEvent(string _eventName)
        {
            _m_eventDic.Remove(_eventName);
        }

        public void RemoveSignalEvent(string _eventName, Action _action)
        {
            if (_m_eventDic.TryGetValue(_eventName, out Action action))
                action -= _action;
        }

        public void CleanAllSignalEvent()
        {
            _m_eventDic.Clear();
        }


    }
}
