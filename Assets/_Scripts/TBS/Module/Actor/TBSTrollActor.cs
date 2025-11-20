using DG.Tweening;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{

    public class TBSTrollActor : TBSActorBase , ITBSEnemyActor
    {
        public TBSTrollActor(TBSActorMonoBase _mono) : base(_mono)
        {
        }

        public override void Attack(TBSActorBase _target)
        {
            _m_actorMono.animEventTrigger.AddAnimationEvent("dealAttack", dealAttack);

            //todo
            _m_attackEnemyActorList.Add(_target);

            Vector3 originalPos = _m_actorMono.gameObject.transform.position;
            Sequence seq = DOTween.Sequence();
            Tween move2AttackTween = _m_actorMono.gameObject.transform.DOMove(_target.GetEnemyAttackStandPos(), 1f)
                .OnStart(
                () =>
                {
                    //GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSMain));
                    //GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));

                    //TBSCursorMgr.instance.HideSelectionCursor();
                    _m_animationCtl.PlaySingleAniamtion(_m_runAnimClip);
                })
                .OnComplete(
                () =>
                {
                    _m_animationCtl.PlaySingleAniamtion(_m_attackAnimClip);
                });


            Tween rotateTween_1 = _m_actorMono.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);

            Tween move2OriginalTween = _m_actorMono.gameObject.transform.DOMove(originalPos, 1f)
                .OnStart(
                () =>
                {
                    _m_animationCtl.PlaySingleAniamtion(_m_runAnimClip);
                })
                .OnComplete(
                () =>
                {
                    _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);
                });
            Tween rotateTween_2 = _m_actorMono.gameObject.transform.DOLocalRotate(new Vector3(0,180,0), 0.5f);




            seq.Append(move2AttackTween);

            seq.Append(DOVirtual.DelayedCall((_m_actorMono as TBSTrollActorMono).attackAnimDuration,
                () =>
                {
                    _m_attackEnemyActorList.Clear();
                    _m_actorMono.animEventTrigger.RemoveAnimationEvent("dealAttack");
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.characterRefObj.id);
                }));
            seq.Append(rotateTween_1);
            seq.Append(move2OriginalTween);
            seq.Append(rotateTween_2);


            _m_tweenContainer?.RegDoTween(seq);
        }

        public void DealEnemyAction(TBSActorBase _target)
        {
            Attack(_target);
        }

        public override void ReleaseSkill(long skillId, TBSActorBase _target)
        {
        }
    }
}
