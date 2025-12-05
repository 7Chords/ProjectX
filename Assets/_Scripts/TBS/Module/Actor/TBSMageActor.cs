using DG.Tweening;
using GameCore.UI;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            TBSMageActorMono actorMono = _m_actorMono as TBSMageActorMono;
            Sequence seq = DOTween.Sequence();

            Tween lookAtTargetTween = _m_actorMono.gameObject.transform.DOLookAt(new Vector3(_target.GetGameObject().transform.position.x,
                GetGameObject().transform.position.y, _target.GetGameObject().transform.position.z), 0.25f);

            seq.Append(lookAtTargetTween);

            GameObject flyBall = null;
            float flyTime = Vector3.Distance(_target.GetPos(), actorMono.attackSourceTran.position) / actorMono.attackFlySpeed;
            seq.Append(DOVirtual.DelayedCall(actorMono.attackSpwanTime,
                () =>
                {
                    flyBall = ResourcesHelper.LoadGameObject(actorMono.attackSpawnObjName, actorMono.attackSourceTran.position, Quaternion.identity);
                    Vector3 dir = (_target.GetPos() - flyBall.transform.position).normalized;
                    flyBall.transform.LookAt(dir);
                    flyBall.GetComponent<Rigidbody>().velocity = dir * actorMono.attackFlySpeed;
                    flyBall.GetComponent<AttackFlyObj>().Initialize(_target.GetGameObject(), dealAttack);
                }).OnStart(
                ()=>
                {
                    _m_animationCtl.PlaySingleAniamtion(_m_attackAnimClip);
                }));

            seq.Append(DOVirtual.DelayedCall(actorMono.attackSpwanTime + flyTime,
                () =>
                {
                    _m_attackEnemyActorList.Remove(_target);
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ACTION_END,actorInfo.characterRefObj.id);
                    _m_animationCtl.PlaySingleAniamtion(_m_idleAnimClip);

                }));
            _m_tweenContainer?.RegDoTween(seq);

        }


        public override void ReleaseSkill(long skillId, TBSActorBase _target)
        {

        }
    }
}
