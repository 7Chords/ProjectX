using SCFrame;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSActorMgr : TBSSubMgrBase
    {
        public override ETBSSubMgrType tbsSubMgrType => ETBSSubMgrType.ACTOR;

        private TBSTeamInfo _m_playerTeamInfo;
        private TBSTeamInfo _m_enemyTeamInfo;

        private List<GameObject> _m_playerActorGOList;
        private List<GameObject> _m_enemyActorGOList;

        private List<TBSActorBase> _m_playerActorModuleList;
        private List<TBSActorBase> _m_enemyActorModuleList;


        private GameObject _m_tbsStage;//回合制战斗舞台
        private TBSGameMono _m_gameMono;

        private int _m_curSelectActorIndex;
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_REST, onTBSActorMgrRest);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_CHG, onTBSActorChg);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_DEFEND_INPUT, onTBSDefendInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ATTACK_INPUT, onTBSAttackInput);



        }
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_REST, onTBSActorMgrRest);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_CHG, onTBSActorChg);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_DEFEND_INPUT, onTBSDefendInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ATTACK_INPUT, onTBSAttackInput);

        }

        public override void OnResume() { }

        public override void OnSuspend() { }

        #region 事件回调

        private void onTBSActorMgrWork()
        {
            _m_playerTeamInfo = SCModel.instance.tbsModel.battleInfo?.playerTeamInfo;
            _m_enemyTeamInfo = SCModel.instance.tbsModel.battleInfo?.enemyTeamInfo;

            if (_m_playerActorGOList == null)
                _m_playerActorGOList = new List<GameObject>();
            _m_playerActorGOList.Clear();

            if (_m_enemyActorGOList == null)
                _m_enemyActorGOList = new List<GameObject>();
            _m_enemyActorGOList.Clear();

            if (_m_playerActorModuleList == null)
                _m_playerActorModuleList = new List<TBSActorBase>();
            _m_playerActorModuleList.Clear();

            if (_m_enemyActorModuleList == null)
                _m_enemyActorModuleList = new List<TBSActorBase>();
            _m_enemyActorModuleList.Clear();

            //加载舞台
            _m_tbsStage = ResourcesHelper.LoadGameObject(TBSGameMono.assetObjName,
                SCGame.instance.playerGO.transform.position,Quaternion.identity,true);
            _m_gameMono = _m_tbsStage.GetComponent<TBSGameMono>();

            TBSActorInfo actorInfo = null;
            GameObject actorGO = null;
            TBSActorBase actorBase = null;
            //生成玩家队伍角色
            for(int i =0;i<_m_playerTeamInfo.actorInfoList.Count;i++)
            {
                actorInfo = _m_playerTeamInfo.actorInfoList[i];
                if (actorInfo == null)
                    continue;
                actorGO = ResourcesHelper.LoadGameObject(actorInfo.assetObjName,
                    _m_gameMono.playerPosInfoList[i].pos.position,
                    Quaternion.identity, true);

                _m_playerActorGOList.Add(actorGO);
                actorBase = new TBSActorBase(actorGO.GetComponent<TBSActorMonoBase>());
                actorBase.Initialize();
                _m_playerActorModuleList.Add(actorBase);
            }

            //生成敌人队伍角色
            for (int i = 0; i < _m_enemyTeamInfo.actorInfoList.Count; i++)
            {
                actorInfo = _m_enemyTeamInfo.actorInfoList[i];
                if (actorInfo == null)
                    continue;
                actorGO = ResourcesHelper.LoadGameObject(actorInfo.assetObjName,
                    _m_gameMono.enemyPosInfoList[i].pos.position,
                    Quaternion.Euler(new Vector3(0,180,0)), true);//面朝玩家

                _m_enemyActorGOList.Add(actorGO);
                actorBase = new TBSActorBase(actorGO.GetComponent<TBSActorMonoBase>());
                actorBase.Initialize();
                _m_enemyActorModuleList.Add(actorBase);

            }

            _m_curSelectActorIndex = 0;
            if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.PLAYER)
            {
                //设置相机
                GameCameraMgr.instance.SetCameraTarget(_m_enemyActorGOList[0].transform);
                GameCameraMgr.instance.SetCameraFollow(_m_playerActorGOList[_m_curSelectActorIndex].transform,
                    () =>
                    {
                        GameCoreMgr.instance.uiCoreMgr.HideCurNode();
                        TBSCursorMgr.instance.HideSelectionCursor();
                    },
                    () =>
                    {
                        GameCoreMgr.instance.uiCoreMgr.ShowCurNode();
                        TBSCursorMgr.instance.SetSelectionCursorPos(_m_enemyActorGOList[_m_curSelectActorIndex].transform);
                    });
                TBSCursorMgr.instance.SetSelectionCursorPos(_m_enemyActorGOList[_m_curSelectActorIndex].transform);
                GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(_m_gameMono.playerPosInfoList[_m_curSelectActorIndex].cameraIdlePos);
                
            }
            else
            {
                //todo
            }

        }

        private void onTBSActorMgrRest()
        {
            SCCommon.DestoryGameObject(_m_tbsStage);
            _m_gameMono = null;
        }


        private void onTBSActorChg(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            //todo
            int chgStep = (int)_objs[0];
            _m_curSelectActorIndex += chgStep;

            //更换回合持有方了 代码时序保证先更换回合持有方 再更换角色操作
            if ((GameCoreMgr.instance.tbsCoreMgr.getTurnType() == ETBSTurnType.ENEMY
                &&  _m_curSelectActorIndex >= _m_playerActorGOList.Count)
                || (GameCoreMgr.instance.tbsCoreMgr.getTurnType() == ETBSTurnType.PLAYER
                && _m_curSelectActorIndex >= _m_enemyActorGOList.Count))
            {
                _m_curSelectActorIndex = 0;
            }


            if(GameCoreMgr.instance.tbsCoreMgr.getTurnType() == ETBSTurnType.PLAYER)
            {
                //设置相机
                GameCameraMgr.instance.SetCameraTarget(_m_enemyActorGOList[0].transform);
                GameCameraMgr.instance.SetCameraFollow(_m_playerActorGOList[_m_curSelectActorIndex].transform,
                    () =>
                    {
                        TBSCursorMgr.instance.HideSelectionCursor();
                        GameCoreMgr.instance.uiCoreMgr.HideCurNode();
                    },
                    () =>
                    {
                        TBSCursorMgr.instance.SetSelectionCursorPos(_m_enemyActorGOList[_m_curSelectActorIndex].transform);
                        GameCoreMgr.instance.uiCoreMgr.ShowCurNode();
                    });
                GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(_m_gameMono.playerPosInfoList[_m_curSelectActorIndex].cameraIdlePos);
            }
            else if(GameCoreMgr.instance.tbsCoreMgr.getTurnType() == ETBSTurnType.ENEMY)
            {

            }
        }



        private void onTBSDefendInput()
        {

        }

        private void onTBSAttackInput()
        {
            _m_playerActorModuleList[_m_curSelectActorIndex].Attack(_m_enemyActorModuleList[0]);
        }
        #endregion
    }
}
