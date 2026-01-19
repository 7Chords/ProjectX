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

        public void Init(string _teamName,List<ActorData> _playerTeamDataList,bool _isEnemy)
        {
            if (_playerTeamDataList == null)
                return;
            teamName = _teamName;
            actorInfoList = new List<TBSActorInfo>();
            for(int i =0;i< _playerTeamDataList.Count;i++)
            {
                TBSActorInfo actorInfo = new TBSActorInfo();
                actorInfo.Init(_playerTeamDataList[i], _isEnemy);
                actorInfoList.Add(actorInfo);
            }
        }
    }
}
