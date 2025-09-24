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

        public void SetCameraPositionOffsetWithFollow(Transform _tran)
        {
            if (_m_virtualCamera == null)
                return;
            if (_m_followTran == null)
                return;
            _m_virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset =
                _tran.position - _m_followTran.position;
        }
        public void SetCameraFollow(Transform _follow, Action _onCameraFollowStart = null, Action _onCameraFollowFinish = null)
        {
            if (_m_virtualCamera == null)
                return;
            _m_followTran = _follow;
            _m_virtualCamera.Follow = _m_followTran;

            if (SCGame.instance.cinemachineBrain.m_DefaultBlend.m_Style == Style.Cut)
            {
                _onCameraFollowStart?.Invoke();
                _onCameraFollowFinish?.Invoke();
            }
            else
            {
                Tween tween = DOTween.Sequence().AppendInterval(SCGame.instance.cinemachineBrain.m_DefaultBlend.BlendTime).
                    OnComplete(() =>
                    {
                        _onCameraFollowFinish?.Invoke();
                    }).OnStart(() =>
                    {
                        _onCameraFollowStart?.Invoke();
                    });

                _m_tweenContainer?.RegDoTween(tween);
            }


        }

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


        public override void OnDiscard()
        {
            _m_tweenContainer.KillAllDoTween();
        }
    }
}
