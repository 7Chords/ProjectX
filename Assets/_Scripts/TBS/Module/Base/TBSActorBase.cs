using GameCore.Util;
using SCFrame;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using GameCore.RefData;
using GameCore.UI;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace GameCore.TBS
{
    public abstract class TBSActorBase : TBSModuleBase
    {
        protected TBSActorMonoBase _m_actorMono;

        protected TweenContainer _m_tweenContainer;
        protected SCAnimationCtl _m_animationCtl;//动画控制器
        protected TBSBuffHandler _m_buffHander;
        protected TBSPropertyDealer _m_propertyDealer;

        public TBSPropertyDealer propertyDealer => _m_propertyDealer;

        protected AnimationClip _m_idleAnimClip;
        protected AnimationClip _m_runAnimClip;
        protected AnimationClip _m_attackAnimClip;
        protected AnimationClip _m_getHitAnimClip;
        protected AnimationClip _m_defendAnimClip;
        protected AnimationClip _m_dieAnimClip;

        protected TBSActorInfo _m_actorInfo;
        public TBSActorInfo actorInfo => _m_actorInfo;

        protected TBSPosInfo _m_posInfo;

        public TBSPosInfo posInfo => _m_posInfo;

        protected List<TBSActorBase> _m_attackEnemyActorList;

        protected TBSActorSkillRefObj _m_actorSkillRefObj;
        public TBSActorBase(TBSActorMonoBase _mono)
        {
            _m_actorMono = _mono;
        }

        public override void OnInitialize()
        {
            if (_m_actorMono == null)
                return;
            _m_tweenContainer = new TweenContainer();
            _m_animationCtl = new SCAnimationCtl();
            _m_animationCtl.SetAnimator(_m_actorMono.actorAnim);
            _m_animationCtl.Initialize();

            _m_propertyDealer = new TBSPropertyDealer();
            _m_propertyDealer.Initialize();
            _m_propertyDealer.SetActorInfo(_m_actorInfo);

            _m_buffHander = new TBSBuffHandler();
            _m_buffHander.Initialize();

            _m_attackEnemyActorList = new List<TBSActorBase>();

            //这些是基础动画 至于每个角色的技能动画名配在对应的技能RefObj里面
            if (!string.IsNullOrEmpty(_m_actorMono.idleAnimClipName))
                _m_idleAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.idleAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.runAnimClipName))
                _m_runAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.runAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.attackAnimClipName))
                _m_attackAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.attackAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.getHitAnimClipName))
            {
                _m_getHitAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.getHitAnimClipName);
                _m_actorMono.animEventTrigger.AddAnimationEvent(SCConst.PLAY_IDLE_ANIM_EVENT, () =>
                {
                    _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);
                });
            }
            if (!string.IsNullOrEmpty(_m_actorMono.defendAnimClipName))
                _m_defendAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.defendAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.dieAnimClipName))
                _m_dieAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.dieAnimClipName);

            if (_m_idleAnimClip != null)
                _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);


            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_CHG, onTBSActorChg);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_TURN_CHG, onTurnChg);
        }
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_CHG, onTBSActorChg);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_TURN_CHG, onTurnChg);

            _m_actorMono.animEventTrigger?.CleanAllActionEvent();

            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
            _m_animationCtl?.Discard();
            _m_buffHander?.Discard();
            _m_propertyDealer?.Discard();

        }

        public override void OnResume()
        {
        }

        public override void OnSuspend()
        {
        }

        public virtual void SetActorInfo(TBSActorInfo _actorInfo)
        {
            _m_actorInfo = _actorInfo;
        }

        public virtual void SetPosInfo(TBSPosInfo _posInfo)
        {
            _m_posInfo = _posInfo;
        }

        public virtual Vector3 GetEnemyAttackStandPos()
        {
            return _m_actorMono.transform.position - _m_actorMono.enemyAttackStopOffset;
        }

        public virtual Vector3 GetCursorPos()
        {
            if (posInfo == null)
                return Vector3.zero;
            return posInfo.posTran.position + _m_actorMono.cursorOffset;
        }

        public virtual Vector3 GetDamageTextPos()
        {
            return _m_actorMono.goModel.transform.position + _m_actorMono.damageTextOffset;
        }
        public virtual Vector3 GetModelPos()
        {
            return _m_actorMono.goModel.transform.position;
        }

        public virtual Vector3 GetDetailCameraPos()
        {
            return _m_actorMono.transform.position + _m_actorMono.detailCamOffset;
        }

        public GameObject GetActorGameObject()
        {
            return _m_actorMono.gameObject;
        }

        public GameObject GetModelGameObject()
        {
            return _m_actorMono.goModel;
        }

        public Transform GetAsCameraTargetTran()
        {
            return _m_actorMono.asCameraTargetTran;
        }

        public Vector3 GetOpenSkillCameraPos()
        {
            GameGeneralRefObj generalRefObj = SCRefDataMgr.instance.gameGeneralRefObj;
            if (generalRefObj == null)
                return GetActorCameraTran().position;
            return GetActorCameraTran().position + new Vector3(0, generalRefObj.tbsOpenSkillAndItemCameraOffsetY, 0);
        }

        public Transform GetActorCameraTran()
        {
            return _m_actorMono.actorCameraInfoList.Find(x => x.posType == posInfo.posType).cameraTran;
        }

        public List<TBSGameBuffInfo> GetBuffInfoList()
        {
            return _m_buffHander?.buffList;
        }

        public virtual void LookTarget(Vector3 _target, Action _onStart, Action _onFinish)
        {
            if (_target == _m_actorMono.gameObject.transform.rotation.eulerAngles)
            {
                return;
            }
            float duration = SCRefDataMgr.instance.gameGeneralRefObj.tbsActorSingleRotateTime;
            Tween tween = _m_actorMono.gameObject.transform.DOLookAt(_target, duration).OnStart(() =>
            {
                _onStart?.Invoke();
            }).OnComplete(() =>
            {
                _onFinish?.Invoke();
            });

            _m_tweenContainer.RegDoTween(tween);
        }

        public virtual void Attack(ETargetType _targetType, List<TBSActorBase> _targetList)
        {
            if (_targetList == null || _targetType == ETargetType.NONE)
                return;
            if (_targetType == ETargetType.SINGLE)
                Attack_Single(_targetList[0]);
            else if (_targetType == ETargetType.ALL)
                Attack_All(_targetList);

            _m_buffHander?.TriggerAttackBuff();

        }

        public abstract void Attack_Single(TBSActorBase _target);
        public abstract void Attack_All(List<TBSActorBase> _targetList);


        public abstract void ReleaseSkill(long _skillId, List<TBSActorBase> _targetList);

        public virtual void UseItem(long _itemId, List<TBSActorBase> _targetList)
        {
            ItemRefObj itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == _itemId);
            if (itemRefObj == null)
            {
                SCDebugHelper.LogError("找不到id为" + _itemId + "的道具配表数据！！！");
                return;
            }
            GameGeneralRefObj generalRefObj = SCRefDataMgr.instance.gameGeneralRefObj;
            if (generalRefObj == null)
            {
                SCDebugHelper.LogError("generalRefObj为空！！！");
                return;
            }
            if (!checkItemCanUse(_itemId))
            {
                GameCommon.ShowCommonTopTip("道具不满足使用条件！");
                return;
            }

            //数据上消耗道具
            SCDataMgr.instance.DeleteItem(_itemId, 1);


            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSConfirm));
            if (itemRefObj.isPlayerTarget)
                GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSPlayerHud));
            else
                GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));

            TBSCursorMgr.instance.HideSelectionCursor();

            if (itemRefObj.isPlayerTarget)
            {
                GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(SCModel.instance.tbsModel.gameMono.playerLookEnemyCenterPos.position, true, 0f);
                GameCameraMgr.instance.SetCameraTarget(SCModel.instance.tbsModel.gameMono.enemyLookPlayerCenterPos);
            }
            else
            {
                GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(SCModel.instance.tbsModel.GetCurActor().GetActorCameraTran().position, true, 0f);
                GameCameraMgr.instance.SetCameraTarget(SCModel.instance.tbsModel.gameMono.playerLookEnemyCenterPos);
            }
            TBSItemHandler.DealItem(itemRefObj, _targetList);

            Tween delayTween = DOVirtual.DelayedCall(generalRefObj.tbsUseItemKeepDuration, () =>
            {
                SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
            });
            _m_tweenContainer?.RegDoTween(delayTween);

        }
        public virtual void Defend()
        {
            //设置防御状态
            actorInfo.isDefending = true;
            Sequence seq = DOTween.Sequence();
            seq.Append(DOVirtual.DelayedCall(_m_actorMono.defendPlayTime,
                () =>
                {
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
                })
                .OnStart(() =>
                {
                    GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSMain));
                    GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
                    TBSCursorMgr.instance.HideSelectionCursor();
                    _m_animationCtl.PlaySingleAniamtion(_m_defendAnimClip);

                }));
            _m_tweenContainer?.RegDoTween(seq);
        }

        public virtual void GetHit()
        {
            if (actorInfo.isDefending)
                return;

            GameGeneralRefObj generalRefObj = SCRefDataMgr.instance.gameGeneralRefObj;
            if (generalRefObj == null)
                return;
            GameCameraMgr.instance.ShakeCamera(generalRefObj.tbsGetHitCamShakeDuration, generalRefObj.tbsGetHitCamShakeStrength);
            GameCameraMgr.instance.FreezeCamera(generalRefObj.tbsGetHitCamFreezeDuration);

            if (_m_getHitAnimClip != null)
                _m_animationCtl.PlaySingleAniamtion(_m_getHitAnimClip);

            _m_buffHander?.TriggerGetHitBuff();

        }

        public virtual void Die()
        {
            _m_actorInfo.hasDead = true;

            if (_m_dieAnimClip != null)
            {
                //如果是敌人的话 播放完死亡动画要销毁
                if (actorInfo.isEnemy)
                {
                    _m_actorMono.animEventTrigger.RemoveAnimationEvent("showDieOver");
                    _m_actorMono.animEventTrigger.AddAnimationEvent("showDieOver", showDieOver);
                }
                _m_animationCtl.PlaySingleAniamtion(_m_dieAnimClip);
            }

            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_DIE, actorInfo.runningId);

            _m_buffHander?.TriggerActorDieBuff();

        }

        public virtual void TakeDamage(int _damage, bool _needShowFloatText = true, string _extraStr = "")
        {
            if (_damage <= 0)
            {
                return;
            }
            _m_actorInfo.curHp = Mathf.Max(_m_actorInfo.curHp - _damage, 0);

            //ui飘字
            if (_needShowFloatText)
                GameCommon.ShowDamageFloatText(_damage, GetDamageTextPos(), _extraStr);
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, actorInfo.runningId);
            if (_m_actorInfo.curHp == 0)
                Die();
            else
                GetHit();
        }

        public virtual void TakeMagic(int _magicAmount)
        {
            if (_magicAmount <= 0)
            {
                return;
            }
            _m_actorInfo.curMp = Mathf.Max(_m_actorInfo.curMp - _magicAmount, 0);
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, actorInfo.runningId);
        }

        public virtual void HealHp(int _healAmount)
        {
            if (_healAmount <= 0)
            {
                Debug.LogError("治疗Hp小于等于0，请检查！！！");
                return;
            }
            _m_actorInfo.curHp = Mathf.Min(_m_actorInfo.curHp + _healAmount, _m_actorInfo.maxHp);
            GameCommon.ShowHealFloatText(_healAmount, GetDamageTextPos(), "");
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, actorInfo.runningId);
        }

        public virtual void HealMp(int _healAmount)
        {
            if (_healAmount <= 0)
            {
                Debug.LogError("治疗Mp小于等于0，请检查！！！");
                return;
            }
            _m_actorInfo.curMp = Mathf.Min(_m_actorInfo.curMp + _healAmount, _m_actorInfo.maxMp);
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, actorInfo.runningId);
        }

        public virtual void Rebirth(float _healRatio)
        {
            actorInfo.hasDead = false;
            HealHp(Mathf.RoundToInt(_healRatio * actorInfo.maxHp));
            if (_m_idleAnimClip != null)
                _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);
        }

        public virtual bool MissJudge()
        {
            float randomNum = Random.Range(0f, 1f);
            if (randomNum < _m_actorInfo.missChance)
                return true;
            return false;
        }

        public virtual void Miss()
        {
            GameCommon.ShowAttackStateText(ETBSAttackState.MISS, GetDamageTextPos());
        }

        public virtual bool CriticalJudge()
        {
            float randomNum = Random.Range(0f, 1f);
            if (randomNum < _m_actorInfo.criticalChance)
                return true;
            return false;
        }

        public virtual void GetAttackInvalid()
        {
            GameCommon.ShowAttackStateText(ETBSAttackState.INVALID, GetDamageTextPos());
        }
        public virtual void GetAttackBounce()
        {
            GameCommon.ShowAttackStateText(ETBSAttackState.BOUNCE, GetDamageTextPos());
        }

        public virtual void GetAttackSuck()
        {
            GameCommon.ShowAttackStateText(ETBSAttackState.SUCK, GetDamageTextPos());
        }


        protected virtual void dealAttack()
        {
            TBSGameAttackInfo attackInfo = TBSAttackHandler.CreateTBSAttackInfo();
            attackInfo.srcActorList = new List<TBSActorBase>();
            attackInfo.srcActorList.Add(this);
            attackInfo.targetActorList = _m_attackEnemyActorList;

            //普通攻击不消耗血和蓝
            //attackInfo.srcUseHpList = new List<int>();
            //attackInfo.srcUseHpList.Add(_m_actorSkillRefObj.skillNeedHp);

            //attackInfo.srcUseMpList = new List<int>();
            //attackInfo.srcUseMpList.Add(_m_actorSkillRefObj.skillNeedMp);

            attackInfo.baseDamage = actorInfo.attack;
            attackInfo.damageType = actorInfo.attackDamageType;
            attackInfo.physicsLevelType = actorInfo.attackPhysicalLevel;
            attackInfo.magicAttributeType = actorInfo.attackMagicAttribute;
            attackInfo.damageCauseType = EDamageCauseType.ATTACK;
            //处理器处理攻击信息
            TBSAttackHandler.DealAttack(attackInfo);
        }

        protected virtual void dealSkill()
        {
            if (_m_actorSkillRefObj == null)
                return;
            TBSGameSkillInfo skillInfo = TBSAttackHandler.CreateTBSSkillInfo();
            skillInfo.srcActorList = new List<TBSActorBase>();
            skillInfo.srcActorList.Add(this);
            skillInfo.targetActorList = _m_attackEnemyActorList;

            skillInfo.srcUseHpList = new List<int>();
            skillInfo.srcUseHpList.Add(_m_actorSkillRefObj.skillNeedHp);

            skillInfo.srcUseMpList = new List<int>();
            skillInfo.srcUseMpList.Add(_m_actorSkillRefObj.skillNeedMp);

            skillInfo.skillEffectType = _m_actorSkillRefObj.skillEffectType;
            skillInfo.baseDamage = actorInfo.attack;
            skillInfo.damageAmountType = _m_actorSkillRefObj.damageAmountType;
            skillInfo.damageType = _m_actorSkillRefObj.damageType;
            skillInfo.physicsLevelType = _m_actorSkillRefObj.physicsLevelType;
            skillInfo.magicAttributeType = _m_actorSkillRefObj.magicAttributeType;
            skillInfo.damageCauseType = EDamageCauseType.SKILL;
            //处理器处理技能信息
            TBSAttackHandler.DealSkill(skillInfo);
        }

        public virtual void GetBuff(TBSGameBuffInfo _buffInfo)
        {
            if (_buffInfo == null)
                return;
            if (_m_buffHander == null)
                return;
            _m_buffHander.AddBuff(_buffInfo);
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_GET_BUFF, _buffInfo);

        }

        public virtual void RemoveBuff(TBSGameBuffInfo _buffInfo)
        {
            if (_buffInfo == null)
                return;
            if (_m_buffHander == null)
                return;
            _m_buffHander.RemoveBuff(_buffInfo);
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_REMOVE_BUFF, _buffInfo);

        }
        //public virtual void DealItem()
        //{

        //    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.runningId);
        //}

        protected bool checkSkillCanRelease(long _skillId)
        {
            TBSActorSkillRefObj skillRefObj = SCRefDataMgr.instance.tbsActorSkillRefList.refDataList.Find(x => x.id == _skillId);
            if (skillRefObj == null)
                return false;
            return actorInfo.curHp >= skillRefObj.skillNeedHp &&
                actorInfo.curMp >= skillRefObj.skillNeedMp;
        }
        protected bool checkItemCanUse(long _itemId)
        {
            ItemRefObj itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == _itemId);
            if (itemRefObj == null)
                return false;
            switch (itemRefObj.id)
            {
                case 1003://复活卷轴
                    if (SCModel.instance.tbsModel.CheckHasPlayerActorDead())
                        return true;
                    return false;
                default:
                    break;
            }
            return true;
        }
        private void onTBSActorChg()
        {
            TBSActorBase actor = SCModel.instance.tbsModel.GetCurActor();
            if (actor == null)
                return;
            if (actor == this)
            {
                //取消防御状态
                actorInfo.isDefending = false;
                if (_m_idleAnimClip != null)
                    _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);

                _m_buffHander?.TriggerActorActionBuff();

            }
        }

        private void onTurnChg()
        {
            if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.PLAYER)
            {
                _m_buffHander?.BuffTickAndRemove();
            }
        }

        protected virtual void showDieOver()
        {
            SCCommon.DestoryGameObject(GetActorGameObject());
        }

        protected List<CameraMovingPlayableAsset> GetCameraMovingAssets(PlayableAsset _skillAsset)
        {
            TrackAsset targetTrack = null;

            foreach (PlayableBinding pb in _skillAsset.outputs)
            {
                targetTrack = pb.sourceObject as TrackAsset;
                if (targetTrack is CameraMovingTrack)
                {
                    break;
                }
            }
            List<CameraMovingPlayableAsset> assetList = new List<CameraMovingPlayableAsset>();
            foreach (TimelineClip clip in targetTrack.GetClips())
            {
                assetList.Add(clip.asset as CameraMovingPlayableAsset);
            }
            return assetList;
        }
    }
}
