using SCFrame;
using System.Collections.Generic;

namespace GameCore.TBS
{
    public class TBSBuffHandler: _ASCLifeObjBase
    {
        public List<TBSGameBuffInfo> buffList;

        public override void OnInitialize()
        {
            buffList = new List<TBSGameBuffInfo>();
            this.OnUpdate(buffTickAndRemove);
        }

        public override void OnDiscard()
        {
            this.RemoveUpdate(buffTickAndRemove);
        }

        public override void OnResume()
        {
        }

        public override void OnSuspend()
        {
        }




        /// <summary>
        /// buff的效果周期和生命周期计时(回合制）
        /// </summary>
        private void buffTickAndRemove()
        {
            List<TBSGameBuffInfo> deleteBuffList = new List<TBSGameBuffInfo>();
            foreach (var buffInfo in buffList)
            {
                buffInfo.onTurnTick?.Invoke();

                if (buffInfo.remainTurnCount == 0)
                {
                    deleteBuffList.Add(buffInfo);
                }
                else
                {
                    buffInfo.remainTurnCount--;
                }
            }

            foreach (var buffInfo in deleteBuffList)
            {
                RemoveBuff(buffInfo);
            }
        }

        /// <summary>
        /// 添加buff
        /// </summary>
        /// <param name="_buffInfo"></param>
        public void AddBuff(TBSGameBuffInfo _buffInfo)
        {
            if (_buffInfo == null) return;
            TBSGameBuffInfo findBuffInfo = FindBuff(_buffInfo.buffRefObj.id);

            if (findBuffInfo != null)
            {
                //重置buff的剩余回合数
                findBuffInfo.remainTurnCount = findBuffInfo.totalTurnCount;
            }
            else
            {
                buffList.Add(_buffInfo);
                //对buffList进行排序
                //按照优先级降序排序 优先级数字越大排得越前面
                //buffList.Sort((buff1, buff2) => buff2.buffData.priority.CompareTo(buff1.buffData.priority));

                //触发创建buff时的回调
                findBuffInfo.onBuffAdd();
            }
        }

        /// <summary>
        /// 移除buff
        /// </summary>
        /// <param name="_buffInfo"></param>
        public void RemoveBuff(TBSGameBuffInfo _buffInfo)
        {
            buffList.Remove(_buffInfo);

            _buffInfo.onBuffRemove?.Invoke();
        }

        /// <summary>
        /// 查找列表中的buff
        /// </summary>
        /// <param name="_buffDataID"></param>
        /// <returns></returns>
        private TBSGameBuffInfo FindBuff(long _buffDataID)
        {
            foreach (var buffinfo in buffList)
            {
                if (buffinfo.buffRefObj.id == _buffDataID)
                {
                    return buffinfo;
                }
            }

            return default;
        }
    }
}
