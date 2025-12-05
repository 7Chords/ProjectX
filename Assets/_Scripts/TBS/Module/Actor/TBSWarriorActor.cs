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

            Vector3 originalPos = _m_actorMono.gameObject.transform.position;

            Sequence seq = DOTween.Sequence();
            Tween lookAtTargetTween = _m_actorMono.gameObject.transform.DOLookAt(new Vector3(_target.GetGameObject().transform.position.x,
                GetGameObject().transform.position.y, _target.GetGameObject().transform.position.z), 0.25f);
            Tween move2AttackTween = _m_actorMono.gameObject.transform.DOMove(_target.GetEnemyAttackStandPos(), 1f)
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


            Tween rotateTween_1 = _m_actorMono.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f);

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
            Tween rotateTween_2 = _m_actorMono.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);
            

            seq.Append(lookAtTargetTween);
            seq.Append(move2AttackTween);

            seq.Append(DOVirtual.DelayedCall((_m_actorMono as TBSWarriorActorMono).attackAnimDuration, 
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


        public override void ReleaseSkill(long skillId, TBSActorBase _target)
        {
            _m_attackEnemyActorList.Add(_target);

            TBSActorSkillRefObj skillRefObj = SCRefDataMgr.instance.tbsActorSkillRefList.refDataList.Find(x => x.id == skillId);
            if (skillRefObj == null)
                return;
            PlayableAsset skillAsset = ResourcesHelper.LoadAsset<PlayableAsset>(skillRefObj.skillPlayableAssetName);
            if (skillAsset == null)
                return;
            _m_actorSkillRefObj = skillRefObj;
            if (!_m_actorSkillRefObj.needMove)
            {
                _m_actorMono.skillDirector.Play(skillAsset);
            }
            else
            {
                switch(_m_actorSkillRefObj.skillName)
                {
                    case "—∏Ω›π•ª˜":
                        {
                            _m_actorMono.signalEventTrigger.AddSignalEvent("CommonDealSkill", dealSkill);
                            Vector3 originalPos = _m_actorMono.gameObject.transform.position;
                            Sequence seq = DOTween.Sequence();
                            Tween move2AttackTween = _m_actorMono.gameObject.transform.DOMove(_target.GetEnemyAttackStandPos(), 0.5f)
                                .OnStart(
                                () =>
                                {
                                    GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSConfirm));
                                    GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSMain));
                                    GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
                                    TBSCursorMgr.instance.HideSelectionCursor();
                                    _m_animationCtl.speed = 2f;
                                    _m_animationCtl.PlaySingleAniamtion(_m_runAnimClip);
                                })
                                .OnComplete(
                                () =>
                                {
                                    _m_animationCtl.speed = 1f;
                                    _m_actorMono.skillDirector.Play(skillAsset);
                                });


                            Tween rotateTween_1 = _m_actorMono.gameObject.transform.DOLocalRotate(new Vector3(0, 180, 0), 0.5f);

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
                            Tween rotateTween_2 = _m_actorMono.gameObject.transform.DOLocalRotate(Vector3.zero, 0.5f);

                            seq.Append(move2AttackTween);

                            seq.Append(DOVirtual.DelayedCall((float)skillAsset.duration,
                                () =>
                                {
                                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.characterRefObj.id);
                                    _m_actorMono.signalEventTrigger.RemoveSignalEvent("CommonDealSkill");
                                    _m_attackEnemyActorList.Clear();

                                }));
                            seq.Append(rotateTween_1);
                            seq.Append(move2OriginalTween);
                            seq.Append(rotateTween_2);


                            _m_tweenContainer?.RegDoTween(seq);

                        }
                        break;
                    case "À≤…¡Õª¥Ã":
                        break;
                }
            }



        }
    }
}
