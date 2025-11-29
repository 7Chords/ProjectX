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
        private int _m_curSelectActorIdx;
        public UIPanelTBSEnemyHud(UIMonoTBSEnemyHud _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }


        public override void AfterInitialize()
        {
            _m_enemyHudItemList = new List<UIPanelTBSEnemyHudItem>();
        }
        public override void BeforeDiscard()
        {
        }
        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_SELECT_SINGLE_ENEMY_TARGET_CHG, onTBSSelectEnemyTargetChg);
        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_SELECT_SINGLE_ENEMY_TARGET_CHG, onTBSSelectEnemyTargetChg);

        }

        private void spawnItems()
        {
            if (_m_enemyActorList == null)
                return;
            if (_m_enemyHudItemList == null)
                return;

            _m_curSelectActorIdx = 0;

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
            UIPanelTBSEnemyHudItem tmpItem;

            for (int i = 0; i < _m_enemyHudItemList.Count; i++)
            {
                tmpItem = _m_enemyHudItemList[i];
                if (tmpItem == null)
                    continue;
                if (i == _m_curSelectActorIdx)
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

        private void onTBSSelectEnemyTargetChg(object[] _args)
        {
            if (_args == null || _args.Length == 0)
                return;
            _m_curSelectActorIdx = (int)_args[0];
            refreshItemListShow();
        }
    }
}
