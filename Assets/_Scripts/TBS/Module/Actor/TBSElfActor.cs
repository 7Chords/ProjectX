using DG.Tweening;
using GameCore.RefData;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSElfActor : TBSActorBase, ITBSEnemyActor
    {
        public TBSElfActor(TBSActorMonoBase _mono) : base(_mono)
        {
        }

        public override void Attack_All(List<TBSActorBase> _targetList)
        {
        }

        public override void Attack_Single(TBSActorBase _target)
        {

            _m_attackEnemyActorList.Add(_target);

            GameGeneralRefObj generalRefObj = SCRefDataMgr.instance.gameGeneralRefObj;

            TBSElfActorMono actorMono = _m_actorMono as TBSElfActorMono;
            Sequence seq = DOTween.Sequence();

            Tween lookAtTargetTween = _m_actorMono.goModel.transform.DOLookAt(new Vector3(_target.GetActorGameObject().transform.position.x,
                GetActorGameObject().transform.position.y, _target.GetActorGameObject().transform.position.z), generalRefObj.tbsMeleeLookAtTargetDuration);

            seq.Append(lookAtTargetTween);

            GameObject flyBall = null;
            float flyTime = Vector3.Distance(_target.GetModelPos(), actorMono.attackSourceTran.position) / actorMono.attackFlySpeed;
            seq.Append(DOVirtual.DelayedCall(actorMono.attackSpwanTime,
                () =>
                {
                    flyBall = ResourcesHelper.LoadGameObject(actorMono.attackSpawnObjName, actorMono.attackSourceTran.position, Quaternion.identity);
                    Vector3 dir = (_target.GetModelPos() - flyBall.transform.position).normalized;
                    flyBall.transform.LookAt(dir);
                    flyBall.GetComponent<Rigidbody>().velocity = dir * actorMono.attackFlySpeed;
                    flyBall.GetComponent<AttackFlyObj>().Initialize(_target.GetActorGameObject(), dealAttack);
                }).OnStart(
                () =>
                {
                    _m_animationCtl.PlaySingleAniamtion(_m_attackAnimClip);
                }));

            seq.Append(DOVirtual.DelayedCall(actorMono.attackSpwanTime + flyTime,
                () =>
                {
                    _m_attackEnemyActorList.Remove(_target);
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                    _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);

                }));

            seq.Append(_m_actorMono.goModel.transform.DOLocalRotate(Vector3.zero, generalRefObj.tbsMeleeRotateDuration));

            _m_tweenContainer?.RegDoTween(seq);
        }

        public void DealEnemyAction()
        {
            long actionId = TBSEnemyActionHandler.GetEnemyActionId(actorInfo.characterRefObj.id);

            if (actionId == GameConst.ENEMY_NORMAL_ATTACK_ID)
            {
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
            else
            {
                TBSActorSkillRefObj skillRefObj = SCRefDataMgr.instance.tbsActorSkillRefList.refDataList.Find(x => x.id == actionId);
                if (skillRefObj == null)
                    return;
                List<TBSActorBase> targetList = new List<TBSActorBase>();
                if (skillRefObj.damageTargetType == ETargetType.ALL)
                {
                    targetList = SCModel.instance.tbsModel.playerActorModuleList;
                    ReleaseSkill(actionId, targetList);
                }
                else if (skillRefObj.damageTargetType == ETargetType.SINGLE)
                {
                    TBSActorBase targetActor = SCModel.instance.tbsModel.GetRandomAliveActor(true);
                    if (targetActor == null)
                        return;
                    targetList.Add(targetActor);
                    ReleaseSkill(actionId, targetList);
                }

            }
        }

        public override void ReleaseSkill(long _skillId, List<TBSActorBase> _targetList)
        {
        }
    }

}
