using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSTrollActorMono : TBSActorMonoBase
    {
        [Header("普攻攻击动画持续时间")]
        public float attackAnimDuration;

        [Header("防御动画播放多久跳转下一个角色")]
        public float defendPlayTime;
    }
}
