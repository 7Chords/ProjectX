using System;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    public class SCTaskHelper : SingletonMono<SCTaskHelper>
    {
        private Action _m_updateEvent;
        private Action _m_lateUpdateEvent;
        private Action _m_fixedUpdateEvent;

        // 用于存储需要在下一帧执行的方法
        private Queue<Action> _m_nextUpdateActionQueue;
        private Queue<Action> _m_nextLateUpdateActionQueue;
        private Queue<Action> _m_nextFixedUpdateActionQueue;

        protected override void Awake()
        {
            base.Awake();
            _m_nextUpdateActionQueue = new Queue<Action>();
            _m_nextLateUpdateActionQueue = new Queue<Action>();
            _m_nextFixedUpdateActionQueue = new Queue<Action>();

        }
        private void OnDestroy()
        {
            _m_updateEvent = null;
            _m_lateUpdateEvent = null;
            _m_fixedUpdateEvent = null;
            _m_nextUpdateActionQueue.Clear();
            _m_nextLateUpdateActionQueue.Clear();
            _m_nextFixedUpdateActionQueue.Clear();
            _m_nextUpdateActionQueue = null;
            _m_nextLateUpdateActionQueue = null;
            _m_nextFixedUpdateActionQueue = null;


        }

        /// <summary>
        /// 添加Update监听
        /// </summary>
        /// <param name="_action"></param>
        public void AddUpdateListener(Action _action)
        {
            _m_updateEvent += _action;
        }
        /// <summary>
        /// 移除Update监听
        /// </summary>
        /// <param name="_action"></param>
        public void RemoveUpdateListener(Action _action)
        {
            _m_updateEvent -= _action;
        }

        /// <summary>
        /// 添加LateUpdate监听
        /// </summary>
        /// <param name="_action"></param>
        public void AddLateUpdateListener(Action _action)
        {
            _m_lateUpdateEvent += _action;
        }
        /// <summary>
        /// 移除LateUpdate监听
        /// </summary>
        /// <param name="_action"></param>
        public void RemoveLateUpdateListener(Action _action)
        {
            _m_lateUpdateEvent -= _action;
        }

        /// <summary>
        /// 添加FixedUpdate监听
        /// </summary>
        /// <param name="_action"></param>
        public void AddFixedUpdateListener(Action _action)
        {
            _m_fixedUpdateEvent += _action;
        }
        /// <summary>
        /// 移除FixedUpdate监听
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
            ExecuteQueuedActions(_m_nextUpdateActionQueue);
        }
        private void LateUpdate()
        {
            _m_lateUpdateEvent?.Invoke();
            ExecuteQueuedActions(_m_nextLateUpdateActionQueue);

        }
        private void FixedUpdate()
        {
            _m_fixedUpdateEvent?.Invoke();
            ExecuteQueuedActions(_m_nextFixedUpdateActionQueue);
        }

        /// <summary>
        /// 在update下一帧执行
        /// </summary>
        /// <param name="_action"></param>
        public void DoInNextUpdate(Action _action)
        {
            if(_action != null)
            {
                _m_nextUpdateActionQueue.Enqueue(_action);
            }
        }

        /// <summary>
        /// 在fixedupdate下一帧执行
        /// </summary>
        /// <param name="_action"></param>
        public void DoInNextFixedUpdate(Action _action)
        {
            if (_action != null)
            {
                _m_nextFixedUpdateActionQueue.Enqueue(_action);
            }
        }

        /// <summary>
        /// 在lateupdate下一帧执行
        /// </summary>
        /// <param name="_action"></param>
        public void DoInNextLateUpdate(Action _action)
        {
            if (_action != null)
            {
                _m_nextLateUpdateActionQueue.Enqueue(_action);
            }
        }

        /// <summary>
        /// 执行队列中的所有方法并清空队列
        /// </summary>
        /// <param name="_actionsQueue">方法队列</param>
        private void ExecuteQueuedActions(Queue<Action> _actionsQueue)
        {
            if (_actionsQueue.Count > 0)
            {
                // 先复制队列内容，避免在执行过程中修改队列
                var actionsToExecute = new List<Action>(_actionsQueue);
                _actionsQueue.Clear();

                // 执行所有方法
                foreach (var action in actionsToExecute)
                {
                    try
                    {
                        action?.Invoke();
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError($"SCTaskHelper:Error executing queued action: {ex.Message}");
                    }
                }
            }
        }
    }
}
