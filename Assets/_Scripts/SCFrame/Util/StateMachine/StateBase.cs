namespace SCFrame
{
    /// <summary>
    /// 状态机状态基类
    /// </summary>
    public class StateBase
    {
        protected StateMachine stateMachine;


        /// <summary>
        /// 初始化状态
        /// 只在状态第一次创建时执行
        /// </summary>
        /// <param name="_owner">宿主</param>
        /// <param name="stateType">状态类型枚举的值</param>
        /// <param name="_stateMachine">所属状态机</param>
        public virtual void Init(StateMachine _stateMachine)
        {
            stateMachine = _stateMachine;
        }

        /// <summary>
        /// 反初始化
        /// </summary>
        public virtual void Discard()
        {
            // 放回对象池
            this.ObjectPushPool();
        }

        /// <summary>
        /// 状态进入
        /// 每次进入都会执行
        /// </summary>
        public virtual void Enter() { }

        /// <summary>
        /// 状态退出
        /// </summary>
        public virtual void Exit() { }

        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
    }
}
