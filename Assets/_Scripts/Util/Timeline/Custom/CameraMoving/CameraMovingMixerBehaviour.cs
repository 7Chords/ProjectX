//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Playables;

//namespace GameCore.Util
//{
//    public class CameraMovingMixerBehaviour : PlayableBehaviour
//    {

//        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
//        {
//            // 1. 校验目标对象（playerData可获取轨道绑定的对象，更优雅的赋值方式）
//            targetObject = playerData as GameObject;
//            if (targetObject == null) return;

//            // 2. 关键：获取混合器的所有输入子Playable（对应轨道上的所有Clip）
//            int inputCount = playable.GetInputCount();
//            // 用于存储参与混合的Clip（权重>0）及其数据和权重
//            List<(CameraMovingPlayableBehaviour behaviour, float weight)> blendingClips = new List<(CameraMovingPlayableBehaviour, float)>();

//            // 3. 遍历所有子Playable，筛选有效（权重>0）的Clip
//            for (int i = 0; i < inputCount; i++)
//            {
//                // 获取第i个子Playable
//                Playable inputPlayable = playable.GetInput(i);
//                // 获取该子Playable的权重（0~1，>0表示参与当前帧的播放/混合）
//                float inputWeight = playable.GetInputWeight(i);

//                // 过滤权重为0的Clip（避免浮点误差，使用Mathf.Epsilon）
//                if (inputWeight < Mathf.Epsilon) continue;

//                // 4. 关键：获取子Playable绑定的Behaviour，提取Clip数据
//                CameraMovingPlayableBehaviour inputBehaviour = inputPlayable.<CameraMovingPlayableBehaviour>();
//                if (inputBehaviour != null)
//                {
//                    // 将有效Clip的数据、权重存入列表
//                    blendingClips.Add((inputBehaviour, inputWeight));
//                }
//            }

//            // 5. 处理混合逻辑（分两种情况：无混合（1个Clip）、混合（2个Clip））
//            if (blendingClips.Count == 1)
//            {
//                // 无混合：直接应用该Clip的数据（权重=1）
//                var singleClip = blendingClips[0];
//                targetObject.transform.position = singleClip.behaviour.targetPosition;
//            }
//            else if (blendingClips.Count == 2)
//            {
//                // 【核心需求】获取两个混合Clip的数据（前后Clip）
//                var previousClip = blendingClips[0];  // 前一个Clip（权重从1降为0）
//                var nextClip = blendingClips[1];      // 后一个Clip（权重从0升为1）

//                // 打印两个Clip的数据（验证获取结果，可根据需求使用数据）
//                Debug.Log($"当前混合的两个Clip：\n" +
//                          $"前一个Clip目标位置：{previousClip.behaviour.targetPosition}，权重：{previousClip.weight:F2}\n" +
//                          $"后一个Clip目标位置：{nextClip.behaviour.targetPosition}，权重：{nextClip.weight:F2}");

//                // 6. 基于两个Clip的数据实现平滑混合（加权插值）
//                // 归一化权重（确保两个权重之和为1，避免插值异常）
//                float totalWeight = previousClip.weight + nextClip.weight;
//                float normalizedPrevWeight = previousClip.weight / totalWeight;
//                float normalizedNextWeight = nextClip.weight / totalWeight;

//                // 加权插值计算最终属性（也可使用Vector3.Lerp，效果一致）
//                Vector3 finalPosition = previousClip.behaviour.targetPosition * normalizedPrevWeight +
//                                        nextClip.behaviour.targetPosition * normalizedNextWeight;

//                // 应用混合后的属性到目标对象
//                targetObject.transform.position = finalPosition;
//            }
//        }
//    }
//}
