using GameCore.Util;
using SCFrame;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using GameCore.RefData;
using GameCore.UI;

namespace GameCore.TBS
{
    public abstract class TBSActorBase : TBSModuleBase
    {
        protected TBSActorMonoBase _m_actorMono;

        protected TweenContainer _m_tweenContainer;
        protected SCAnimationCtl _m_animationCtl;//动画控制器

        protected AnimationClip _m_idleAnimClip;
        protected AnimationClip _m_runAnimClip;
        protected AnimationClip _m_attackAnimClip;
        protected AnimationClip _m_getHitAnimClip;
        protected AnimationClip _m_defendAnimClip;
        protected AnimationClip _m_dieAnimClip;

        protected TBSActorInfo _m_actorInfo;
        public TBSActorInfo actorInfo => _m_actorInfo;

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
            _m_attackEnemyActorList = new List<TBSActorBase>();

            //这些是基础动画 至于每个角色的技能动画名配在对应的技能RefObj里面
            if (!string.IsNullOrEmpty(_m_actorMono.idleAnimClipName))
                _m_idleAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.idleAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.runAnimClipName))
                _m_runAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.runAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.attackAnimClipName))
                _m_attackAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.attackAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.getHitAnimClipName))
                _m_getHitAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.getHitAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.defendAnimClipName))
                _m_defendAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.defendAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.dieAnimClipName))
                _m_dieAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.dieAnimClipName);

            if (_m_idleAnimClip != null)
                _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);


            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_ACTION_END, onTBSActorActionEnd);

        }
        public override void OnDiscard()
        {

            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_ACTION_END, onTBSActorActionEnd);

            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
            _m_animationCtl?.Discard();
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

        public virtual Vector3 GetEnemyAttackStandPos()
        {
            return _m_actorMono.transform.position - _m_actorMono.enemyAttackStopOffset;
        }

        public virtual Vector3 GetCursorPos()
        {
            return _m_actorMono.transform.position + _m_actorMono.cursorOffset;
        }

        public virtual Vector3 GetDamageTextPos()
        {
            return _m_actorMono.transform.position + _m_actorMono.damageTextOffset;
        }
        public virtual Vector3 GetPos()
        {
            return _m_actorMono.transform.position;
        }

        public GameObject GetGameObject()
        {
            return _m_actorMono.gameObject;
        }
        public virtual void LookTarget(Vector3 _target,Action _onStart,Action _onFinish)
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

        public abstract void Attack(TBSActorBase _target);

        public abstract void ReleaseSkill(long skillId, TBSActorBase _target);

        public virtual void Defend()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(DOVirtual.DelayedCall(_m_actorMono.defendPlayTime,
                () =>
                {
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END, actorInfo.characterId);
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
            _m_animationCtl.PlaySingleAniamtion(_m_getHitAnimClip);
        }

        public virtual void Die()
        {
            _m_animationCtl.PlaySingleAniamtion(_m_dieAnimClip);
        }

        public virtual void TakeDamage(int _damage, bool _needShopFloatText = true , string _extraStr ="")
        {
            if (_damage <= 0)
            {
                Debug.LogError("伤害量小于等于0，请检查！！！");
                return;
            }
            _m_actorInfo.curHp = Mathf.Max(_m_actorInfo.curHp - _damage, 0);

            //ui飘字
            if(_needShopFloatText)
                GameCommon.ShowDamageFloatText(_damage, GetDamageTextPos(), _extraStr);

            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, actorInfo.characterId);
        }

        public virtual void TakeMagic(int _magicAmount)
        {
            if (_magicAmount <= 0)
            {
                Debug.LogError("魔法消耗量小于等于0，请检查！！！");
                return;
            }
            _m_actorInfo.curMp = Mathf.Max(_m_actorInfo.curHp - _magicAmount, 0);
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, actorInfo.characterId);
        }

        public virtual void HealHp(int _healAmount)
        {
            if (_healAmount <= 0)
            {
                Debug.LogError("治疗Hp小于等于0，请检查！！！");
                return;
            }
            _m_actorInfo.curHp = Mathf.Min(_m_actorInfo.curHp + _healAmount, _m_actorInfo.maxHp);
        }

        public virtual void HealMp(int _healAmount)
        {
            if (_healAmount <= 0)
            {
                Debug.LogError("治疗Mp小于等于0，请检查！！！");
                return;
            }
            _m_actorInfo.curMp = Mathf.Min(_m_actorInfo.curMp + _healAmount, _m_actorInfo.maxMp);
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

        }
        public virtual void GetAttackBounce()
        {

        }

        public virtual void GetAttackSuck()
        {

        }


        protected virtual void dealAttack()
        {
            TBSGameAttackInfo attackInfo = TBSAttackHandler.CreateTBSAttackInfo();
            attackInfo.srcActorList = new List<TBSActorBase>();
            attackInfo.srcActorList.Add(this);
            attackInfo.targetActorList = _m_attackEnemyActorList;

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

            skillInfo.baseDamage = actorInfo.attack;
            skillInfo.damageAmountType = _m_actorSkillRefObj.damageAmountType;
            skillInfo.damageType = _m_actorSkillRefObj.damageType;
            skillInfo.physicsLevelType = _m_actorSkillRefObj.physicsLevelType;
            skillInfo.magicAttributeType = _m_actorSkillRefObj.magicAttributeType;
            skillInfo.damageCauseType = EDamageCauseType.SKILL;
            //处理器处理技能信息
            TBSAttackHandler.DealSkill(skillInfo);
        }


        protected virtual void onTBSActorActionEnd(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            long characterId = (long)_objs[0];
            //恢复为idle
            if(characterId != actorInfo.characterId)
            {
                if (_m_idleAnimClip != null)
                    _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);
            }

        }
    }
}
