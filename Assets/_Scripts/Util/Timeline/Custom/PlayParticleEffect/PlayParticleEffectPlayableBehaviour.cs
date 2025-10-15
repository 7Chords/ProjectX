using GameCore.Util;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Timeline播放特效节点行为类
/// </summary>
public class PlayParticleEffectPlayableBehaviour : PlayableBehaviour
{
    public string effectName;
    public GameObject effectPrefab;
    public Transform targetTransform;
    public Vector3 positionOffset;
    public bool useLocalOffset;
    public Vector3 rotation;
    public bool relativeToTarget;
    public Vector3 scale = Vector3.one;
    public bool autoDestroy;
    public float destroyDelay;
    public bool followTarget;
    public GameObject directorGameObject;

    private GameObject spawnedEffect;
    private bool isPlaying;
    private Playable currentPlayable;


    // Called when the state of the playable is set to Play
    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (isPlaying) return;

        currentPlayable = playable;
        isPlaying = true;

        if (!string.IsNullOrEmpty(effectName))
        {
            SpawnEffect();
        }
    }

    // Called when the state of the playable is set to Paused
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (!isPlaying) return;

        isPlaying = false;

        // 如果设置了自动销毁且特效还在，则销毁特效
        if (autoDestroy && spawnedEffect != null)
        {
            if (destroyDelay > 0)
            {
                // 延迟销毁
                if (Application.isPlaying)
                {
                    GameObject.Destroy(spawnedEffect, destroyDelay);
                }
                else
                {
                    GameObject.DestroyImmediate(spawnedEffect);
                }
            }
            else
            {
                if (Application.isPlaying)
                {
                    GameObject.Destroy(spawnedEffect);
                }
                else
                {
                    GameObject.DestroyImmediate(spawnedEffect);
                }
            }
            spawnedEffect = null;
        }
    }

    // Called each frame while the state is set to Play
    public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (spawnedEffect != null && followTarget && targetTransform != null)
        {
            UpdateEffectPosition();
        }
    }

    private void SpawnEffect()
    {
        // 计算生成位置
        Vector3 spawnPosition = CalculateSpawnPosition();
        Quaternion spawnRotation = CalculateSpawnRotation();


        if(Application.isPlaying)
        {
            spawnedEffect = ParticleMgr.instance.PlayEffect(
                effectName,
                spawnPosition,
                spawnRotation,
                targetTransform,
                autoDestroy,
                destroyDelay,
                scale
            ).gameObject;
        }
        else
        {
            spawnedEffect = GameObject.Instantiate(effectPrefab);
            spawnedEffect.transform.SetParent(targetTransform);
            spawnedEffect.transform.position = spawnPosition;
            spawnedEffect.transform.rotation = spawnRotation;
            spawnedEffect.transform.localScale = scale;

        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        Vector3 basePosition;

        if (targetTransform != null)
        {
            basePosition = targetTransform.position;
            if (useLocalOffset)
            {
                basePosition += targetTransform.TransformDirection(positionOffset);
            }
            else
            {
                basePosition += positionOffset;
            }
        }
        else
        {
            // 如果没有指定目标，使用导演对象的位置
            basePosition = directorGameObject != null ?
                directorGameObject.transform.position : Vector3.zero;
            basePosition += positionOffset;
        }

        return basePosition;
    }

    private Quaternion CalculateSpawnRotation()
    {
        Quaternion baseRotation = Quaternion.identity;

        if (targetTransform != null && relativeToTarget)
        {
            baseRotation = targetTransform.rotation * Quaternion.Euler(rotation);
        }
        else
        {
            baseRotation = Quaternion.Euler(rotation);
        }

        return baseRotation;
    }

    private void UpdateEffectPosition()
    {
        if (spawnedEffect == null || targetTransform == null) return;

        Vector3 newPosition = CalculateSpawnPosition();
        Quaternion newRotation = CalculateSpawnRotation();

        spawnedEffect.transform.position = newPosition;
        spawnedEffect.transform.rotation = newRotation;
    }


    // 手动销毁特效（在需要提前销毁时使用）
    public void DestroyEffect()
    {
        if (spawnedEffect != null)
        {
            if (Application.isPlaying)
            {
                GameObject.Destroy(spawnedEffect);
            }
            else
            {
                GameObject.DestroyImmediate(spawnedEffect);
            }
            spawnedEffect = null;
        }
    }
}