using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public partial class TBSActorMgr : TBSSubMgrBase
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


        private int _m_singleTargetIndex;
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_REST, onTBSActorMgrRest);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_ACTION_END, onTBSActorActionEnd);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_DEFENCE, onTBSActorDefence);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_ATTACK, onTBSActorAttack);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_SKILL, onTBSActorSkill);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_TARGET_HIGHLIGHT_LEFT, onTBSActorTargetHighlightLeft);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_TARGET_HIGHLIGHT_RIGHT, onTBSActorTargetHighlightRight);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_TURN_CHG_SHOW_END, onTBSTurnChgShowEnd);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SELECT_ENEMY_ALL_OR_SINGLE_STATE_SWITCH, onTBSSelectEnemyAllOrSingleStateSwitch);

        }
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_REST, onTBSActorMgrRest);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_ACTION_END, onTBSActorActionEnd);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_DEFENCE, onTBSActorDefence);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_ATTACK, onTBSActorAttack);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_SKILL, onTBSActorSkill);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_TARGET_HIGHLIGHT_LEFT, onTBSActorTargetHighlightLeft);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_TARGET_HIGHLIGHT_RIGHT, onTBSActorTargetHighlightRight);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_TURN_CHG_SHOW_END, onTBSTurnChgShowEnd);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SELECT_ENEMY_ALL_OR_SINGLE_STATE_SWITCH, onTBSSelectEnemyAllOrSingleStateSwitch);

            foreach (var actor in _m_playerActorModuleList)
            {
                if (actor == null)
                    continue;
                actor.Discard();
            }
            _m_playerActorModuleList.Clear();
            _m_playerActorModuleList = null;

            foreach (var actor in _m_enemyActorModuleList)
            {
                if (actor == null)
                    continue;
                actor.Discard();
            }
            _m_enemyActorModuleList.Clear();
            _m_enemyActorModuleList = null;
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
            TBSActorBase actor = null;
            //生成玩家队伍角色
            for(int i =0;i<_m_playerTeamInfo.actorInfoList.Count;i++)
            {
                actorInfo = _m_playerTeamInfo.actorInfoList[i];
                if (actorInfo == null)
                    continue;
                actorGO = ResourcesHelper.LoadGameObject(actorInfo.characterRefObj.assetModelObjName,
                    _m_gameMono.playerPosInfoList[i].pos.position,
                    Quaternion.identity, true);

                _m_playerActorGOList.Add(actorGO);
                actor = TBSEnumFactory.CreateTBSActorByProfession(actorInfo.professionType,
                    actorGO.GetComponent<TBSActorMonoBase>());
                if (actor == null)
                    continue;
                actor.Initialize();
                actor.SetActorInfo(actorInfo);
                actor.SetPosInfo(_m_gameMono.playerPosInfoList[i]);
                _m_playerActorModuleList.Add(actor);
            }

            //生成敌人队伍角色
            for (int i = 0; i < _m_enemyTeamInfo.actorInfoList.Count; i++)
            {
                actorInfo = _m_enemyTeamInfo.actorInfoList[i];
                if (actorInfo == null)
                    continue;
                actorGO = ResourcesHelper.LoadGameObject(actorInfo.characterRefObj.assetModelObjName,
                    _m_gameMono.enemyPosInfoList[i].pos.position,
                    Quaternion.Euler(new Vector3(0,180,0)), true);//面朝玩家

                _m_enemyActorGOList.Add(actorGO);
                actor = TBSEnumFactory.CreateTBSActorByProfession(actorInfo.professionType,
                    actorGO.GetComponent<TBSActorMonoBase>());
                if (actor == null)
                    continue;
                actor.Initialize();
                actor.SetActorInfo(actorInfo);
                actor.SetPosInfo(_m_gameMono.enemyPosInfoList[i]);
                _m_enemyActorModuleList.Add(actor);

            }

            _m_curSelectActorIndex = 0;

            SCModel.instance.tbsModel.curActorIndex = _m_curSelectActorIndex;
            SCModel.instance.tbsModel.selectTargetType = _m_playerTeamInfo.actorInfoList[0].attackTargetType;
            SCModel.instance.tbsModel.playerActorModuleList = _m_playerActorModuleList;
            SCModel.instance.tbsModel.enemyActorModuleList = _m_enemyActorModuleList;


            refreshCameraAndCursor(true,true);

        }

        private void onTBSActorMgrRest()
        {
            _m_playerTeamInfo = null;
            _m_enemyTeamInfo = null;
            _m_playerActorGOList.Clear();
            _m_playerActorGOList = null;
            _m_enemyActorGOList.Clear();
            _m_enemyActorGOList = null;
            _m_playerActorModuleList.Clear();
            _m_playerActorModuleList = null;
            _m_enemyActorModuleList.Clear();
            _m_enemyActorModuleList = null;

            SCCommon.DestoryGameObject(_m_tbsStage);
            _m_gameMono = null;
        }


        private void onTBSActorActionEnd(object[] _objs)
        {

            void jumpToNextActorIdx()
            {
                //跳到下一个行动角色索引
                if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.ENEMY)
                {
                    do
                    {
                        _m_curSelectActorIndex++;
                        if (_m_curSelectActorIndex >= _m_enemyActorGOList.Count)
                        {
                            break;
                        }
                    }
                    while (_m_enemyActorModuleList[_m_curSelectActorIndex].actorInfo.hasDead);
                }
                else if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.PLAYER)
                {
                    do
                    {
                        _m_curSelectActorIndex++;
                        if (_m_curSelectActorIndex >= _m_playerActorGOList.Count)
                        {
                            break;
                        }
                    }
                    while (_m_playerActorModuleList[_m_curSelectActorIndex].actorInfo.hasDead);
                }
            }


            jumpToNextActorIdx();

            //更换回合持有方了 代码时序保证先更换回合持有方 再更换角色操作
            if ((SCModel.instance.tbsModel.curTurnType == ETBSTurnType.ENEMY
                && _m_curSelectActorIndex >= _m_enemyActorGOList.Count)
                || (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.PLAYER
                && _m_curSelectActorIndex >= _m_playerActorGOList.Count))
            {
                _m_curSelectActorIndex = -1;
                //发送队伍行动结束的信息
                SCMsgCenter.SendMsgAct(SCMsgConst.TBS_TRAM_ACTION_END);

                jumpToNextActorIdx();
            }

            SCModel.instance.tbsModel.curActorIndex = _m_curSelectActorIndex;

            //不是牵扯到回合持有者切换的处理
            if (_m_curSelectActorIndex != 0)
            {

                if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.ENEMY)
                    (_m_enemyActorModuleList[_m_curSelectActorIndex] as ITBSEnemyActor).DealEnemyAction();
                else
                    SCModel.instance.tbsModel.selectTargetType = _m_playerActorModuleList[_m_curSelectActorIndex].actorInfo.attackTargetType;

                refreshCameraAndCursor(true);
            }
            else
            {
                GameCameraMgr.instance.SetCameraFollow(null);
                GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSTurnChg(SCUIShowType.ADDITION));
            }
        }



        private void onTBSActorDefence()
        {
            _m_playerActorModuleList[_m_curSelectActorIndex].Defend();
        }

        private void onTBSActorAttack()
        {
            _m_playerActorModuleList[_m_curSelectActorIndex].Attack(_m_enemyActorModuleList[_m_curSelectActorIndex].actorInfo.attackTargetType
                ,_m_enemyActorModuleList);
        }

        private void onTBSActorSkill(object[] _args)
        {
            if (_args == null || _args.Length == 0)
                return;
            long skillId = (long)_args[0];
            _m_playerActorModuleList[_m_curSelectActorIndex].ReleaseSkill(skillId, _m_enemyActorModuleList[_m_singleTargetIndex]);
        }

        private void onTBSActorTargetHighlightLeft()
        {
            _m_singleTargetIndex--;
            if (_m_singleTargetIndex < 0)
                _m_singleTargetIndex = _m_enemyActorModuleList.Count - 1;
            SCModel.instance.tbsModel.curSelectSingleTargetIdx = _m_singleTargetIndex;
            TBSCursorMgr.instance.MoveSingleCursor2Pos(_m_enemyActorModuleList[_m_singleTargetIndex].GetCursorPos());

        }

        private void onTBSActorTargetHighlightRight()
        {
            _m_singleTargetIndex++;
            if (_m_singleTargetIndex > _m_enemyActorModuleList.Count - 1)
                _m_singleTargetIndex = 0;
            SCModel.instance.tbsModel.curSelectSingleTargetIdx = _m_singleTargetIndex;
            TBSCursorMgr.instance.MoveSingleCursor2Pos(_m_enemyActorModuleList[_m_singleTargetIndex].GetCursorPos());
        }

        private void onTBSTurnChgShowEnd()
        {
            if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.ENEMY)
                (_m_enemyActorModuleList[_m_curSelectActorIndex] as ITBSEnemyActor).DealEnemyAction();
            else
                SCModel.instance.tbsModel.selectTargetType = _m_playerActorModuleList[_m_curSelectActorIndex].actorInfo.attackTargetType;


            refreshCameraAndCursor(true);
        }

        private void onTBSActorDie(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            if (SCModel.instance.tbsModel.CheckAllActorsDead(true))
                GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSLose(SCUIShowType.ADDITION));
            else if(SCModel.instance.tbsModel.CheckAllActorsDead(false))
                GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSWin(SCUIShowType.ADDITION));

        }

        private void onTBSSelectEnemyAllOrSingleStateSwitch()
        {
            TBSCursorMgr.instance.ChangeCursorShowMode(SCModel.instance.tbsModel.selectTargetType);
        }
        #endregion

        private void refreshCameraAndCursor(bool _reSetFollow,bool _firstSet = false)
        {
            if (_m_enemyActorGOList == null || _m_enemyActorGOList.Count == 0 || _m_playerActorGOList.Count == 0 ||
                _m_playerActorGOList.Count == 0 || _m_enemyActorModuleList == null || _m_enemyActorModuleList.Count == 0)
                return;


            void hideUIAndCursor()
            {
                if (!_firstSet)
                {
                    GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSMain));
                    GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
                }
                TBSCursorMgr.instance.HideSelectionCursor();

            }

            void showUIAndCursor()
            {
                //第一次要等相机到达正确位置了才加载敌人hud
                if (_firstSet)
                    GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSEnemyHud(SCUIShowType.ADDITION, _m_enemyActorModuleList));
                else
                {
                    GameCoreMgr.instance.uiCoreMgr.ShowNode(nameof(UINodeTBSEnemyHud));
                    GameCoreMgr.instance.uiCoreMgr.ShowNode(nameof(UINodeTBSMain));
                }

                //设置光标
                List<Vector3> worldPosList = new List<Vector3>();
                if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                    worldPosList.Add(_m_enemyActorModuleList[_m_singleTargetIndex].GetCursorPos());
                else if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
                {
                    foreach(var module in _m_enemyActorModuleList)
                    {
                        worldPosList.Add(module.GetCursorPos());
                    }
                }
                TBSCursorMgr.instance.SetSelectionCursor(worldPosList);
            }

            void setCameraOffset_Player()
            {
                GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(_m_gameMono.playerPosInfoList[_m_curSelectActorIndex].cameraIdlePos, hideUIAndCursor, showUIAndCursor);
            }

            void setCameraOffset_Enemy()
            {
                GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(_m_gameMono.enemyPosInfoList[_m_curSelectActorIndex].cameraIdlePos);
            }

            if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.PLAYER)
            {
                //设置相机
                GameCameraMgr.instance.SetCameraTarget(_m_gameMono.playerLookEnemyCenterPos);
                if(_reSetFollow)
                    GameCameraMgr.instance.SetCameraFollow(_m_playerActorGOList[_m_curSelectActorIndex].transform);
                setCameraOffset_Player();

            }
            else if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.ENEMY)
            {
                //设置相机
                GameCameraMgr.instance.SetCameraTarget(_m_gameMono.enemyLookPlayerCenterPos);
                GameCameraMgr.instance.SetCameraFollow(_m_enemyActorGOList[_m_curSelectActorIndex].transform);
                setCameraOffset_Enemy();
            }
        }
    }
}
