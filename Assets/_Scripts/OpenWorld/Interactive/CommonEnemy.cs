using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCFrame;
using UnityEngine.EventSystems;
using System;
using GameCore.TBS;

namespace GameCore.OW
{
    public class CommonEnemy : MonoBehaviour
    {
        public List<long> enemyIdList;
        private void Start()
        {
            this.AddCollisionEnter(onCollisionEnter);
        }
        private void OnDisable()
        {
            this.RemoveCollisionEnter(onCollisionEnter);
        }
        private void onCollisionEnter(Collision _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {
                List<ActorData> dataList = new List<ActorData>();
                for(int i =0;i<enemyIdList.Count;i++)
                {
                    ActorData data = new ActorData();
                    data.InitNew(enemyIdList[i]);
                    dataList.Add(data);
                }
                TBSBattleInfo battleInfo = new TBSBattleInfo();
                battleInfo.Init(SCSaveSys.instance.gameData.playerActorDataList, dataList);
                TBSGameStarter.instance.LoadTBSGame(SCSaveSys.instance.gameData.playerActorDataList, dataList);
                Destroy(gameObject);
            }
        }

    }
}
