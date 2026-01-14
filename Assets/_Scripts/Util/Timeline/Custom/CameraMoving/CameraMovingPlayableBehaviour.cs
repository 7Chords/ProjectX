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

        private Vector3 _m_previousOffset;

        // Called when the owning graph starts playing
        public override void OnGraphStart(Playable playable)
        {
            _m_previousOffset = cameraMovingItem.offset;

            GameCameraMgr.instance.SetCameraFollow(cameraMovingItem.follow);
            GameCameraMgr.instance.SetCameraTarget(cameraMovingItem.target);
            GameCameraMgr.instance.SetCameraTransitionType(cameraMovingItem.blendStyle);
            GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(cameraMovingItem.offset,true);
        }

        // Called when the owning graph stops playing
        public override void OnGraphStop(Playable playable)
        {

        }

        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {

        }

        // Called when the state of the playable is set to Paused
        public override void OnBehaviourPause(Playable playable, FrameData info)
        {

        }

        // Called each frame while the state is set to Play
        public override void PrepareFrame(Playable playable, FrameData info)
        {

        }
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            //需要支持过渡效果的只有offset


            //当前所占的权重 
            float blendWeight = info.weight;

            //int inputCount = playable.GetInputCount();

            //Debug.Log(blendWeight);
            //Vector3 currentPosition = Vector3.Lerp(
            //    _previousPosition,  // 起始值（上一个Clip的目标位置，或当前对象位置）
            //    targetPosition,     // 目标值（当前Clip的目标位置）
            //    blendWeight         // 混合权重（平滑过渡的插值因子）
            //);

            // 4. 应用插值后的属性到目标对象
            //targetObject.transform.position = currentPosition;


            // 5. 更新上一个位置（用于下一次帧更新的插值基准，可选，根据需求调整）
            if (blendWeight >= 1f - Mathf.Epsilon)
            {

            }

        }
    }

}