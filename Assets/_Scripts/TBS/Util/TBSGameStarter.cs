using DG.Tweening;
using GameCore.OW;
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

        private TBSBattleInfo _m_battleInfo;

        private List<ActorData> _m_playerTeamDataList;
        private List<ActorData> _m_enemyTeamDataList;

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

        public void LoadTBSGame(List<ActorData> _playerTeamDataList, List<ActorData> _enemyTeamDataList)
        {
            if (_playerTeamDataList == null || _enemyTeamDataList == null)
                return;
            _m_playerTeamDataList = _playerTeamDataList;
            _m_enemyTeamDataList = _enemyTeamDataList;
            _m_battleInfo = new TBSBattleInfo();
            _m_battleInfo.Init(_m_playerTeamDataList, _m_enemyTeamDataList);
            SCModel.instance.tbsModel.Init(_m_battleInfo);
            reset();
            _m_stepCounter.RegAllDoneDelegate(onLoadOver);
            change2TBSGame();
            SCMsgCenter.SendMsg(SCMsgConst.TBS_GAME_START);
        }

        public void ReloadTBSGame()
        {
            if (_m_battleInfo == null)
                return;
            SCMsgCenter.SendMsg(SCMsgConst.TBS_GAME_FINISH);
            _m_battleInfo = new TBSBattleInfo();
            _m_battleInfo.Init(_m_playerTeamDataList, _m_enemyTeamDataList);
            SCModel.instance.tbsModel.Init(_m_battleInfo);
            reset();
            _m_stepCounter.RegAllDoneDelegate(onLoadOver);
            change2TBSGame();
            SCMsgCenter.SendMsg(SCMsgConst.TBS_GAME_START);
        }

        public void UnloadTBSGame()
        {
            SCMsgCenter.SendMsg(SCMsgConst.TBS_GAME_FINISH);
            change2OWGame();
        }
        private void change2TBSGame()
        {
            SCCommon.SetGameObjectEnable(SCGame.instance.playerGO, false);

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

        private void change2OWGame()
        {
            Time.timeScale = 1;

            SCCommon.SetGameObjectEnable(SCGame.instance.playerGO, true);
            SCGame.instance.owCamera.gameObject.SetActive(true);
            SCGame.instance.virtualCamera.gameObject.SetActive(false);
            Cursor.visible = false;

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

        private void onLoadOver()
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

        public void reset()
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