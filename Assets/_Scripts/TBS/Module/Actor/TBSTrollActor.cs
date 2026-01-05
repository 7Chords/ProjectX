using DG.Tweening;
using GameCore.RefData;
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

        public override void Attack_All(List<TBSActorBase> _targetList)
        {
        }

        public override void Attack_Single(TBSActorBase _target)
        {
            _m_actorMono.animEventTrigger.AddAnimationEvent("dealAttack", dealAttack);

            _m_attackEnemyActorList.Add(_target);

            GameGeneralRefObj generalRefObj = SCRefDataMgr.instance.gameGeneralRefObj;

            Vector3 originalPos = _m_actorMono.goModel.transform.position;
            Sequence seq = DOTween.Sequence();
            Tween lookAtTargetTween = _m_actorMono.goModel.transform.DOLookAt(new Vector3(_target.GetActorGameObject().transform.position.x,
                GetActorGameObject().transform.position.y, _target.GetActorGameObject().transform.position.z), generalRefObj.tbsMeleeLookAtTargetDuration);
            Tween move2AttackTween = _m_actorMono.goModel.transform.DOMove(_target.GetEnemyAttackStandPos(), generalRefObj.tbsMeleeMoveToTargetDuration)
                .OnStart(
                () =>
                {
                    _m_animationCtl.PlaySingleAniamtion(_m_runAnimClip);
                })
                .OnComplete(
                () =>
                {
                    _m_animationCtl.PlaySingleAniamtion(_m_attackAnimClip);
                });


            Tween rotateTween_1 = _m_actorMono.goModel.transform.DOLocalRotate(new Vector3(0, 180, 0), generalRefObj.tbsMeleeRotateDuration);

            Tween move2OriginalTween = _m_actorMono.goModel.transform.DOMove(originalPos, generalRefObj.tbsMeleeMoveToOriginalDuration)
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
            Tween rotateTween_2 = _m_actorMono.goModel.transform.DOLocalRotate(Vector3.zero, generalRefObj.tbsMeleeRotateDuration);



            seq.Append(lookAtTargetTween);
            seq.Append(move2AttackTween);

            seq.Append(DOVirtual.DelayedCall((_m_actorMono as TBSTrollActorMono).attackAnimDuration,
                () =>
                {
                    _m_attackEnemyActorList.Clear();
                    _m_actorMono.animEventTrigger.RemoveAnimationEvent("dealAttack");
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                }));
            seq.Append(rotateTween_1);
            seq.Append(move2OriginalTween);
            seq.Append(rotateTween_2);


            _m_tweenContainer?.RegDoTween(seq);
        }

        public void DealEnemyAction()
        {
            //todo:完善逻辑 做普攻技能区分

            TBSActorBase targetActor = SCModel.instance.tbsModel.GetRandomAliveActor(true);
            if (targetActor == null)
                return;

            if (actorInfo.attackTargetType == ETargetType.ALL)
            {
                GameCameraMgr.instance.SetCameraTarget(SCModel.instance.tbsModel.gameMono.enemyLookPlayerCenterPos);
                Attack_All(SCModel.instance.tbsModel.playerActorModuleList);
            }
            else if (actorInfo.attackTargetType == ETargetType.SINGLE)
            {
                GameCameraMgr.instance.SetCameraTarget(targetActor.GetAsCameraTargetTran());
                Attack_Single(targetActor);
            }


        }

        public override void ReleaseSkill(long skillId, List<TBSActorBase> _targetList)
        {
        }
    }
}
