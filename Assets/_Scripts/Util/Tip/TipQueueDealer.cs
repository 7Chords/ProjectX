using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameCore;
using SCFrame;

namespace GameCore.Util
{
    /// <summary>
    /// Tip队列管理器 - 用于按间隔依次显示多个Tip，避免重叠
    /// </summary>
    public class TipQueueDealer : Singleton<TipQueueDealer>
    {

        public const float TIP_SHOW_INTERNAL = 0.5f;

        // 存储Tip信息的队列
        private Queue<TipData> _m_tipQueue;

        // 是否正在处理队列
        private bool _m_isProcessingQueue = false;

        /// <summary>
        /// Tip数据结构体 - 存储显示Tip所需的所有信息
        /// </summary>
        private struct TipData
        {
            // Tip内容
            public string content;
            // Tip类型（0：通用顶部Tip  1：世界坐标Tip）
            public TipType tipType;
            // 世界坐标（仅TipType=1时有效）
            public Vector3 worldPosition;

            public TipData(string content, TipType tipType, Vector3 worldPos = default)
            {
                this.content = content;
                this.tipType = tipType;
                this.worldPosition = worldPos;
            }
        }

        /// <summary>
        /// Tip类型枚举
        /// </summary>
        private enum TipType
        {
            CommonTopTip,    // 通用顶部Tip
            WorldPositionTip // 世界坐标Tip
        }


        public override void OnInitialize()
        {
            _m_tipQueue = new Queue<TipData>();
        }

        public override void OnDiscard()
        {
            ClearTipQueue();
        }

        #region 对外提供的调用方法（替代原有直接调用ShowTip的方式）
        /// <summary>
        /// 加入通用顶部Tip到队列
        /// </summary>
        /// <param name="_content">Tip内容</param>
        public void EnqueueCommonTopTip(string _content)
        {
            if (string.IsNullOrEmpty(_content)) return;

            _m_tipQueue.Enqueue(new TipData(_content, TipType.CommonTopTip));
            // 开始处理队列（如果未在处理）
            if (!_m_isProcessingQueue)
            {
                SCTaskHelper.instance.CreateCoroutine(this,ProcessTipQueue());
            }
        }

        /// <summary>
        /// 加入世界坐标Tip到队列
        /// </summary>
        /// <param name="_content">Tip内容</param>
        /// <param name="_worldPos">世界坐标</param>
        public void EnqueueWorldPositionTip(string _content, Vector3 _worldPos)
        {
            if (string.IsNullOrEmpty(_content)) return;

            _m_tipQueue.Enqueue(new TipData(_content, TipType.WorldPositionTip, _worldPos));
            // 开始处理队列（如果未在处理）
            if (!_m_isProcessingQueue)
            {
                SCTaskHelper.instance.CreateCoroutine(this,ProcessTipQueue());
            }
        }
        #endregion

        #region 内部队列处理逻辑
        /// <summary>
        /// 处理Tip队列的协程
        /// </summary>
        private IEnumerator ProcessTipQueue()
        {
            _m_isProcessingQueue = true;

            // 循环处理队列中的所有Tip
            while (_m_tipQueue.Count > 0)
            {
                // 取出队列第一个Tip
                TipData currentTip = _m_tipQueue.Dequeue();

                // 根据Tip类型显示对应的Tip
                switch (currentTip.tipType)
                {
                    case TipType.CommonTopTip:
                        GameCommon.ShowCommonTopTip(currentTip.content);
                        break;
                    case TipType.WorldPositionTip:
                        GameCommon.ShowTip(currentTip.content, currentTip.worldPosition);
                        break;
                }

                // 等待Tip显示间隔
                yield return new WaitForSeconds(TIP_SHOW_INTERNAL );
            }

            // 队列为空，标记为未处理状态
            _m_isProcessingQueue = false;
        }
        #endregion

        /// <summary>
        /// 清空Tip队列（可选方法，比如场景切换时调用）
        /// </summary>
        public void ClearTipQueue()
        {
            _m_tipQueue.Clear();
            _m_isProcessingQueue = false;
            SCTaskHelper.instance.KillAllCoroutines(this);
        }
    }
}
