using SCFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.Util
{
    [Serializable]
    public class ParticleInfo
    {
        public string effectName;
        public ParticleSystem particleSystem;
        public GameObject rootGameObject;

        public ParticleInfo(string _name, ParticleSystem _system, GameObject _root)
        {
            effectName = _name;
            particleSystem = _system;
            rootGameObject = _root;
        }
    }

    /// <summary>
    /// 粒子特效管理器
    /// </summary>
    public class ParticleMgr : Singleton<ParticleMgr>
    {
        private List<ParticleInfo> _m_activeParticleEffectList;

        private GameObject _m_particleRootGO;

        //private Dictionary<string, GameObject> _m_particlePrefabCache;


        public override void OnInitialize()
        {
            _m_particleRootGO = new GameObject("PARTICLE_ROOT");
            //_m_particlePrefabCache = new Dictionary<string, GameObject>();
            _m_activeParticleEffectList = new List<ParticleInfo>();
        }

        public override void OnDiscard()
        {
            // 清理所有协程和资源
            ClearAllEffects();
        }

        #region 主要播放方法

        /// <summary>
        /// 播放粒子特效
        /// </summary>
        public ParticleSystem PlayEffect(string _effectName,
            Vector3 _position,
            Quaternion _rotation = default,
            Transform _parent = null,
            bool _autoDestroy = true,
            float _destroyDelay = -1f,
            Vector3? _scale = null)
        {
            //加载粒子预制体
            GameObject prefab = GetParticlePrefab(_effectName);
            if (prefab == null)
            {
                Debug.LogError($"粒子特效预制体加载失败：{_effectName}");
                return null;
            }

            //粒子实例位置等信息设置
            Transform parentTransform = _parent != null ? _parent : _m_particleRootGO.transform;
            prefab.transform.SetParent(parentTransform);
            prefab.transform.position = _position;
            prefab.transform.rotation = _rotation;
            if (_scale.HasValue)
            {
                prefab.transform.localScale = _scale.Value;
            }

            ParticleSystem particleSystem = prefab.GetComponent<ParticleSystem>();
            if (particleSystem == null)
            {
                particleSystem = prefab.GetComponentInChildren<ParticleSystem>();
                if (particleSystem == null)
                {
                    Debug.LogWarning($"粒子预制体上没有ParticleSystem组件，特效名：{_effectName}");
                    SCCommon.DestoryGameObject(prefab);
                    return null;
                }
            }

            //创建粒子信息
            ParticleInfo info = new ParticleInfo(_effectName, particleSystem, prefab);
            // 播放粒子
            particleSystem.Play();

            // 自动销毁处理
            if (_autoDestroy)
            {
                float delay = _destroyDelay >= 0 ? _destroyDelay : GetParticleDuration(particleSystem);
                SCTaskHelper.instance.CreateCoroutine(prefab, AutoDestroyEffect(info, delay));
            }

            _m_activeParticleEffectList.Add(info);
            return particleSystem;
        }

        #endregion

        #region 简化重载方法

        //仅需特效名称和位置
        public ParticleSystem PlayEffect(string _effectName, Vector3 _position)
        {
            return PlayEffect(_effectName, _position, default, null, true, -1f, null);
        }

        //指定位置和旋转
        public ParticleSystem PlayEffect(string _effectName, Vector3 _position, Quaternion _rotation)
        {
            return PlayEffect(_effectName, _position, _rotation, null, true, -1f, null);
        }

        //指定父物体
        public ParticleSystem PlayEffect(string _effectName, Vector3 _position, Transform _parent)
        {
            return PlayEffect(_effectName, _position, default, _parent, true, -1f, null);
        }

        //完全自定义参数
        public ParticleSystem PlayEffect(string _effectName,
            Vector3 _position,
            Quaternion _rotation,
            Transform _parent,
            bool _autoDestroy,
            float _destroyDelay)
        {
            return PlayEffect(_effectName, _position, _rotation, _parent, _autoDestroy, _destroyDelay, null);
        }

        //自定义缩放
        public ParticleSystem PlayEffect(string _effectName, Vector3 _position, Vector3 _scale)
        {
            return PlayEffect(_effectName, _position, default, null, true, -1f, _scale);
        }

        //跟随父物体（位置和旋转随父物体）
        public ParticleSystem PlayEffectOnTransform(string _effectName, Transform _parent, Vector3? _localPosition = null)
        {
            Vector3 position = _localPosition.HasValue ? _parent.TransformPoint(_localPosition.Value) : _parent.position;
            return PlayEffect(_effectName, position, _parent.rotation, _parent, true, -1f, null);
        }

        //世界坐标播放，不自动销毁（用于持续效果）
        public ParticleSystem PlayEffectPersistent(string _effectName, Vector3 _position)
        {
            return PlayEffect(_effectName, _position, default, null, false, -1f, null);
        }

        #endregion

        #region 特效控制

        /// <summary>
        /// 停止指定名称的粒子特效
        /// </summary>
        public void StopEffect(string _effectName, bool _immediate = false)
        {
            List<ParticleInfo> effectsToRemove = new List<ParticleInfo>();

            for (int i = _m_activeParticleEffectList.Count - 1; i >= 0; i--)
            {
                ParticleInfo info = _m_activeParticleEffectList[i];
                if (info.effectName == _effectName)
                {
                    if (_immediate)
                    {
                        DestroyParticleEffect(info);
                        effectsToRemove.Add(info);
                    }
                    else
                    {
                        StopParticleEffect(info);
                    }
                }
            }

            // 移除已销毁的特效
            foreach (var effect in effectsToRemove)
            {
                _m_activeParticleEffectList.Remove(effect);
            }
        }

        /// <summary>
        /// 停止所有指定名称的粒子特效
        /// </summary>
        public void StopAllEffectsByName(string _effectName, bool _immediate = false)
        {
            StopEffect(_effectName, _immediate);
        }

        /// <summary>
        /// 暂停粒子特效
        /// </summary>
        public void PauseEffect(string _effectName)
        {
            foreach (var info in _m_activeParticleEffectList)
            {
                if (info.effectName == _effectName && info.particleSystem != null)
                {
                    info.particleSystem.Pause();
                }
            }
        }

        /// <summary>
        /// 暂停所有粒子特效
        /// </summary>
        public void PauseAllEffects()
        {
            foreach (var info in _m_activeParticleEffectList)
            {
                if (info.particleSystem != null)
                {
                    info.particleSystem.Pause();
                }
            }
        }

        /// <summary>
        /// 恢复播放粒子特效
        /// </summary>
        public void ResumeEffect(string _effectName)
        {
            foreach (var info in _m_activeParticleEffectList)
            {
                if (info.effectName == _effectName && info.particleSystem != null)
                {
                    info.particleSystem.Play();
                }
            }
        }

        /// <summary>
        /// 恢复所有粒子特效
        /// </summary>
        public void ResumeAllEffects()
        {
            foreach (var info in _m_activeParticleEffectList)
            {
                if (info.particleSystem != null)
                {
                    info.particleSystem.Play();
                }
            }
        }

        /// <summary>
        /// 停止所有粒子特效
        /// </summary>
        public void StopAllEffects(bool _immediate = false)
        {
            for (int i = _m_activeParticleEffectList.Count - 1; i >= 0; i--)
            {
                ParticleInfo info = _m_activeParticleEffectList[i];
                if (_immediate)
                {
                    DestroyParticleEffect(info);
                }
                else
                {
                    StopParticleEffect(info);
                }
            }

            if (_immediate)
            {
                _m_activeParticleEffectList.Clear();
            }
        }

        /// <summary>
        /// 清理所有粒子特效
        /// </summary>
        public void ClearAllEffects()
        {
            StopAllEffects(true);
        }

        /// <summary>
        /// 清理所有已完成的粒子特效
        /// </summary>
        public void CleanupFinishedEffects()
        {
            for (int i = _m_activeParticleEffectList.Count - 1; i >= 0; i--)
            {
                ParticleInfo info = _m_activeParticleEffectList[i];
                if (info.particleSystem == null || !info.particleSystem.IsAlive())
                {
                    DestroyParticleEffect(info);
                    _m_activeParticleEffectList.RemoveAt(i);
                }
            }
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取粒子预制体（带缓存）
        /// </summary>
        private GameObject GetParticlePrefab(string _effectName)
        {
            GameObject prefab = ResourcesHelper.LoadGameObject(_effectName,null,true);

            if (prefab == null)
                Debug.LogWarning($"粒子资源加载失败，特效名：{_effectName}");

            return prefab;
        }

        /// <summary>
        /// 获取粒子系统持续时间
        /// </summary>
        private float GetParticleDuration(ParticleSystem _ps)
        {
            if (_ps == null) return 0f;

            ParticleSystem.MainModule main = _ps.main;
            return main.duration + main.startLifetime.constantMax;
        }

        /// <summary>
        /// 停止粒子效果（优雅停止）
        /// </summary>
        private void StopParticleEffect(ParticleInfo _info)
        {
            if (_info.particleSystem != null)
            {
                _info.particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                SCTaskHelper.instance.KillAllCoroutines(_info.rootGameObject);
                SCTaskHelper.instance.CreateCoroutine(_info.rootGameObject, AutoDestroyEffect(_info, GetParticleDuration(_info.particleSystem)));
            }
        }

        /// <summary>
        /// 立即销毁粒子效果
        /// </summary>
        private void DestroyParticleEffect(ParticleInfo _info)
        {

            SCTaskHelper.instance.KillAllCoroutines(_info.rootGameObject);
            SCCommon.DestoryGameObject(_info.rootGameObject);
        }

        /// <summary>
        /// 自动销毁粒子特效
        /// </summary>
        private IEnumerator AutoDestroyEffect(ParticleInfo _info, float _delay)
        {
            yield return new WaitForSeconds(_delay);

            // 等待粒子完全消失
            if (_info.particleSystem != null)
            {
                yield return new WaitWhile(() => _info.particleSystem.IsAlive());
            }

            // 从列表中移除并销毁对象
            if (_m_activeParticleEffectList.Contains(_info))
            {
                _m_activeParticleEffectList.Remove(_info);
            }

            if (_info.rootGameObject != null)
            {
                SCCommon.DestoryGameObject(_info.rootGameObject);
            }
        }

        /// <summary>
        /// 检查特效是否正在播放
        /// </summary>
        public bool IsEffectPlaying(string _effectName)
        {
            foreach (var info in _m_activeParticleEffectList)
            {
                if (info.effectName == _effectName && info.particleSystem != null && info.particleSystem.IsAlive())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取指定名称的所有活跃粒子特效
        /// </summary>
        public List<ParticleSystem> GetEffectsByName(string _effectName)
        {
            List<ParticleSystem> results = new List<ParticleSystem>();
            foreach (var info in _m_activeParticleEffectList)
            {
                if (info.effectName == _effectName && info.particleSystem != null)
                {
                    results.Add(info.particleSystem);
                }
            }
            return results;
        }

        /// <summary>
        /// 预加载粒子资源（可选，用于提前加载常用特效）
        /// </summary>
        public void PreloadEffect(string _effectName)
        {
            GetParticlePrefab(_effectName);
        }

        /// <summary>
        /// 清理资源缓存（在内存紧张时调用）
        /// </summary>
        public void ClearCache()
        {
            Resources.UnloadUnusedAssets();
        }

        #endregion
    }
}

