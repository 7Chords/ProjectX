using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCFrame;
using UnityEngine.EventSystems;
using System;
using GameCore.TBS;

namespace GameCore.OW
{
    [Serializable]
    public class EnemyItem
    {
        public long enemyId;
        public int enemyLevel;
    }

    public class CommonEnemy : _ASCLifeGameObjBase
    {
        [Header("敌人列表")]
        public List<EnemyItem> enemyItemList;
        [Header("角色动画机")]
        public Animator animator;
        [Header("空闲动画名")]
        public string idleAnimName;
        private SCAnimationCtl _m_animCtl;
        [Header("玩家战败后位置")]
        public Transform tranPlayerLose;
        private bool _m_hasEnter;
        private void Start()
        {
            Initialize();
        }
        private void onCollisionEnter(Collision _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {
                List<ActorData> dataList = new List<ActorData>();
                for(int i =0;i<enemyItemList.Count;i++)
                {
                    ActorData data = new ActorData();
                    data.InitNew(enemyItemList[i].enemyId,enemyItemList[i].enemyLevel);
                    dataList.Add(data);
                }
                TBSGameStarter.instance.LoadTBSGame(SCDataMgr.instance.playerActorInfoList, dataList,this);
            }
        }
        private void onTriggerEnter(Collider _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER_SWORD)
            {
                List<ActorData> dataList = new List<ActorData>();
                for (int i = 0; i < enemyItemList.Count; i++)
                {
                    ActorData data = new ActorData();
                    data.InitNew(enemyItemList[i].enemyId, enemyItemList[i].enemyLevel);
                    dataList.Add(data);
                }
                TBSGameStarter.instance.LoadTBSGame(SCDataMgr.instance.playerActorInfoList, dataList, this);
            }
        }
        public override void OnInitialize()
        {
            _m_animCtl = new SCAnimationCtl();
            _m_animCtl.SetAnimator(animator);
            _m_animCtl.Initialize();

            if (!string.IsNullOrEmpty(idleAnimName))
                _m_animCtl.PlaySingleAniamtion(ResourcesHelper.LoadAsset<AnimationClip>(idleAnimName));

            this.AddCollisionEnter(onCollisionEnter);
            this.AddTriggerEnter(onTriggerEnter);

            OWEntityMgr.instance.RegisterEntity(this);

        }


        public override void OnDiscard()
        {
            _m_animCtl?.Discard();
            _m_animCtl = null;
            this.RemoveCollisionEnter(onCollisionEnter);
            this.RemoveTriggerEnter(onTriggerEnter);

            OWEntityMgr.instance.UnRegisterEntity(this);

        }

        public override void OnResume()
        {
        }

        public override void OnSuspend()
        {
        }
    }
}
