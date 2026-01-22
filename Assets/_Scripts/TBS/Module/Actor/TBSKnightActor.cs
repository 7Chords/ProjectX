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
    public class TBSKnightActor : TBSActorBase
    {
        public TBSKnightActor(TBSActorMonoBase _mono) : base(_mono)
        {
        }

        public override void Attack_All(List<TBSActorBase> _targetList)
        {

        }

        public override void Attack_Single(TBSActorBase _target)
        {
            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSMain));
            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));

            TBSCursorMgr.instance.HideSelectionCursor();

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

            seq.Append(DOVirtual.DelayedCall((_m_actorMono as TBSKnightActorMono).attackAnimDuration,
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

        public override void ReleaseSkill(long _skillId, List<TBSActorBase> _targetList)
        {
            if (!checkSkillCanRelease(_skillId))
            {
                TipQueueDealer.instance.EnqueueCommonTopTip("MP不足！");
                return;
            }

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


            GameGeneralRefObj generalRefObj = SCRefDataMgr.instance.gameGeneralRefObj;

            GameCommon.ShowSkillNameTip(_m_actorSkillRefObj.skillName);


            switch (_m_actorSkillRefObj.skillName)
            {
                case "骑士决心":
                    {
                        GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSConfirm));
                        GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSPlayerHud));

                        TBSCursorMgr.instance.HideSelectionCursor();

                        GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(SCModel.instance.tbsModel.gameMono.lookAllPlayersVC.transform.position, true, 0f);
                        GameCameraMgr.instance.SetCameraTarget(SCModel.instance.tbsModel.gameMono.lookAllPlayersVC_LookPos);


                        _m_actorMono.signalEventTrigger.AddSignalEvent(GameConst.SPAWN_PARTICLE_EFFECT_EVENT, () =>
                        {
                            GameObject go = ParticleMgr.instance.PlayEffect("circle_yellow"
                                , _targetList[0].GetActorGameObject().transform.position).gameObject;
                        });

                        _m_actorMono.skillDirector.Play(skillAsset);

                        dealSkill();


                        TBSGameBuffInfo buffInfo = TBSBuffFactory.CreateBuffInfo(1010, 3, _targetList[0]);
                        TBSGameBuffInfo buffInfo_2 = TBSBuffFactory.CreateBuffInfo(1011, 3, _targetList[0]);
                        _targetList[0].GetBuff(buffInfo);
                        _targetList[0].GetBuff(buffInfo_2);

                        TipQueueDealer.instance.EnqueueWorldPositionTip(buffInfo.buffRefObj.buffName, _targetList[0].GetCursorPos());
                        TipQueueDealer.instance.EnqueueWorldPositionTip(buffInfo_2.buffRefObj.buffName, _targetList[0].GetCursorPos());


                        Sequence seq = DOTween.Sequence();

                        seq.Append(DOVirtual.DelayedCall((float)skillAsset.duration,
                            () =>
                            {
                                SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                                _m_actorMono.signalEventTrigger.RemoveSignalEvent(GameConst.SPAWN_PARTICLE_EFFECT_EVENT);
                                _m_attackEnemyActorList.Clear();

                            }));

                        _m_tweenContainer?.RegDoTween(seq);
                    }
                    break;
                case "坚如磐石":
                    {
                        GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSConfirm));
                        GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSPlayerHud));

                        TBSCursorMgr.instance.HideSelectionCursor();

                        GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(SCModel.instance.tbsModel.gameMono.lookAllPlayersVC.transform.position, true, 0f);
                        GameCameraMgr.instance.SetCameraTarget(SCModel.instance.tbsModel.gameMono.lookAllPlayersVC_LookPos);


                        _m_actorMono.signalEventTrigger.AddSignalEvent(GameConst.SPAWN_PARTICLE_EFFECT_EVENT, () =>
                        {
                            ParticleMgr.instance.PlayEffect("circle_yellow", _targetList[0].GetActorGameObject().transform.position);
                            ParticleMgr.instance.PlayEffect("circle_yellow", _targetList[1].GetActorGameObject().transform.position);
                            ParticleMgr.instance.PlayEffect("circle_yellow", _targetList[2].GetActorGameObject().transform.position);

                        });

                        _m_actorMono.skillDirector.Play(skillAsset);

                        dealSkill();


                        TBSGameBuffInfo buffInfo = TBSBuffFactory.CreateBuffInfo(1004, 3, _targetList[0]);
                        TBSGameBuffInfo buffInfo_2 = TBSBuffFactory.CreateBuffInfo(1004, 3, _targetList[1]);
                        TBSGameBuffInfo buffInfo_3 = TBSBuffFactory.CreateBuffInfo(1004, 3, _targetList[2]);

                        _targetList[0].GetBuff(buffInfo);
                        _targetList[1].GetBuff(buffInfo_2);
                        _targetList[2].GetBuff(buffInfo_3);

                        TipQueueDealer.instance.EnqueueWorldPositionTip(buffInfo.buffRefObj.buffName, _targetList[0].GetCursorPos());
                        TipQueueDealer.instance.EnqueueWorldPositionTip(buffInfo.buffRefObj.buffName, _targetList[1].GetCursorPos());
                        TipQueueDealer.instance.EnqueueWorldPositionTip(buffInfo.buffRefObj.buffName, _targetList[2].GetCursorPos());


                        Sequence seq = DOTween.Sequence();

                        seq.Append(DOVirtual.DelayedCall((float)skillAsset.duration,
                            () =>
                            {
                                SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                                _m_actorMono.signalEventTrigger.RemoveSignalEvent(GameConst.SPAWN_PARTICLE_EFFECT_EVENT);
                                _m_attackEnemyActorList.Clear();

                            }));

                        _m_tweenContainer?.RegDoTween(seq);
                    }
                    break;
                default:
                    break;
            }
        }
    }

}
