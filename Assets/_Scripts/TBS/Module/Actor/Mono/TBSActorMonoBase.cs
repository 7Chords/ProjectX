using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TBSActorMonoBase : MonoBehaviour
{
    [Header("光标相对于pos的偏移")]
    public Vector3 cursorOffset;
    [Header("角色动画器")]
    public Animator actorAnim;
    [Header("角色底部特效位置")]
    public Transform bottomSfxTran;
    [Header("角色中部特效位置")]
    public Transform centerSfxTran;
    [Header("角色顶部特效位置")]
    public Transform topSfxTran;
    [Header("敌人攻击自己时停止位置的偏移")]
    public Vector3 enemyAttackStopOffset;
    [Header("攻击动画名")]
    public string attackAnimClipName;
    [Header("奔跑动画名")]
    public string runAnimClipName;
    [Header("受击动画名")]
    public string getHitAnimClipName;
    [Header("待机动画名")]
    public string idleAnimClipName;
    [Header("防御动画名")]
    public string defendAnimClipName;

}
