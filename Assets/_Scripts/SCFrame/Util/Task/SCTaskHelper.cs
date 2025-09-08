using System;
using UnityEngine;

namespace SCFrame
{
    public class SCTaskHelper : SingletonMono<SCTaskHelper>
    {
        private Action _m_updateEvent;
        private Action _m_lateUpdateEvent;
        private Action _m_fixedUpdateEvent;

        /// <summary>
        /// Ìí¼ÓUpdate¼àÌý
        /// </summary>
        /// <param name="_action"></param>
        public void AddUpdateListener(Action _action)
        {
            _m_updateEvent += _action;
        }
        /// <summary>
        /// ÒÆ³ýUpdate¼àÌý
        /// </summary>
        /// <param name="_action"></param>
        public void RemoveUpdateListener(Action _action)
        {
            _m_updateEvent -= _action;
        }

        /// <summary>
        /// Ìí¼ÓLateUpdate¼àÌý
        /// </summary>
        /// <param name="_action"></param>
        public void AddLateUpdateListener(Action _action)
        {
            _m_lateUpdateEvent += _action;
        }
        /// <summary>
        /// ÒÆ³ýLateUpdate¼àÌý
        /// </summary>
        /// <param name="_action"></param>
        public void RemoveLateUpdateListener(Action _action)
        {
            _m_lateUpdateEvent -= _action;
        }

        /// <summary>
        /// Ìí¼ÓFixedUpdate¼àÌý
        /// </summary>
        /// <param name="_action"></param>
        public void AddFixedUpdateListener(Action _action)
        {
            _m_fixedUpdateEvent += _action;
        }
        /// <summary>
        /// ÒÆ³ýFixedUpdate¼àÌý
        /// </summary>
        /// <param name="_action"></param>
        public void RemoveFixedUpdateListener(Action _action)
        {
            _m_fixedUpdateEvent -= _action;
        }

        public void ClearAllUpdateListener()
        {
            _m_updateEvent = null;
        }
        public void ClearAllFixedUpdateListener()
        {
            _m_fixedUpdateEvent = null;
        }
        public void ClearAllLateUpdateListener()
        {
            _m_lateUpdateEvent = null;
        }

        private void Update()
        {
            _m_updateEvent?.Invoke();
        }
        private void LateUpdate()
        {
            _m_lateUpdateEvent?.Invoke();
        }
        private void FixedUpdate()
        {
            _m_fixedUpdateEvent?.Invoke();
        }

        public void DoInNextUpdate()
        {

        }

        public void DoInNextFixedUpdate()
        {

        }
        public void DoInNextLateUpdate()
        {

        }
    }
}
