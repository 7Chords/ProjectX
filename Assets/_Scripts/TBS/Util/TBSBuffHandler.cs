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
        }

        public override void OnDiscard()
        {
            buffList?.Clear();
            buffList = null;
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
        public void BuffTickAndRemove()
        {
            List<TBSGameBuffInfo> deleteBuffList = new List<TBSGameBuffInfo>();
            foreach (var buffInfo in buffList)
            {
                buffInfo.onTurnTick?.Invoke();

                buffInfo.remainTurnCount--;

                if (buffInfo.remainTurnCount == 0)
                {
                    deleteBuffList.Add(buffInfo);
                }
                else
                {
                    SCDebugHelper.LogWarning(buffInfo.buffRefObj.buffName + ":" + buffInfo.remainTurnCount);
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
            TBSGameBuffInfo findBuffInfo = findBuff(_buffInfo.buffRefObj.id);

            if (findBuffInfo != null)
            {
                //重置buff的剩余回合数
                findBuffInfo.remainTurnCount = findBuffInfo.totalTurnCount;
            }
            else
            {
                buffList.Add(_buffInfo);
                //触发创建buff时的回调
                _buffInfo.onBuffAdd?.Invoke();
            }
        }

        /// <summary>
        /// 移除buff
        /// </summary>
        /// <param name="_buffInfo"></param>
        public void RemoveBuff(TBSGameBuffInfo _buffInfo)
        {
            if (!buffList.Contains(_buffInfo))
                return;

            buffList.Remove(_buffInfo);

            _buffInfo.onBuffRemove?.Invoke();
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_REMOVE_BUFF, _buffInfo);

        }

        public void ClearAllBuffs()
        {
            if (buffList == null)
                return;
            List<TBSGameBuffInfo> deleteInfoList = new List<TBSGameBuffInfo>();
            foreach (TBSGameBuffInfo buffInfo in buffList)
            {
                deleteInfoList.Add(buffInfo);
            }
            buffList.Clear();
            foreach (TBSGameBuffInfo buffInfo in deleteInfoList)
            {
                SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_REMOVE_BUFF, buffInfo);
            }
        }

        /// <summary>
        /// 查找列表中的buff
        /// </summary>
        /// <param name="_buffDataID"></param>
        /// <returns></returns>
        private TBSGameBuffInfo findBuff(long _buffDataID)
        {
            foreach (var buffInfo in buffList)
            {
                if (buffInfo.buffRefObj.id == _buffDataID)
                {
                    return buffInfo;
                }
            }

            return default;
        }

        public void TriggerAttackBuff()
        {
            foreach (var buffInfo in buffList)
            {
                if (buffInfo == null)
                    continue;
                buffInfo.onAttack?.Invoke();
            }
        }

        public void TriggerGetHitBuff()
        {
            foreach (var buffInfo in buffList)
            {
                if (buffInfo == null)
                    continue;
                buffInfo.onGetHit?.Invoke();
            }
        }

        public void TriggerActorDieBuff()
        {
            foreach (var buffInfo in buffList)
            {
                if (buffInfo == null)
                    continue;
                buffInfo.onActorDie?.Invoke();
            }
        }

        public void TriggerActorActionBuff()
        {
            foreach (var buffInfo in buffList)
            {
                if (buffInfo == null)
                    continue;
                buffInfo.onActorAction?.Invoke();
            }
        }



    }
}
