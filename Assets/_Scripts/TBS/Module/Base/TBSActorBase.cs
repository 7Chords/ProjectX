using DG.Tweening;
using GameCore.Util;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSActorBase : TBSModuleBase
    {
        protected TBSActorMonoBase _m_actorMono;

        //public TBSActorMonoBase actorMono { get => _m_actorMono; private set { } }

        protected virtual string AttackAnimName { get => "Attack"; }
        protected virtual string RunAnimName { get => "Run"; }
        protected virtual string GetHitAnimName { get => "GetHit"; }
        protected virtual string IdleAnimName { get => "Idle"; }


        protected TweenContainer _m_tweenContainer;
        public TBSActorBase(TBSActorMonoBase _mono)
        {
            _m_actorMono = _mono;
        }

        public override void OnInitialize()
        {
            _m_tweenContainer = new TweenContainer();
        }
        public override void OnDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }

        public override void OnResume()
        {
        }

        public override void OnSuspend()
        {
        }

        public virtual Vector3 getEnemyAttackStandPos()
        {
            return _m_actorMono.transform.position - _m_actorMono.enemyAttackStopOffset;
        }

        public virtual Vector3 getCursorPos()
        {
            return _m_actorMono.transform.position + _m_actorMono.cursorOffset;
        }

        public virtual Vector3 getPos()
        {
            return _m_actorMono.transform.position;
        }

        public virtual void Attack(TBSActorBase _target) { }

        public virtual void ReleaseSkill(long skillId, TBSActorBase _target) { }
    }
}
