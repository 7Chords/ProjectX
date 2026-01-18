using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCFrame;
using UnityEngine.EventSystems;
using System;

namespace GameCore.OW
{
    public class CommonEnemy : MonoBehaviour
    {
        private void Start()
        {
            this.AddCollisionEnter(onCollisionEnter);
        }
        private void OnDestroy()
        {
            this.RemoveCollisionEnter(onCollisionEnter);
        }
        private void onCollisionEnter(Collision _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {
                SCMsgCenter.SendMsg(SCMsgConst.TBS_GAME_START);
                Destroy(gameObject);
            }
        }

    }
}
