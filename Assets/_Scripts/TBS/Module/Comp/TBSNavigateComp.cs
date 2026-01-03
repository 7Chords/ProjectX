using GameCore.UI;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    /// <summary>
    /// 导航组件
    /// </summary>
    public class TBSNavigateComp : TBSCompBase
    {

        private int _m_singleEnemyTargetIndex;
        private int _m_singlePlayerTargetIndex;
        public override void OnInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SWITCH_TO_LEFT_INPUT, onTBSSwitchToLeftInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SWITCH_TO_RIGHT_INPUT, onTBSSwitchToRightInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SWITCH_TO_DOWN_INPUT, onTBSSwitchToDownInput);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SWITCH_TO_UP_INPUT, onTBSSwitchToUpInput);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_MOUSE_CLICK_ENEMY_INPUT, onTBSMouseClickEnemyInput);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_MOUSE_CLICK_PLAYER_INPUT, onTBSMouseClickPlayerInput);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);
            

            _m_singleEnemyTargetIndex = 0;
            _m_singlePlayerTargetIndex = 0;
        }


        public override void OnDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SWITCH_TO_LEFT_INPUT, onTBSSwitchToLeftInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SWITCH_TO_RIGHT_INPUT, onTBSSwitchToRightInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SWITCH_TO_DOWN_INPUT, onTBSSwitchToDownInput);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SWITCH_TO_UP_INPUT, onTBSSwitchToUpInput);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_MOUSE_CLICK_ENEMY_INPUT, onTBSMouseClickEnemyInput);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_MOUSE_CLICK_PLAYER_INPUT, onTBSMouseClickPlayerInput);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);

        }

        public override void OnSuspend()
        {

        }

        public override void OnResume()
        {

        }

        private void onTBSSwitchToLeftInput()
        {
            _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
            if(topFullNode.GetNodeName() == nameof(UINodeTBSMain))
            {
                _m_singleEnemyTargetIndex--;
                if (_m_singleEnemyTargetIndex < 0)
                    _m_singleEnemyTargetIndex = SCModel.instance.tbsModel.enemyActorModuleList.Count - 1;
                SCModel.instance.tbsModel.curSelectSingleEnemyTargetIdx = _m_singleEnemyTargetIndex;
            }
            else if(topFullNode.GetNodeName() == nameof(UINodeTBSConfirm) && !(topFullNode as UINodeTBSConfirm).isPlayerTargetConfirm)
            {
                _m_singleEnemyTargetIndex--;
                if (_m_singleEnemyTargetIndex < 0)
                    _m_singleEnemyTargetIndex = SCModel.instance.tbsModel.enemyActorModuleList.Count - 1;
                SCModel.instance.tbsModel.curSelectSingleEnemyTargetIdx = _m_singleEnemyTargetIndex;
            }
            else if(topFullNode.GetNodeName() == nameof(UINodeTBSConfirm) && (topFullNode as UINodeTBSConfirm).isPlayerTargetConfirm)
            {
                //由于摄像头问题 所以索引要反过来处理

                _m_singlePlayerTargetIndex = SCModel.instance.tbsModel.GetNextAliveActorIndex(true, _m_singlePlayerTargetIndex);
                SCModel.instance.tbsModel.curSelectSinglePlayerTargetIdx = _m_singlePlayerTargetIndex;
            }
        }

        private void onTBSSwitchToRightInput()
        {
            _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
            if (topFullNode.GetNodeName() == nameof(UINodeTBSMain))
            {
                _m_singleEnemyTargetIndex++;
                if (_m_singleEnemyTargetIndex > SCModel.instance.tbsModel.enemyActorModuleList.Count - 1)
                    _m_singleEnemyTargetIndex = 0;
                SCModel.instance.tbsModel.curSelectSingleEnemyTargetIdx = _m_singleEnemyTargetIndex;
            }
            else if (topFullNode.GetNodeName() == nameof(UINodeTBSConfirm) && !(topFullNode as UINodeTBSConfirm).isPlayerTargetConfirm)
            {
                _m_singleEnemyTargetIndex++;
                if (_m_singleEnemyTargetIndex > SCModel.instance.tbsModel.enemyActorModuleList.Count - 1)
                    _m_singleEnemyTargetIndex = 0;
                SCModel.instance.tbsModel.curSelectSingleEnemyTargetIdx = _m_singleEnemyTargetIndex;
            }
            else if (topFullNode.GetNodeName() == nameof(UINodeTBSConfirm) && (topFullNode as UINodeTBSConfirm).isPlayerTargetConfirm)
            {
                _m_singlePlayerTargetIndex = SCModel.instance.tbsModel.GetLastAliveActorIndex(true, _m_singlePlayerTargetIndex);
                SCModel.instance.tbsModel.curSelectSinglePlayerTargetIdx = _m_singlePlayerTargetIndex;
            }
        }

        private void onTBSSwitchToUpInput()
        {
            _ASCUINodeBase topNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode();
            if (topNode == null || topNode.hasHideNode)
                return;
            switch (topNode.GetNodeName())
            {
                case nameof(UINodeTBSSkill):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP);
                    break;
                case nameof(UINodeTBSItem):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ITEM_HIGHTLIGHT_UP);
                    break;
                default:
                    break;
            }
        }

        private void onTBSSwitchToDownInput()
        {


            _ASCUINodeBase topNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode();
            if (topNode == null || topNode.hasHideNode)
                return;
            switch (topNode.GetNodeName())
            {
                case nameof(UINodeTBSSkill):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN);
                    break;
                case nameof(UINodeTBSItem):
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ITEM_HIGHTLIGHT_DOWN);
                    break;
                default:
                    break;
            }
        }

        private void onTBSMouseClickEnemyInput(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
            {
                _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
                if (topFullNode.GetNodeName() == nameof(UINodeTBSConfirm) && !(topFullNode as UINodeTBSConfirm).isPlayerTargetConfirm)
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_CONFIRM_INPUT);

                return;
            }


            GameObject enemyGO = _objs[0] as GameObject;
            if (enemyGO == null)
                return;
            int goIndex = SCModel.instance.tbsModel.GetActorGOIndex(enemyGO, false);

            //重复点击选择同一个角色 在处于确认状态的情况下表示“确认”
            if (goIndex == _m_singleEnemyTargetIndex)
            {
                _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
                if(topFullNode.GetNodeName() == nameof(UINodeTBSConfirm))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_CONFIRM_INPUT);
            }
            else
            {
                _m_singleEnemyTargetIndex = goIndex;
                SCModel.instance.tbsModel.curSelectSingleEnemyTargetIdx = _m_singleEnemyTargetIndex;
            }
        }

        private void onTBSMouseClickPlayerInput(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
            {
                _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
                if (topFullNode.GetNodeName() == nameof(UINodeTBSConfirm) && (topFullNode as UINodeTBSConfirm).isPlayerTargetConfirm)
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_CONFIRM_INPUT);

                return;
            }


            GameObject playerGO = _objs[0] as GameObject;
            if (playerGO == null)
                return;
            int goIndex = SCModel.instance.tbsModel.GetActorGOIndex(playerGO, true);

            //重复点击选择同一个角色 在处于确认状态的情况下表示“确认”
            if (goIndex == _m_singlePlayerTargetIndex)
            {
                _ASCUINodeBase topFullNode = GameCoreMgr.instance.uiCoreMgr.GetTopNode(SCUIShowType.FULL);
                if (topFullNode.GetNodeName() == nameof(UINodeTBSConfirm))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_CONFIRM_INPUT);
            }
            else
            {
                _m_singlePlayerTargetIndex = goIndex;
                SCModel.instance.tbsModel.curSelectSinglePlayerTargetIdx = _m_singlePlayerTargetIndex;
            }
        }


        private void onTBSActorDie(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            long runningId = (long)_objs[0];
            TBSActorBase actor = SCModel.instance.tbsModel.GetActorByRunningId(runningId);
            if (actor == null)
                return;

            int actorIdx = -1;
            if (actor.actorInfo.isEnemy)
            {
                actorIdx = SCModel.instance.tbsModel.enemyActorModuleList.IndexOf(actor);
                SCModel.instance.tbsModel.enemyActorModuleList.Remove(actor);
                GameObject actorGO = actor.GetActorGameObject();
                SCModel.instance.tbsModel.enemyActorGOList.Remove(actorGO);

                SCMsgCenter.SendMsg(SCMsgConst.TBS_ENEMY_ACTOR_REMOVE_FROM_LIST, runningId);

                //处理光标的越界问题
                if (actorIdx == _m_singleEnemyTargetIndex)
                {
                    if (_m_singleEnemyTargetIndex >= SCModel.instance.tbsModel.enemyActorModuleList.Count)
                    {
                        if (_m_singleEnemyTargetIndex - 1 >= 0)
                            _m_singleEnemyTargetIndex--;

                        SCModel.instance.tbsModel.curSelectSingleEnemyTargetIdx = _m_singleEnemyTargetIndex;
                    }
                }
            }
            else//如果死亡的是玩家 只需要纠正一下当前选择的索引
            {
                if (SCModel.instance.tbsModel.CheckAllActorsDead(true))
                    return;
                actorIdx = SCModel.instance.tbsModel.playerActorModuleList.IndexOf(actor);
                if (actorIdx == _m_singlePlayerTargetIndex)
                {
                    TBSActorBase tmpActor = SCModel.instance.tbsModel.playerActorModuleList[_m_singlePlayerTargetIndex];
                    while(tmpActor.actorInfo.hasDead)
                    {
                        _m_singlePlayerTargetIndex++;
                        if(_m_singlePlayerTargetIndex >= SCModel.instance.tbsModel.playerActorModuleList.Count)
                            _m_singlePlayerTargetIndex = 0;
                        tmpActor = SCModel.instance.tbsModel.playerActorModuleList[_m_singlePlayerTargetIndex];
                    }

                    SCModel.instance.tbsModel.curSelectSinglePlayerTargetIdx = _m_singlePlayerTargetIndex;
                }
            }
        }
    }
}
