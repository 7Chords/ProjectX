using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSTeamInfo
    {
        //public ETBSTeamType teamType;
        public string teamName;
        public List<TBSActorInfo> actorInfoList;

        public void InitNewInfo()
        {
            teamName = "PlayerTeam";
            actorInfoList = new List<TBSActorInfo>();
            TBSActorInfo playerActorInfo = new TBSActorInfo();
            playerActorInfo.InitNewInfo();
            actorInfoList.Add(playerActorInfo);
        }

        public void InitTempInfo()
        {
            teamName = "PlayerTeam";
            actorInfoList = new List<TBSActorInfo>();
            TBSActorInfo playerActorInfo = new TBSActorInfo();
            playerActorInfo.InitNewInfo();
            actorInfoList.Add(playerActorInfo);
        }
    }
}
