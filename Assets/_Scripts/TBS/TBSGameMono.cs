using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameCore.TBS
{
    [Serializable]
    public class TBSPosInfo 
    {
        public ETBSPos posType;
        public Transform posTran;
    }

    public class TBSGameMono : MonoBehaviour
    {
        public List<TBSPosInfo> playerPosInfoList;
        public List<TBSPosInfo> enemyPosInfoList;

        public Transform playerLookEnemyCenterPos;
        public Transform enemyLookPlayerCenterPos;

        //public Transform cameraLook


        public static string assetGroupName = "Stage";
        public static string assetObjName = "TBSStage";

    }
}
