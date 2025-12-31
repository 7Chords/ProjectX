using DG.Tweening;
using GameCore.RefData;
using GameCore.UI;
using GameCore.Util;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace GameCore.TBS
{
    public class TBSMageActor : TBSActorBase
    {
        public TBSMageActor(TBSActorMonoBase _mono) : base(_mono)
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

            _m_attackEnemyActorList.Add(_target);

            GameGeneralRefObj generalRefObj = SCRefDataMgr.instance.gameGeneralRefObj;

            TBSMageActorMono actorMono = _m_actorMono as TBSMageActorMono;
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
                ()=>
                {
                    _m_animationCtl.PlaySingleAniamtion(_m_attackAnimClip);
                }));

            seq.Append(DOVirtual.DelayedCall(actorMono.attackSpwanTime + flyTime,
                () =>
                {
                    _m_attackEnemyActorList.Remove(_target);
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END,actorInfo.runningId);
                    _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);

                }));

            seq.Append(_m_actorMono.goModel.transform.DOLocalRotate(Vector3.zero, generalRefObj.tbsMeleeRotateDuration));

            _m_tweenContainer?.RegDoTween(seq);

        }


        public override void ReleaseSkill(long _skillId, List<TBSActorBase> _targetList)
        {
            if (!checkSkillCanRelease(_skillId))
            {
                GameCommon.ShowCommonTip("MP²»×ã£¡");
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

            switch (_m_actorSkillRefObj.skillName)
            {
                case "À¶ÑæÍÂÏ¢":
                    {
                        GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSConfirm));
                        GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
                        TBSCursorMgr.instance.HideSelectionCursor();

                        _m_actorMono.signalEventTrigger.AddSignalEvent(GameConst.SPAWN_DAMAGE_AREA_EVENT, ()=>
                        {
                            GameObject go = ParticleMgr.instance.PlayEffect("snow_hit"
                                , _targetList[0].GetActorGameObject().transform.position).gameObject;
                            go.GetComponent<CommonDamageArea>().Initialize(_targetList[0].GetActorGameObject(), dealSkill);
                        });

                        _m_actorMono.skillDirector.Play(skillAsset);


                        Sequence seq = DOTween.Sequence();

                        Tween lookAtTargetTween = _m_actorMono.goModel.transform.DOLookAt(new Vector3(_targetList[0].GetActorGameObject().transform.position.x,
                            GetActorGameObject().transform.position.y, _targetList[0].GetActorGameObject().transform.position.z), generalRefObj.tbsMeleeLookAtTargetDuration);

                        seq.Append(lookAtTargetTween);
                        seq.Append(DOVirtual.DelayedCall((float)skillAsset.duration,
                            () =>
                            {
                                SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                                _m_actorMono.signalEventTrigger.RemoveSignalEvent(GameConst.SPAWN_DAMAGE_AREA_EVENT);
                                _m_attackEnemyActorList.Clear();

                            }));
                        seq.Append(_m_actorMono.goModel.transform.DOLocalRotate(Vector3.zero, generalRefObj.tbsMeleeRotateDuration));

                        _m_tweenContainer?.RegDoTween(seq);
                    }
                    break;
                default:
                    break;
            }


            //if (!_m_actorSkillRefObj.needMove)
            //{
            //    GameCoreMgr.instance.uiCoreMgr.RemoveNode(nameof(UINodeTBSConfirm));
            //    GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
            //    TBSCursorMgr.instance.HideSelectionCursor();


            //    _m_actorMono.signalEventTrigger.AddSignalEvent("CommonDealSkill", dealSkill);
            //    _m_actorMono.skillDirector.Play(skillAsset);


            //    Sequence seq = DOTween.Sequence();
            //    seq.Append(DOVirtual.DelayedCall((float)skillAsset.duration,
            //        () =>
            //        {
            //            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
            //            _m_actorMono.signalEventTrigger.RemoveSignalEvent("CommonDealSkill");
            //            _m_attackEnemyActorList.Clear();

            //        }));

            //    _m_tweenContainer?.RegDoTween(seq);
            //}
            //else
            //{
            //    switch (_m_actorSkillRefObj.skillName)
            //    {
            //    }
            //}
        }
    }
}
