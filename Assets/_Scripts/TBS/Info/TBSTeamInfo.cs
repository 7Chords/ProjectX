using GameCore.RefData;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSTeamInfo
    {
        public string teamName;
        public List<TBSActorInfo> actorInfoList;

        public void Init(string _teamName,List<ActorData> _teamDataList,bool _isEnemy)
        {
            if (_teamDataList == null)
                return;
            teamName = _teamName;
            actorInfoList = new List<TBSActorInfo>();
            for(int i =0;i< _teamDataList.Count;i++)
            {
                TBSActorInfo actorInfo = new TBSActorInfo();
                actorInfo.Init(_teamDataList[i], _isEnemy);
                actorInfoList.Add(actorInfo);
            }
        }
        public void Init(string _teamName, List<TBSActorInfo> _teamInfoList, bool _isEnemy)
        {
            if (_teamInfoList == null)
                return;
            teamName = _teamName;
            actorInfoList = _teamInfoList;
        }
    }
}
