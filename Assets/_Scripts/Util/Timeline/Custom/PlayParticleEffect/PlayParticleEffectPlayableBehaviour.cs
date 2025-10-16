using GameCore.Util;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


namespace GameCore.Util
{
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
        public float simulateSpeed;

        private GameObject _m_spawnedEffect;
        private bool _m_isPlaying;
        private Playable _m_currentPlayable;//当前播放的playable


        // Called when the state of the playable is set to Play
        public override void OnBehaviourPlay(Playable _playable, FrameData _info)
        {
            if (_m_isPlaying) return;

            _m_currentPlayable = _playable;
            _m_isPlaying = true;

            SpawnEffect();
        }

        public override void OnBehaviourPause(Playable _playable, FrameData _info)
        {
            if (!_m_isPlaying) return;

            _m_isPlaying = false;

            if (!Application.isPlaying && _m_spawnedEffect != null)
            {
                GameObject.DestroyImmediate(_m_spawnedEffect);
            }
        }

        public override void ProcessFrame(Playable _playable, FrameData _info, object _playerData)
        {
            // 确保在编辑模式下也能正确处理帧
            updateEffectState(_playable);
        }

        private void SpawnEffect()
        {
            // 计算生成位置
            Vector3 spawnPosition = calculateSpawnPosition();
            Quaternion spawnRotation = calculateSpawnRotation();


            if (Application.isPlaying && !string.IsNullOrEmpty(effectName))
            {
                ParticleSystem particleSystem = ParticleMgr.instance.PlayEffect(
                    effectName,
                    spawnPosition,
                    spawnRotation,
                    targetTransform,
                    autoDestroy,
                    destroyDelay,
                    scale
                );
                _m_spawnedEffect = particleSystem.gameObject;
                var mainModule = particleSystem.GetComponent<ParticleSystem>().main;
                mainModule.simulationSpeed = simulateSpeed;

                ParticleSystem[] systems = particleSystem.GetComponentsInChildren<ParticleSystem>();
                foreach (var sys in systems)
                {
                    var main = sys.main;
                    main.simulationSpeed = simulateSpeed;
                }

            }
            else
            {
                _m_spawnedEffect = GameObject.Instantiate(effectPrefab);
                _m_spawnedEffect.transform.SetParent(targetTransform);
                _m_spawnedEffect.transform.position = spawnPosition;
                _m_spawnedEffect.transform.rotation = spawnRotation;
                _m_spawnedEffect.transform.localScale = scale;
                ParticleSystem particleSystem = _m_spawnedEffect.GetComponent<ParticleSystem>();
                var mainModule = particleSystem.main;
                mainModule.simulationSpeed = simulateSpeed;

                ParticleSystem[] systems = particleSystem.GetComponentsInChildren<ParticleSystem>();
                foreach (var sys in systems)
                {
                    var main = sys.main;
                    main.simulationSpeed = simulateSpeed;
                }

                particleSystem.Play();
            }
        }

        private Vector3 calculateSpawnPosition()
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

        private Quaternion calculateSpawnRotation()
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

        private void updateEffectState(Playable playable)
        {
            if (_m_spawnedEffect != null)
            {
                float currentTime = (float)playable.GetTime();

                if (!Application.isPlaying)
                {
                    // 编辑模式下手动模拟粒子系统
                    _m_spawnedEffect.GetComponent<ParticleSystem>().Simulate(currentTime, true, false);
                }
                else
                {
                    // 运行模式下正常播放
                    if (!_m_spawnedEffect.GetComponent<ParticleSystem>().isPlaying)
                        _m_spawnedEffect.GetComponent<ParticleSystem>().Play();
                }
            }
        }

        public void destroyEffect()
        {
            if (_m_spawnedEffect != null)
            {
                if (Application.isPlaying)
                {
                    GameObject.Destroy(_m_spawnedEffect);
                }
                else
                {
                    GameObject.DestroyImmediate(_m_spawnedEffect);
                }
                _m_spawnedEffect = null;
            }
        }
    }
}