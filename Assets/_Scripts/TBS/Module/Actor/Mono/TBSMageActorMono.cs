using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSMageActorMono : TBSActorMonoBase
    {
        [Header("普攻攻击的发射点")]
        public Transform attackSourceTran;

        [Header("普攻攻击生成法球的动画时间")]
        public float attackSpwanTime;

        [Header("普攻法球的飞行速度")]
        public float attackFlySpeed;

        [Header("普攻法球的资质obj名")]
        public string attackSpawnObjName;

    }
}
