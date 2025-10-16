using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

namespace GameCore.Util
{
    [TrackColor(1, 1, 0)]
    [TrackClipType(typeof(PlayParticleEffectPlayableAsset))]
    [TrackBindingType(typeof(Transform))]
    public class PlayParticleEffectTrack : TrackAsset
    {

    }

}