using DG.Tweening;
using GameCore.RefData;
using GameCore.UI;
using GameCore.Util;
using SCFrame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace GameCore.TBS
{
    public class TBSGiantActor : TBSActorBase, ITBSEnemyActor
    {
        public TBSGiantActor(TBSActorMonoBase _mono) : base(_mono)
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

            seq.Append(DOVirtual.DelayedCall((_m_actorMono as TBSGiantActorMono).attackAnimDuration,
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
            if (_targetList == null || _targetList.Count == 0)
                return;

            _m_attackEnemyActorList.AddRange(_targetList);

            TBSActorSkillRefObj skillRefObj = SCRefDataMgr.instance.tbsActorSkillRefList.refDataList.Find(x => x.id == _skillId);
            if (skillRefObj == null)
                return;
            PlayableAsset skillAsset = ResourcesHelper.LoadAsset<PlayableAsset>(skillRefObj.skillPlayableAssetName);
            if (skillAsset == null)
                return;
            _m_actorSkillRefObj = skillRefObj;
            GameCommon.ShowSkillNameTip(_m_actorSkillRefObj.skillName);

            switch (_m_actorSkillRefObj.skillName)
            {
                case "¾ÞÊ¯×¹":
                    {
                        GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSConfirm));
                        GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
                        TBSCursorMgr.instance.HideSelectionCursor();


                        List<CameraMovingPlayableAsset> skillAssetList = GetCameraMovingAssets(skillAsset);
                        skillAssetList[0].cameraMovingItem.lookAt = SCModel.instance.tbsModel.gameMono.playerLookEnemyCenterPos;
                        skillAssetList[0].cameraMovingItem.follow = GetModelGameObject().transform;
                        skillAssetList[0].cameraMovingItem.offset = SCModel.instance.tbsModel.gameMono.enemyLookPlayerCenterPos.position 
                            + new Vector3(0, 2, -20);
                        skillAssetList[0].cameraMovingItem.offsetTranslateDuration = 1f;
                        skillAssetList[0].cameraMovingItem.isPlayerOffset = false;

                        TBSGiantActorMono actorMono = _m_actorMono as TBSGiantActorMono;


                        GameObject flyBall = null;
                        float flyTime = Vector3.Distance(SCModel.instance.tbsModel.gameMono.enemyLookPlayerCenterPos.position, actorMono.tranFlyObjSpawn.position) / actorMono.flyObjSpeed;
                        _m_actorMono.signalEventTrigger.AddSignalEvent(GameConst.SPAWN_FLY_OBJ_EVENT, () =>
                        {
                            flyBall = ResourcesHelper.LoadGameObject("Rock", actorMono.tranFlyObjSpawn.position, Quaternion.identity);
                            Vector3 dir = (SCModel.instance.tbsModel.gameMono.enemyLookPlayerCenterPos.position - flyBall.transform.position).normalized;
                            flyBall.transform.LookAt(dir);
                            flyBall.GetComponent<Rigidbody>().velocity = dir * actorMono.flyObjSpeed;
                            flyBall.GetComponent<AttackFlyObj>().Initialize(SCModel.instance.tbsModel.gameMono.enemyLookPlayerCenterPos.gameObject, dealSkill);
                        });
                        _m_actorMono.skillDirector.Play(skillAsset);


                        Sequence seq = DOTween.Sequence();
                        seq.Append(DOVirtual.DelayedCall((float)skillAsset.duration + flyTime,
                            () =>
                            {
                                SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                                _m_actorMono.signalEventTrigger.RemoveSignalEvent(GameConst.SPAWN_FLY_OBJ_EVENT);
                                _m_attackEnemyActorList.Clear();

                            }));

                        _m_tweenContainer?.RegDoTween(seq);

                        break;
                    }
                default:
                    break;
            }

        }
    }
}
