using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSEnemyHud : _ASCUIPanelBase<UIMonoTBSEnemyHud>
    {

        private List<TBSActorBase> _m_enemyActorList;
        private List<UIPanelTBSEnemyHudItem> _m_enemyHudItemList;
        //private int _m_curSelectActorIdx;
        private List<int> _m_curSelectActorIdxList;
        public UIPanelTBSEnemyHud(UIMonoTBSEnemyHud _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }


        public override void AfterInitialize()
        {
            _m_enemyHudItemList = new List<UIPanelTBSEnemyHudItem>();
            _m_curSelectActorIdxList = new List<int>();
        }
        public override void BeforeDiscard()
        {
        }
        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SELECT_SINGLE_ENEMY_TARGET_CHG, onTBSSelectSingleEnemyTargetChg);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SELECT_ENEMY_ALL_OR_SINGLE_STATE_SWITCH, onTBSSelectEnemyAllOrSingleStateSwitch);

        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SELECT_SINGLE_ENEMY_TARGET_CHG, onTBSSelectSingleEnemyTargetChg);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SELECT_ENEMY_ALL_OR_SINGLE_STATE_SWITCH, onTBSSelectEnemyAllOrSingleStateSwitch);

            //refreshItemListShow();
        }

        private void spawnItems()
        {
            if (_m_enemyActorList == null)
                return;
            if (_m_enemyHudItemList == null)
                return;



            GameObject tmpGO;
            UIPanelTBSEnemyHudItem tmpItem;

            for(int i =0;i< _m_enemyActorList.Count;i++)
            {
                tmpGO = ResourcesHelper.LoadGameObject(mono.enemyHudItemObjName, GetGameObject().transform);
                tmpGO.GetRectTransform().localPosition = SCUICommon.WorldPointToUIPoint(GetGameObject().GetRectTransform(), _m_enemyActorList[i].GetGameObject().transform.position);
                tmpGO.GetRectTransform().localPosition += mono.enemyHudItemOffset;
                tmpItem = new UIPanelTBSEnemyHudItem(tmpGO.GetComponent<UIMonoTBSEnemyHudItem>(), SCUIShowType.INTERNAL);
                tmpItem.SetInfo(_m_enemyActorList[i].actorInfo);
                _m_enemyHudItemList.Add(tmpItem);
            }
            refreshItemListShow();
        }

        private void refreshItemListShow()
        {

            if (_m_curSelectActorIdxList == null)
                _m_curSelectActorIdxList = new List<int>();
            _m_curSelectActorIdxList.Clear();

            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                _m_curSelectActorIdxList.Add(SCModel.instance.tbsModel.curSelectSingleTargetIdx);
            else
            {
                int idx = -1;
                foreach (var actorInfo in SCModel.instance.tbsModel.battleInfo.playerTeamInfo.actorInfoList)
                {
                    if (actorInfo.hasDead)
                        continue;
                    idx++;
                    _m_curSelectActorIdxList.Add(idx);
                }
            }


            UIPanelTBSEnemyHudItem tmpItem;

            for (int i = 0; i < _m_enemyHudItemList.Count; i++)
            {
                tmpItem = _m_enemyHudItemList[i];
                if (tmpItem == null)
                    continue;

                if (_m_curSelectActorIdxList.Contains(i))
                    tmpItem.ShowPanel();
                else
                    tmpItem.HidePanel();
            }
        }

        public void SetInfo(List<TBSActorBase> _actorList)
        {
            _m_enemyActorList = _actorList;
            spawnItems();
        }

        //private void onTBSActorTargetHighlightLeft()
        //{
        //    _m_curSelectActorIdx--;
        //    if (_m_curSelectActorIdx < 0)
        //        _m_curSelectActorIdx = _m_enemyHudItemList.Count - 1;
        //    refreshItemListShow();
        //}

        //private void onTBSActorTargetHighlightRight()
        //{
        //    Debug.Log("right!!!");
        //    _m_curSelectActorIdx++;
        //    if (_m_curSelectActorIdx > _m_enemyHudItemList.Count - 1)
        //        _m_curSelectActorIdx = 0;
        //    refreshItemListShow();

        //}

        private void onTBSSelectSingleEnemyTargetChg()
        {
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
                return;

            _m_curSelectActorIdxList.Clear();
            _m_curSelectActorIdxList.Add(SCModel.instance.tbsModel.curSelectSingleTargetIdx);
            refreshItemListShow();
        }

        private void onTBSSelectEnemyAllOrSingleStateSwitch()
        {
            refreshItemListShow();
        }
    }
}
