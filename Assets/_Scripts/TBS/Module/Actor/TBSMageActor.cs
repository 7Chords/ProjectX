using DG.Tweening;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSMageActor : TBSActorBase
    {
        public TBSMageActor(TBSActorMonoBase _mono) : base(_mono)
        {
        }

        public override void Attack(TBSActorBase _target)
        {
            TBSMageActorMono actorMono = _m_actorMono as TBSMageActorMono;
            Sequence seq = DOTween.Sequence();
            GameObject flyBall = null;
            float flyTime = Vector3.Distance(_target.getPos(), actorMono.attackSourceTran.position) / actorMono.attackFlySpeed;
            seq.Append(DOVirtual.DelayedCall(actorMono.attackSpwanTime,
                () =>
                {
                    flyBall = ResourcesHelper.LoadGameObject(actorMono.attackSpawnObjName, actorMono.attackSourceTran.position, Quaternion.identity);
                    Vector3 dir = (_target.getPos() - flyBall.transform.position).normalized;
                    flyBall.transform.LookAt(dir);
                    flyBall.GetComponent<Rigidbody>().velocity = dir * actorMono.attackFlySpeed;
                }).OnStart(
                ()=>
                {
                    GameCoreMgr.instance.uiCoreMgr.HideCurNode();
                    TBSCursorMgr.instance.HideSelectionCursor();
                    _m_animationCtl.PlaySingleAniamtion(_m_attackAnimClip);
                }));

            seq.Append(DOVirtual.DelayedCall(actorMono.attackSpwanTime + flyTime,
                () =>
                {
                    SCCommon.DestoryGameObject(flyBall);
                    SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_ACTION_END);
                    _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);

                }));
        }

        public override void Defend()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(DOVirtual.DelayedCall((_m_actorMono as TBSMageActorMono).defendPlayTime,
                () =>
                {
                    SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ACTOR_ACTION_END);
                })
                .OnStart(() =>
                {
                    GameCoreMgr.instance.uiCoreMgr.HideCurNode();
                    TBSCursorMgr.instance.HideSelectionCursor();
                    _m_animationCtl.PlaySingleAniamtion(_m_defendAnimClip);

                }));
            _m_tweenContainer?.RegDoTween(seq);

        }

        public override void GetHit()
        {
            throw new System.NotImplementedException();
        }

        public override void ReleaseSkill(long skillId, TBSActorBase _target)
        {

        }
    }
}
