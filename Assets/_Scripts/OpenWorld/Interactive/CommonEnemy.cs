using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCFrame;
using UnityEngine.EventSystems;
using System;
using GameCore.TBS;

namespace GameCore.OW
{
    public class CommonEnemy : MonoBehaviour
    {
        [Header("敌人列表")]
        public List<long> enemyIdList;
        [Header("角色动画机")]
        public Animator animator;
        [Header("空闲动画名")]
        public string idleAnimName;
        private SCAnimationCtl _m_animCtl;

        private void Start()
        {
            _m_animCtl = new SCAnimationCtl();
            _m_animCtl.SetAnimator(animator);
            _m_animCtl.Initialize();

            if (!string.IsNullOrEmpty(idleAnimName))
                _m_animCtl.PlaySingleAniamtion(ResourcesHelper.LoadAsset<AnimationClip>(idleAnimName));

            this.AddCollisionEnter(onCollisionEnter);
        }
        private void OnDisable()
        {
            this.RemoveCollisionEnter(onCollisionEnter);
        }
        private void onCollisionEnter(Collision _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {
                List<ActorData> dataList = new List<ActorData>();
                for(int i =0;i<enemyIdList.Count;i++)
                {
                    ActorData data = new ActorData();
                    data.InitNew(enemyIdList[i]);
                    dataList.Add(data);
                }
                TBSBattleInfo battleInfo = new TBSBattleInfo();
                battleInfo.Init(SCSaveSys.instance.gameData.playerActorDataList, dataList);
                TBSGameStarter.instance.LoadTBSGame(SCSaveSys.instance.gameData.playerActorDataList, dataList);
                Destroy(gameObject);
            }
        }

    }
}
