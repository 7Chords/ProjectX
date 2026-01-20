using GameCore.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.OW
{
    public class PlayerMono : MonoBehaviour
    {
        [Header("玩家控制配置")]
        public PlayerControlCfg controlCfg;
        [Header("玩家动画机")]
        public Animator playerAnim;
        [Header("玩家模型")]
        public GameObject playerModel;
        [Header("玩家物体")]
        public GameObject playerGO;
        [Header("玩家刚体")]
        public Rigidbody playerRb;
        [Header("动画事件触发器")]
        public AnimationEventTrigger animEventTrigger;
    }
}
