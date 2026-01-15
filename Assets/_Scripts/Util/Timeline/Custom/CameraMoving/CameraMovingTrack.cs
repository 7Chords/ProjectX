using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace GameCore.Util
{
    [TrackColor(1, 0, 0)]
    [TrackClipType(typeof(CameraMovingPlayableAsset))]
    [TrackBindingType(typeof(GameObject))]
    public class CameraMovingTrack : TrackAsset
    {
    }
}
