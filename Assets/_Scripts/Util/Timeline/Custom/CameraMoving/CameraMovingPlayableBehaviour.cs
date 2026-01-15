using GameCore.TBS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace GameCore.Util
{
    // A behaviour that is attached to a playable
    public class CameraMovingPlayableBehaviour : PlayableBehaviour
    {

        public TBSSkillCameraMovingItem cameraMovingItem;


        // Called when the owning graph starts playing
        public override void OnGraphStart(Playable playable)
        {

        }

        // Called when the owning graph stops playing
        public override void OnGraphStop(Playable playable)
        {

        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {

            GameCameraMgr.instance.SetCameraFollow(cameraMovingItem.follow);
            GameCameraMgr.instance.SetCameraTarget(cameraMovingItem.lookAt);
            GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(cameraMovingItem.offset, true,cameraMovingItem.offsetTranslateDuration);
        }

        // Called when the state of the playable is set to Paused
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {

        }

        // Called each frame while the state is set to Play
        public override void PrepareFrame(Playable playable, FrameData info)
        {

        }

    }

}