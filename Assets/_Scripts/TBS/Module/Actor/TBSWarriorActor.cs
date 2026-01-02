using DG.Tweening;
using GameCore.RefData;
using GameCore.UI;
using SCFrame;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace GameCore.TBS
{

    public class TBSWarriorActor : TBSActorBase
    {
        public TBSWarriorActor(TBSActorMonoBase _mono) : base(_mono)
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

            seq.Append(DOVirtual.DelayedCall((_m_actorMono as TBSWarriorActorMono).attackAnimDuration, 
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
                GameCommon.ShowCommonTip("MP不足！");
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

            //GameCommon.ShowSkillNameTip(_m_actorSkillRefObj.skillName);

            if (!_m_actorSkillRefObj.needMove)
            {
                GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSConfirm));
                GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
                TBSCursorMgr.instance.HideSelectionCursor();
  

                _m_actorMono.signalEventTrigger.AddSignalEvent(GameConst.COMMON_DEAL_SKILL_EVENT, dealSkill);
                _m_actorMono.skillDirector.Play(skillAsset);


                Sequence seq = DOTween.Sequence();
                seq.Append(DOVirtual.DelayedCall((float)skillAsset.duration,
                    () =>
                    {
                        SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                        _m_actorMono.signalEventTrigger.RemoveSignalEvent(GameConst.COMMON_DEAL_SKILL_EVENT);
                        _m_attackEnemyActorList.Clear();

                    }));

                _m_tweenContainer?.RegDoTween(seq);
            }
            else
            {
                switch(_m_actorSkillRefObj.skillName)
                {
                    case "迅捷攻击":
                        {

                            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSConfirm));
                            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
                            TBSCursorMgr.instance.HideSelectionCursor();
                            GameCameraMgr.instance.SetCameraTarget(SCModel.instance.tbsModel.GetCurSelectSingleEnemyTargetActor().GetAsCameraTargetTran());

                            //该技能是单体攻击 所以取目标第一个 这边或许可以支持配置为多个敌人的处理 不过需要商榷todo

                            TBSActorBase target = _targetList[0];
                            if (target == null)
                                return;

                            _m_actorMono.signalEventTrigger.AddSignalEvent(GameConst.COMMON_DEAL_SKILL_EVENT, dealSkill);
                            Vector3 originalPos = _m_actorMono.gameObject.transform.position;

                            GameGeneralRefObj generalRefObj = SCRefDataMgr.instance.gameGeneralRefObj;
                            Sequence seq = DOTween.Sequence();
                            Tween lookAtTargetTween = _m_actorMono.goModel.transform.DOLookAt(new Vector3(target.GetActorGameObject().transform.position.x,
                                GetActorGameObject().transform.position.y, target.GetActorGameObject().transform.position.z), generalRefObj.tbsMeleeLookAtTargetDuration);


                            Tween move2AttackTween = _m_actorMono.goModel.transform.DOMove(target.GetEnemyAttackStandPos(), 0.5f)
                                .OnStart(
                                () =>
                                {
                                    _m_animationCtl.speed = 2f;
                                    _m_animationCtl.PlaySingleAniamtion(_m_runAnimClip);
                                })
                                .OnComplete(
                                () =>
                                {
                                    _m_animationCtl.speed = 1f;
                                    _m_actorMono.skillDirector.Play(skillAsset);
                                });


                            Tween rotateTween_1 = _m_actorMono.goModel.transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f);

                            Tween move2OriginalTween = _m_actorMono.goModel.transform.DOMove(originalPos, 1f)
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
                            Tween rotateTween_2 = _m_actorMono.goModel.transform.DOLocalRotate(Vector3.zero, 0.5f);


                            seq.Append(lookAtTargetTween);

                            seq.Append(move2AttackTween);

                            seq.Append(DOVirtual.DelayedCall((float)skillAsset.duration,
                                () =>
                                {
                                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                                    _m_actorMono.signalEventTrigger.RemoveSignalEvent(GameConst.COMMON_DEAL_SKILL_EVENT);
                                    _m_attackEnemyActorList.Clear();

                                }));
                            seq.Append(rotateTween_1);
                            seq.Append(move2OriginalTween);
                            seq.Append(rotateTween_2);


                            _m_tweenContainer?.RegDoTween(seq);

                        }
                        break;
                    case "四方剑影":
                        {
                            //GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSConfirm));
                            //GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSMain));
                            //GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
                            //TBSCursorMgr.instance.HideSelectionCursor();
                            //_m_actorMono.signalEventTrigger.AddSignalEvent("CommonDealSkill", dealSkill);
                            //_m_actorMono.skillDirector.Play(skillAsset);


                            //Sequence seq = DOTween.Sequence();
                            //seq.Append(DOVirtual.DelayedCall((float)skillAsset.duration,
                            //    () =>
                            //    {
                            //        SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                            //        _m_actorMono.signalEventTrigger.RemoveSignalEvent("CommonDealSkill");
                            //        _m_attackEnemyActorList.Clear();

                            //    }));

                            //_m_tweenContainer?.RegDoTween(seq);

                        }
                        break;
                }
            }



        }
    }
}
