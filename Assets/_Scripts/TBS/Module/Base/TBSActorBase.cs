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

        public void SetData()
        {

        }

        public virtual Vector3 getEnemyAttackStandPos()
        {
            return _m_actorMono.transform.position - _m_actorMono.enemyAttackStopOffset;
        }

        //todo 临时测试动画 后续需构建伤害info
        public virtual void Attack(TBSActorBase _target)
        {
            Vector3 originalPos = _m_actorMono.gameObject.transform.position;
            Sequence seq = DOTween.Sequence();
            Tween move2AttackTween = _m_actorMono.gameObject.transform.DOMove(_target.getEnemyAttackStandPos(), 1f)
                .OnStart(
                () =>
                {
                    GameCoreMgr.instance.uiCoreMgr.HideCurNode();
                    TBSCursorMgr.instance.HideSelectionCursor();
                    _m_actorMono.actorAnim.Play(RunAnimName);
                })
                .OnComplete(
                () =>
                {
                    _m_actorMono.actorAnim.Play(AttackAnimName);
                });


            Tween rotateTween_1 = _m_actorMono.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f);

            Tween move2OriginalTween = _m_actorMono.gameObject.transform.DOMove(originalPos, 1f)
                .OnStart(
                ()=>
                {
                    _m_actorMono.actorAnim.Play(RunAnimName);
                })
                .OnComplete(
                () =>
                {
                    _m_actorMono.actorAnim.Play(IdleAnimName);
                });
            Tween rotateTween_2 = _m_actorMono.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);

            seq.Append(move2AttackTween);
            //todo：根据动画时长具体调整
            seq.Append(DOVirtual.DelayedCall(1f, () => { })
               .OnComplete(() =>
               {
                   SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_CHG, 1);
               }));
            seq.Append(rotateTween_1);
            seq.Append(move2OriginalTween);
            seq.Append(rotateTween_2);


            _m_tweenContainer?.RegDoTween(seq);

        }
    }
}
