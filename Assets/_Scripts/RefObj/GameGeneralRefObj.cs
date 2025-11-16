using SCFrame;
using UnityEngine;
using System.Collections.Generic;
using GameCore.TBS;

namespace GameCore.RefData
{
    public class GameGeneralRefObj : SCRefDataCore
    {
        public GameGeneralRefObj(string _assetPath, string _objName) : base(_assetPath, _objName)
        {
        }
        public List<ETBSCompType> generalCompList;
        public float tbsTargetHighLightChgTime;
        public float tbsActorSingleRotateTime;
        public int tbsInputFrameInterval;
        public float tbsGetHitCamShakeDuration;
        public float tbsGetHitCamShakeStrength;
        public float tbsGetHitCamFreezeDuration;
        protected override void _parseFromString()
        {
            generalCompList = getList<ETBSCompType>("generalCompList");
            tbsTargetHighLightChgTime = getFloat("tbsTargetHighLightChgTime");
            tbsActorSingleRotateTime = getFloat("tbsActorSingleRotateTime");
            tbsInputFrameInterval = getInt("tbsInputFrameInterval");
            tbsGetHitCamShakeDuration = getFloat("tbsGetHitCamShakeDuration");
            tbsGetHitCamShakeStrength = getFloat("tbsGetHitCamShakeStrength");
            tbsGetHitCamFreezeDuration = getFloat("tbsGetHitCamFreezeDuration");

        }
        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "game_general";
    }
}
