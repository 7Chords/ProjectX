using Cinemachine;
using DG.Tweening;
using GameCore.Util;
using SCFrame;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static Cinemachine.CinemachineBlendDefinition;

namespace GameCore
{
    public class GameCameraMgr : Singleton<GameCameraMgr>
    {
        private CinemachineVirtualCamera _m_virtualCamera;



        private Transform _m_followTran;
        private Transform _m_targetTran;
        private TweenContainer _m_tweenContainer;
        public override void OnInitialize()
        {
            _m_virtualCamera = SCGame.instance.virtualCamera;
            _m_tweenContainer = new TweenContainer();
        }
        public override void OnDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;

            SCTaskHelper.instance.KillAllCoroutines(this);
        }

        /// <summary>
        /// 设置虚拟相机与目标的偏移（真正改变了虚拟相机的pos）
        /// </summary>
        /// <param name="_offset"></param>
        /// <param name="_onStart"></param>
        /// <param name="_onFinish"></param>
        public void SetCameraPositionOffsetWithFollow(Vector3 _offset,bool _isPlayer,float _duration = 0.75f, Action _onStart = null,Action _onFinish = null)
        {
            if (_m_virtualCamera == null)
                return;
            if (_m_followTran == null)
                return;

            var transposer = _m_virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer == null)
                return;

            if(_offset == _m_followTran.position)
            {
                _onStart?.Invoke();
                _onFinish?.Invoke();
                return;
            }

            Vector3 targetOffset = Vector3.zero;
            //计算目标偏移量
            if (_isPlayer)
                targetOffset = _offset - _m_followTran.position;
            else
            {
                targetOffset = _m_followTran.position - _offset;
                targetOffset += new Vector3(0, -targetOffset.y * 2, 0);
            }

            if(_duration > 0f)
            {
                //使用 DOTween平滑过渡
                Tween tween = DOTween.To(
                    () => transposer.m_FollowOffset,
                    x => transposer.m_FollowOffset = x,
                    targetOffset,
                    _duration
                ).SetEase(Ease.OutQuart).OnStart(() =>
                {
                    _onStart?.Invoke();
                }).OnComplete(() =>
                {
                    _onFinish?.Invoke();
                });

                _m_tweenContainer.RegDoTween(tween);
            }
            else
            {
                transposer.m_FollowOffset = targetOffset;
                _onStart?.Invoke();
                _onFinish?.Invoke();
            }
        }

        /// <summary>
        /// 设置虚拟相机的跟随对象
        /// </summary>
        /// <param name="_follow"></param>
        /// <param name="_onCameraFollowStart"></param>
        /// <param name="_onCameraFollowFinish"></param>
        public void SetCameraFollow(Transform _follow, Action _onCameraFollowStart = null, Action _onCameraFollowFinish = null)
        {
            if (_m_virtualCamera == null)
                return;
            _m_followTran = _follow;
            _m_virtualCamera.Follow = _m_followTran;

            _onCameraFollowStart?.Invoke();

            Tween tween = DOVirtual.DelayedCall(0.5f,
                () =>
                {
                    _onCameraFollowFinish?.Invoke();
                });

            _m_tweenContainer?.RegDoTween(tween);


        }

        /// <summary>
        /// 设置虚拟相机的目标
        /// </summary>
        /// <param name="_target"></param>
        public void SetCameraTarget(Transform _target)
        {
            if (_m_virtualCamera == null)
                return;
            _m_targetTran = _target;
            _m_virtualCamera.LookAt = _m_targetTran;

        }

        public void SetCameraTransitionType(Style _style)
        {
            SCGame.instance.cinemachineBrain.m_DefaultBlend.m_Style = _style;
        }

        /// <summary>
        /// 相机震动
        /// </summary>
        /// <param name="_shakeDuration"></param>
        /// <param name="_shakeStrength"></param>
        public void ShakeCamera(float _shakeDuration,float _shakeStrength)
        {
            SCGame.instance.cinemachineImpulseSource.m_ImpulseDefinition.m_ImpulseDuration = _shakeDuration;
            SCGame.instance.cinemachineImpulseSource.GenerateImpulse(_shakeStrength);
            //Tween shakeTween = SCGame.instance.virtualCamera.transform.DOShakePosition(_shakeDuration, _shakeStrength);
            //_m_tweenContainer?.RegDoTween(shakeTween);
        }

        /// <summary>
        /// 冻结相机 顿帧
        /// </summary>
        /// <param name="_freezeDuration"></param>
        public void FreezeCamera(float _freezeDuration)
        {
            SCTaskHelper.instance.CreateCoroutine(this, freezeGameTime(_freezeDuration));
        }

        private IEnumerator freezeGameTime(float pauseDuration)
        {
            float pauseTime = pauseDuration;
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(pauseTime);
            Time.timeScale = 1;
        }


        //todo: 临时方案，后续再优化
        public void SwitchToVirtualCamera(CinemachineVirtualCamera _anotherVC, UnityAction<CinemachineBrain> _switchOverCallback = null)
        {
            _m_virtualCamera.gameObject.SetActive(false);
            _m_virtualCamera = _anotherVC;
            _m_virtualCamera.gameObject.SetActive(true);
            if (_switchOverCallback != null)
                SCGame.instance.cinemachineBrain.m_CameraCutEvent.AddListener(_switchOverCallback);
        }
        public void SwitchToMainVirtualCamera(UnityAction<CinemachineBrain> _switchOverCallback = null)
        {
            _m_virtualCamera.gameObject.SetActive(false);
            _m_virtualCamera = SCGame.instance.virtualCamera;
            _m_virtualCamera.gameObject.SetActive(true);
            if (_switchOverCallback != null)
                SCGame.instance.cinemachineBrain.m_CameraCutEvent.AddListener(_switchOverCallback);
        }
    }
}
