using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace GameCore.Util
{
    [System.Serializable]
    public class PlaySfxPlayableAsset : PlayableAsset
    {
        [Header("音效名称")]
        public string sfxName;
        // Factory method that generates a playable based on this asset
        public override Playable CreatePlayable(PlayableGraph _graph, GameObject _go)
        {
            var scriptPlayable = ScriptPlayable<PlaySfxPlayableBehaviour>.Create(_graph);
            var behaviour = scriptPlayable.GetBehaviour();

            // 传递参数到行为类
            behaviour.sfxName = sfxName;

            return scriptPlayable;
        }
    }
}
