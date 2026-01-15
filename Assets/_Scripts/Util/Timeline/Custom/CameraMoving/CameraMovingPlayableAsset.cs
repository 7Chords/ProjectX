using GameCore.TBS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace GameCore.Util
{
    [System.Serializable]
    public class CameraMovingPlayableAsset : PlayableAsset, ITimelineClipAsset
    {
        [Header("相机运动信息")]
        public TBSSkillCameraMovingItem cameraMovingItem;

        public ClipCaps clipCaps => ClipCaps.Blending;

        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph _graph, GameObject _go)
        {
            var scriptPlayable = ScriptPlayable<CameraMovingPlayableBehaviour>.Create(_graph);
            var behaviour = scriptPlayable.GetBehaviour();

            // 传递参数到行为类
            behaviour.cameraMovingItem = cameraMovingItem;

            return scriptPlayable;
        }

    }


}
