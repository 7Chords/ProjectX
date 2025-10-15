using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Timeline播放特效节点
/// </summary>
[System.Serializable]
public class PlayParticleEffectPlayableAsset : PlayableAsset
{
    [Header("特效设置")]
    [Tooltip("特效资源名称")]
    public string effectName;
    [Tooltip("特效资源(在编辑器模式下预览使用)")]
    public GameObject effectPrefab;

    [Header("位置设置")]
    [Tooltip("参考目标（为空则使用默认位置）")]
    public ExposedReference<Transform> targetTransform;

    [Tooltip("位置偏移")]
    public Vector3 positionOffset = Vector3.zero;

    [Tooltip("使用局部坐标偏移还是世界坐标偏移")]
    public bool useLocalOffset = true;

    [Header("旋转设置")]
    [Tooltip("旋转角度")]
    public Vector3 rotation = Vector3.zero;

    [Tooltip("是否相对于目标旋转")]
    public bool relativeToTarget = false;

    [Header("缩放设置")]
    [Tooltip("缩放比例")]
    public Vector3 scale = Vector3.one;

    [Header("生命周期设置")]
    [Tooltip("自动销毁")]
    public bool autoDestroy = true;

    [Tooltip("销毁延迟时间")]
    public float destroyDelay = 0f;

    [Tooltip("跟随目标移动")]
    public bool followTarget = false;


    // Factory method that generates a playable based on this asset
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
    {
        var scriptPlayable = ScriptPlayable<PlayParticleEffectPlayableBehaviour>.Create(graph);
        var behaviour = scriptPlayable.GetBehaviour();

        // 传递参数到行为类
        behaviour.effectName = effectName;
        behaviour.effectPrefab = effectPrefab;
        behaviour.targetTransform = targetTransform.Resolve(graph.GetResolver());
        behaviour.positionOffset = positionOffset;
        behaviour.useLocalOffset = useLocalOffset;
        behaviour.rotation = rotation;
        behaviour.relativeToTarget = relativeToTarget;
        behaviour.scale = scale;
        behaviour.autoDestroy = autoDestroy;
        behaviour.destroyDelay = destroyDelay;
        behaviour.followTarget = followTarget;
        behaviour.directorGameObject = go;

        return scriptPlayable;
    }
}