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
        public float tbsOpenSkillAndItemCameraOffsetY;

        //一些actor攻击的tween时间参数
        public float tbsMeleeLookAtTargetDuration;
        public float tbsMeleeMoveToTargetDuration;
        public float tbsMeleeRotateDuration;
        public float tbsMeleeMoveToOriginalDuration;

        protected override void _parseFromString()
        {
            generalCompList = getList<ETBSCompType>("generalCompList");
            tbsTargetHighLightChgTime = getFloat("tbsTargetHighLightChgTime");
            tbsActorSingleRotateTime = getFloat("tbsActorSingleRotateTime");
            tbsInputFrameInterval = getInt("tbsInputFrameInterval");
            tbsGetHitCamShakeDuration = getFloat("tbsGetHitCamShakeDuration");
            tbsGetHitCamShakeStrength = getFloat("tbsGetHitCamShakeStrength");
            tbsGetHitCamFreezeDuration = getFloat("tbsGetHitCamFreezeDuration");
            tbsOpenSkillAndItemCameraOffsetY = getFloat("tbsOpenSkillAndItemCameraOffsetY");
            tbsMeleeLookAtTargetDuration = getFloat("tbsMeleeLookAtTargetDuration");
            tbsMeleeMoveToTargetDuration = getFloat("tbsMeleeMoveToTargetDuration");
            tbsMeleeRotateDuration = getFloat("tbsMeleeRotateDuration");
            tbsMeleeMoveToOriginalDuration = getFloat("tbsMeleeMoveToOriginalDuration");


        }
        public static string assetPath => "RefData/ExportTxt";

        public static string sheetName => "game_general";
    }
}
