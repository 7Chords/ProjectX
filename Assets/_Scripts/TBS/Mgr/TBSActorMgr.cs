using GameCore.RefData;
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
        private TBSGameMono _m_gameMono;//回合制战斗全局mono

        private int _m_curActionActorIndex;


        private int _m_selectSingleEnemyTargetIndex;
        private int _m_selectSinglePlayerTargetIndex;
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_REST, onTBSActorMgrRest);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_ACTION_END, onTBSActorActionEnd);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_DEFENCE, onTBSActorDefence);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_ATTACK, onTBSActorAttack);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_SKILL, onTBSActorSkill);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_ITEM, onTBSActorItem);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SELECT_SINGLE_ENEMY_TARGET_CHG, onTBSSelectSingleEnemyTargetChg);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SELECT_SINGLE_PLAYER_TARGET_CHG, onTBSSelectSinglePlayerTargetChg);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_TURN_CHG_SHOW_END, onTBSTurnChgShowEnd);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);

            _m_playerActorModuleList = new List<TBSActorBase>();
            _m_enemyActorModuleList = new List<TBSActorBase>();
        }
        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_WORK, onTBSActorMgrWork);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_MGR_REST, onTBSActorMgrRest);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_ACTION_END, onTBSActorActionEnd);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_DEFENCE, onTBSActorDefence);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_ATTACK, onTBSActorAttack);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_SKILL, onTBSActorSkill);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_ITEM, onTBSActorItem);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SELECT_SINGLE_ENEMY_TARGET_CHG, onTBSSelectSingleEnemyTargetChg);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SELECT_SINGLE_PLAYER_TARGET_CHG, onTBSSelectSinglePlayerTargetChg);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_TURN_CHG_SHOW_END, onTBSTurnChgShowEnd);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);

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
                    _m_gameMono.playerPosInfoList[i].posTran.position,
                    Quaternion.identity, true);

                _m_playerActorGOList.Add(actorGO);
                actor = TBSEnumFactory.CreateTBSActorByProfession(actorInfo.professionType,
                    actorGO.GetComponent<TBSActorMonoBase>());
                if (actor == null)
                    continue;
                actor.SetActorInfo(actorInfo);
                actor.SetPosInfo(_m_gameMono.playerPosInfoList[i]);
                actor.Initialize();
                _m_playerActorModuleList.Add(actor);
            }
            GameCameraMgr.instance.SetDetailCamera(_m_playerActorModuleList[0].GetDetailCameraPos(), true);

            //生成敌人队伍角色
            for (int i = 0; i < _m_enemyTeamInfo.actorInfoList.Count; i++)
            {
                actorInfo = _m_enemyTeamInfo.actorInfoList[i];
                if (actorInfo == null)
                    continue;
                actorGO = ResourcesHelper.LoadGameObject(actorInfo.characterRefObj.assetModelObjName,
                    _m_gameMono.enemyPosInfoList[i].posTran.position,
                    Quaternion.Euler(new Vector3(0,180,0)), true);//面朝玩家

                _m_enemyActorGOList.Add(actorGO);
                actor = TBSEnumFactory.CreateTBSActorByProfession(actorInfo.professionType,
                    actorGO.GetComponent<TBSActorMonoBase>());
                if (actor == null)
                    continue;
                actor.SetActorInfo(actorInfo);
                actor.SetPosInfo(_m_gameMono.enemyPosInfoList[i]);
                actor.Initialize();
                _m_enemyActorModuleList.Add(actor);

            }

            _m_curActionActorIndex = 0;

            SCModel.instance.tbsModel.playerActorModuleList = _m_playerActorModuleList;
            SCModel.instance.tbsModel.enemyActorModuleList = _m_enemyActorModuleList;
            SCModel.instance.tbsModel.playerActorGOList = _m_playerActorGOList;
            SCModel.instance.tbsModel.enemyActorGOList = _m_enemyActorGOList;
            SCModel.instance.tbsModel.gameMono = _m_gameMono;
            SCModel.instance.tbsModel.curActorIndex = _m_curActionActorIndex;
            SCModel.instance.tbsModel.selectTargetType = _m_playerTeamInfo.actorInfoList[0].attackTargetType;

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
                        _m_curActionActorIndex++;
                        if (_m_curActionActorIndex >= _m_enemyActorGOList.Count)
                        {
                            break;
                        }
                    }
                    while (_m_enemyActorModuleList[_m_curActionActorIndex].actorInfo.hasDead);
                }
                else if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.PLAYER)
                {
                    do
                    {
                        _m_curActionActorIndex++;
                        if (_m_curActionActorIndex >= _m_playerActorGOList.Count)
                        {
                            break;
                        }
                    }
                    while (_m_playerActorModuleList[_m_curActionActorIndex].actorInfo.hasDead);
                }
            }


            jumpToNextActorIdx();

            //更换回合持有方了 代码时序保证先更换回合持有方 再更换角色操作
            if ((SCModel.instance.tbsModel.curTurnType == ETBSTurnType.ENEMY
                && _m_curActionActorIndex >= _m_enemyActorGOList.Count)
                || (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.PLAYER
                && _m_curActionActorIndex >= _m_playerActorGOList.Count))
            {
                _m_curActionActorIndex = -1;
                //发送队伍行动结束的信息
                SCMsgCenter.SendMsg(SCMsgConst.TBS_TRAM_ACTION_END);

                jumpToNextActorIdx();
            }

            SCModel.instance.tbsModel.curActorIndex = _m_curActionActorIndex;

            //不是牵扯到回合持有者切换的处理
            if (_m_curActionActorIndex != 0)
            {
                refreshCameraAndCursor(true);

                if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.ENEMY)
                    (_m_enemyActorModuleList[_m_curActionActorIndex] as ITBSEnemyActor).DealEnemyAction();
                else
                    SCModel.instance.tbsModel.selectTargetType = _m_playerActorModuleList[_m_curActionActorIndex].actorInfo.attackTargetType;
            }
            else
            {
                GameCameraMgr.instance.SetCameraFollow(null);
                GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSTurnChg(SCUIShowType.ADDITION));
            }
        }



        private void onTBSActorDefence()
        {
            _m_playerActorModuleList[_m_curActionActorIndex].Defend();
        }

        private void onTBSActorAttack()
        {
            List<TBSActorBase> targetList = new List<TBSActorBase>();
            if(_m_playerActorModuleList[_m_curActionActorIndex].actorInfo.attackTargetType == ETargetType.ALL)
            {
                targetList = _m_enemyActorModuleList;
                GameCameraMgr.instance.SetCameraTarget(_m_gameMono.playerLookEnemyCenterPos);
                _m_playerActorModuleList[_m_curActionActorIndex].Attack(_m_playerActorModuleList[_m_curActionActorIndex].actorInfo.attackTargetType
                    ,targetList);
            }
            else if(_m_playerActorModuleList[_m_curActionActorIndex].actorInfo.attackTargetType == ETargetType.SINGLE)
            {
                targetList.Add(_m_enemyActorModuleList[_m_selectSingleEnemyTargetIndex]);
                GameCameraMgr.instance.SetCameraTarget(_m_enemyActorModuleList[_m_selectSingleEnemyTargetIndex].GetAsCameraTargetTran());
                _m_playerActorModuleList[_m_curActionActorIndex].Attack(_m_playerActorModuleList[_m_curActionActorIndex].actorInfo.attackTargetType
                    ,targetList);
            }
        }

        private void onTBSActorSkill(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            long skillId = (long)_objs[0];

            TBSActorSkillRefObj skillRefObj = SCRefDataMgr.instance.tbsActorSkillRefList.refDataList.Find(x => x.id == skillId);
            if (skillRefObj == null)
                return;

            List<TBSActorBase> targetList = new List<TBSActorBase>();
            if (!skillRefObj.isPlayerTarget)
            {
                if (skillRefObj.damageTargetType == ETargetType.ALL)
                {
                    targetList = _m_enemyActorModuleList;
                    _m_playerActorModuleList[_m_curActionActorIndex].ReleaseSkill(skillId, targetList);

                }
                else if (skillRefObj.damageTargetType == ETargetType.SINGLE)
                {
                    targetList.Add(_m_enemyActorModuleList[_m_selectSingleEnemyTargetIndex]);
                    _m_playerActorModuleList[_m_curActionActorIndex].ReleaseSkill(skillId, targetList);
                }
            }
            else
            {
                if (skillRefObj.damageTargetType == ETargetType.ALL)
                {
                    targetList = _m_playerActorModuleList;
                    _m_playerActorModuleList[_m_curActionActorIndex].ReleaseSkill(skillId, targetList);

                }
                else if (skillRefObj.damageTargetType == ETargetType.SINGLE)
                {
                    targetList.Add(_m_playerActorModuleList[_m_selectSinglePlayerTargetIndex]);
                    _m_playerActorModuleList[_m_curActionActorIndex].ReleaseSkill(skillId, targetList);
                }
            }
        }

        private void onTBSActorItem(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            long itemId = (long)_objs[0];
            ItemRefObj itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == itemId);
            if (itemRefObj == null)
                return;


            List<TBSActorBase> targetList = new List<TBSActorBase>();
            if (itemRefObj.itemTargetType == ETargetType.ALL)
            {
                if (!itemRefObj.isPlayerTarget)
                    targetList = _m_enemyActorModuleList;
                else
                    targetList = _m_playerActorModuleList;
                _m_playerActorModuleList[_m_curActionActorIndex].UseItem(itemId, targetList);

            }
            else if (itemRefObj.itemTargetType == ETargetType.SINGLE)
            {
                if (!itemRefObj.isPlayerTarget)
                    targetList.Add(_m_enemyActorModuleList[_m_selectSingleEnemyTargetIndex]);
                else
                    targetList.Add(_m_playerActorModuleList[_m_selectSinglePlayerTargetIndex]);
                _m_playerActorModuleList[_m_curActionActorIndex].UseItem(itemId, targetList);
            }

        }
        private void onTBSSelectSingleEnemyTargetChg()
        {
            _m_selectSingleEnemyTargetIndex = SCModel.instance.tbsModel.curSelectSingleEnemyTargetIdx;
        }

        private void onTBSSelectSinglePlayerTargetChg()
        {
            _m_selectSinglePlayerTargetIndex = SCModel.instance.tbsModel.curSelectSinglePlayerTargetIdx;
        }
        private void onTBSTurnChgShowEnd()
        {
            if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.ENEMY)
                (_m_enemyActorModuleList[_m_curActionActorIndex] as ITBSEnemyActor).DealEnemyAction();
            else
                SCModel.instance.tbsModel.selectTargetType = _m_playerActorModuleList[_m_curActionActorIndex].actorInfo.attackTargetType;


            refreshCameraAndCursor(true);
        }

        private void onTBSActorDie(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            long runningId = (long)_objs[0];
            TBSActorBase actor = SCModel.instance.tbsModel.GetActorByRunningId(runningId);
            if (actor == null)
                return;

            if (SCModel.instance.tbsModel.CheckAllActorsDead(true))
            {
                SCMsgCenter.SendMsg(SCMsgConst.TBS_ALL_PLAYER_ACTOR_DIE);
                return;
            }
            else if (SCModel.instance.tbsModel.CheckAllActorsDead(false))
            {
                SCMsgCenter.SendMsg(SCMsgConst.TBS_ALL_ENEMY_ACTOR_DIE);
                return;
            }
        }

        #endregion

        /// <summary>
        /// 刷新光标和相机
        /// </summary>t
        /// <param name="_reSetFollow"></param>
        /// <param name="_firstSet"></param>
        private void refreshCameraAndCursor(bool _reSetFollow,bool _firstSet = false)
        {
            if (_m_enemyActorGOList == null || _m_enemyActorGOList.Count == 0 || _m_playerActorGOList.Count == 0 ||
                _m_playerActorGOList.Count == 0 || _m_enemyActorModuleList == null || _m_enemyActorModuleList.Count == 0)
                return;


            void hideUIAndCursor()
            {
                //ui不用隐藏了 因为这个方法触发时的情况main和enemyhud已经隐藏了

                TBSCursorMgr.instance.HideSelectionCursor();

            }

            void showUIAndCursor()
            {
                //第一次要等相机到达正确位置了才加载敌人hud
                if (_firstSet)
                    GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSEnemyHud(SCUIShowType.ADDITION, _m_enemyActorModuleList));
                else
                {
                    GameCoreMgr.instance.uiCoreMgr.ShowNodeButNotMove2Top(nameof(UINodeTBSEnemyHud));
                    GameCoreMgr.instance.uiCoreMgr.ShowNode(nameof(UINodeTBSMain));
                }

                //设置光标
                List<Vector3> worldPosList = new List<Vector3>();
                if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                    worldPosList.Add(_m_enemyActorModuleList[_m_selectSingleEnemyTargetIndex].GetCursorPos());
                else if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
                {
                    worldPosList = SCModel.instance.tbsModel.GetPosList(false, ETargetAliveType.ALIVE);
                }
                TBSCursorMgr.instance.SetSelectionCursor(worldPosList);
            }

            if (SCModel.instance.tbsModel.curTurnType == ETBSTurnType.PLAYER)
            {

                float offsetChangeDuration = 0;
                if (_firstSet)
                    offsetChangeDuration = GameConst.CAMERA_OFFSET_TRANSITION_DURATION;
                else
                    offsetChangeDuration = _m_curActionActorIndex == 0 ? 0 : GameConst.CAMERA_OFFSET_TRANSITION_DURATION;

                if(offsetChangeDuration == 0)
                {
                    //设置相机
                    GameCameraMgr.instance.SetCameraTarget(_m_gameMono.playerLookEnemyCenterPos);
                    if (_reSetFollow)
                        GameCameraMgr.instance.SetCameraFollow(_m_playerActorModuleList[_m_curActionActorIndex].GetModelGameObject().transform, GameConst.CAMERA_FOLLOW_CHANGE_DURATION, hideUIAndCursor, showUIAndCursor);

                    GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(_m_playerActorModuleList[_m_curActionActorIndex].GetActorCameraTran().position
                        , true, offsetChangeDuration);
                }
                else if(offsetChangeDuration == GameConst.CAMERA_OFFSET_TRANSITION_DURATION)
                {
                    //设置相机
                    GameCameraMgr.instance.SetCameraTarget(_m_gameMono.playerLookEnemyCenterPos);
                    if (_reSetFollow)
                        GameCameraMgr.instance.SetCameraFollow(_m_playerActorModuleList[_m_curActionActorIndex].GetModelGameObject().transform);

                    GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(_m_playerActorModuleList[_m_curActionActorIndex].GetActorCameraTran().position
                        , true, offsetChangeDuration, hideUIAndCursor, showUIAndCursor);
                }
            }
            else
            {
                //设置相机
                GameCameraMgr.instance.SetCameraFollow(_m_enemyActorModuleList[_m_curActionActorIndex].GetModelGameObject().transform);

                GameCameraMgr.instance.SetCameraPositionOffsetWithFollow(_m_enemyActorModuleList[_m_curActionActorIndex].GetActorCameraTran().position, false, 0f);
            }
        }
    }
}
