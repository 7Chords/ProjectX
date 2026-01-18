using DG.Tweening;
using SCFrame;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace GameCore.TBS
{
    public class TBSGameStarter : Singleton<TBSGameStarter>
    {
        private List<Action> _m_onLoadOverActionList = new List<Action>();
        private SCStepCounter _m_stepCounter = new SCStepCounter();
        private TweenContainer _m_tweenContainer;
        public override void OnInitialize()
        {
            _m_onLoadOverActionList = new List<Action>();
            _m_stepCounter = new SCStepCounter();
            _m_tweenContainer = new TweenContainer();
        }

        public override void OnDiscard()
        {
            _m_onLoadOverActionList.Clear();
            _m_onLoadOverActionList = null;
            _m_stepCounter.ResetAll();
            _m_stepCounter = null;
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }
        public void StartGame()
        {
            Reset();
            _m_stepCounter.RegAllDoneDelegate(OnLoadOver);
            if (SCGame.instance.globalVolumn.TryGet<LensDistortion>(out LensDistortion comp))
            {
                Time.timeScale = 0;
                Tween tween = DOTween.To(
                     () => comp.intensity.value,
                     x => comp.intensity.value = x,
                     -0.75f,
                     0.1f
                 );
                tween.SetUpdate(true);
                _m_tweenContainer?.RegDoTween(tween);
            }
        }
        public void AddOneLoadStep()
        {
            _m_stepCounter.AddDoneStepCount();
        }
        public void ChangeLoadStepCount(int _count)
        {
            _m_stepCounter.ChgTotalStepCount(_count);
        }
        public void RegisterLoadOverCallback(Action _callBack)
        {
            if (_callBack != null)
                _m_onLoadOverActionList.Add(_callBack);
        }

        public void OnLoadOver()
        {
            Tween tween = DOVirtual.DelayedCall(1f, () =>
            {

                SCCommon.SetGameObjectEnable(SCGame.instance.playerGO, false);
                SCGame.instance.owCamera.gameObject.SetActive(false);
                SCGame.instance.virtualCamera.gameObject.SetActive(true);
                Cursor.visible = true;
                foreach (var callback in _m_onLoadOverActionList)
                {
                    callback?.Invoke();
                }
                if (SCGame.instance.globalVolumn.TryGet<LensDistortion>(out LensDistortion comp))
                {
                    Time.timeScale = 1;
                    comp.intensity.value = 0;
                }
            });
            _m_tweenContainer.RegDoTween(tween);
        }

        public void Reset()
        {
            if (SCGame.instance.globalVolumn.TryGet<LensDistortion>(out LensDistortion comp))
            {
                comp.intensity.value = 0f;
            }
            _m_onLoadOverActionList.Clear();
            _m_stepCounter.ResetAll();
        }

    }
}