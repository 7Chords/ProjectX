using Cinemachine;
using DG.Tweening;
using GameCore.Util;
using SCFrame;
using System;
using UnityEngine;
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
        }

        /// <summary>
        /// 设置虚拟相机与目标的偏移（真正改变了虚拟相机的pos）
        /// </summary>
        /// <param name="_tran"></param>
        /// <param name="_onStart"></param>
        /// <param name="_onFinish"></param>
        public void SetCameraPositionOffsetWithFollow(Transform _tran,Action _onStart = null,Action _onFinish = null)
        {
            if (_m_virtualCamera == null)
                return;
            if (_m_followTran == null)
                return;

            var transposer = _m_virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer == null)
                return;

            if(_tran.position == _m_followTran.position)
            {
                _onStart?.Invoke();
                _onFinish?.Invoke();
                return;
            }

            //计算目标偏移量
            Vector3 targetOffset = _tran.position - _m_followTran.position;

            //使用 DOTween平滑过渡
            Tween tween = DOTween.To(
                () => transposer.m_FollowOffset,
                x => transposer.m_FollowOffset = x,
                targetOffset,
                0.75f
            ).SetEase(Ease.OutQuart).OnStart(() =>
            {
                _onStart?.Invoke();
            }).OnComplete(() =>
            {
                _onFinish?.Invoke();
            });

            _m_tweenContainer.RegDoTween(tween);
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

    }
}
