using GameCore.Util;
using SCFrame;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;

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

        protected TBSActorInfo _m_actorInfo;

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
            //这些是基础动画 至于每个角色的技能动画名配在对应的技能RefObj里面
            if(!string.IsNullOrEmpty(_m_actorMono.idleAnimClipName))
                _m_idleAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.idleAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.runAnimClipName))
                _m_runAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.runAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.attackAnimClipName))
                _m_attackAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.attackAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.getHitAnimClipName))
                _m_getHitAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.getHitAnimClipName);
            if (!string.IsNullOrEmpty(_m_actorMono.defendAnimClipName))
                _m_defendAnimClip = ResourcesHelper.LoadAsset<AnimationClip>(_m_actorMono.defendAnimClipName);

            if (_m_idleAnimClip != null)
                _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);
        }
        public override void OnDiscard()
        {
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

        public virtual Vector3 getEnemyAttackStandPos()
        {
            return _m_actorMono.transform.position - _m_actorMono.enemyAttackStopOffset;
        }

        public virtual Vector3 getCursorPos()
        {
            return _m_actorMono.transform.position + _m_actorMono.cursorOffset;
        }

        public virtual Vector3 getPos()
        {
            return _m_actorMono.transform.position;
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

        public abstract void Defend();

        public abstract void GetHit();

        public virtual void TakeDamage(int _damage)
        {
            if (_damage <= 0)
            {
                Debug.LogError("伤害量小于等于0，请检查！！！");
                return;
            }
            _m_actorInfo.curHp = Mathf.Max(_m_actorInfo.curHp - _damage, 0);
        }

        public virtual void TakeMagic(int _magicAmount)
        {
            if (_magicAmount <= 0)
            {
                Debug.LogError("魔法消耗量小于等于0，请检查！！！");
                return;
            }
            _m_actorInfo.curMp = Mathf.Max(_m_actorInfo.curHp - _magicAmount, 0);
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
            float randomNum = Random.Range(0, 1);
            if (randomNum < _m_actorInfo.missChance)
                return true;
            return false;
        }

        public virtual bool CriticalJudge()
        {
            float randomNum = Random.Range(0, 1);
            if (randomNum < _m_actorInfo.criticalChance)
                return true;
            return false;
        }

    }
}
