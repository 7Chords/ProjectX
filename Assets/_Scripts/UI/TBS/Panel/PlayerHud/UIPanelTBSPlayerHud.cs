using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSPlayerHud : _ASCUIPanelBase<UIMonoTBSPlayerHud>
    {

        private List<TBSActorBase> _m_playerActorList;
        private List<UIPanelTBSPlayerHudItem> _m_playerHudItemList;
        //private int _m_curSelectActorIdx;
        private List<int> _m_curSelectActorIdxList;
        public UIPanelTBSPlayerHud(UIMonoTBSPlayerHud _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }


        public override void AfterInitialize()
        {

            _m_playerHudItemList = new List<UIPanelTBSPlayerHudItem>();
            _m_curSelectActorIdxList = new List<int>();
        }
        public override void BeforeDiscard()
        {
        }
        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_SELECT_SINGLE_PLAYER_TARGET_CHG, onTBSSelectSinglePlayerTargetChg);

        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_SELECT_SINGLE_PLAYER_TARGET_CHG, onTBSSelectSinglePlayerTargetChg);

            refreshItemListShow();
        }

        private void spawnItems()
        {
            if (_m_playerActorList == null)
                return;
            if (_m_playerHudItemList == null)
                return;



            GameObject tmpGO;
            UIPanelTBSPlayerHudItem tmpItem;

            for(int i =0;i< _m_playerActorList.Count;i++)
            {
                tmpGO = ResourcesHelper.LoadGameObject(mono.playerHudItemObjName, GetGameObject().transform);
                tmpGO.GetRectTransform().localPosition = SCUICommon.WorldPointToUIPoint(GetGameObject().GetRectTransform(), _m_playerActorList[i].GetActorGameObject().transform.position);
                tmpGO.GetRectTransform().localPosition += mono.playerHudItemOffset;
                tmpItem = new UIPanelTBSPlayerHudItem(tmpGO.GetComponent<UIMonoTBSPlayerHudItem>(), SCUIShowType.INTERNAL);
                tmpItem.SetInfo(_m_playerActorList[i].actorInfo);
                _m_playerHudItemList.Add(tmpItem);
            }
            refreshItemListShow();
        }

        private void refreshItemListShow()
        {

            if (_m_curSelectActorIdxList == null)
                _m_curSelectActorIdxList = new List<int>();
            _m_curSelectActorIdxList.Clear();

            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                _m_curSelectActorIdxList.Add(SCModel.instance.tbsModel.curSelectSinglePlayerTargetIdx);
            else
            {
                int idx = -1;
                foreach(var actor in SCModel.instance.tbsModel.playerActorModuleList)
                {
                    idx++;
                    if (actor.actorInfo.hasDead)
                        continue;
                    _m_curSelectActorIdxList.Add(idx);
                }
            }


            UIPanelTBSPlayerHudItem tmpItem;

            for (int i = 0; i < _m_playerHudItemList.Count; i++)
            {
                tmpItem = _m_playerHudItemList[i];
                if (tmpItem == null)
                    continue;

                if (_m_curSelectActorIdxList.Contains(i))
                {
                    tmpItem.ShowPanel();
                    tmpItem.SetInfo(_m_playerActorList[i].actorInfo);
                }
                else
                    tmpItem.HidePanel();
            }
        }

        public void SetInfo(List<TBSActorBase> _actorList)
        {
            _m_playerActorList = _actorList;
            spawnItems();
        }

        private void onTBSSelectSinglePlayerTargetChg()
        {
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
                return;
            refreshItemListShow();
        }

    }
}
