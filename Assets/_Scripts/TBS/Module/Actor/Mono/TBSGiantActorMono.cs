using UnityEngine;

namespace GameCore.TBS
{
    public class TBSGiantActorMono : TBSActorMonoBase
    {
        [Header("普攻攻击动画持续时间")]
        public float attackAnimDuration;
        [Header("生成飞行物位置")]
        public Transform tranFlyObjSpawn;
        [Header("飞行物速度")]
        public float flyObjSpeed;
    }

}
