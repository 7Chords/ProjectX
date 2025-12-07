using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSCharacterActionContainer : UIPanelContainerBase<UIMonoTBSCharacterActionContainer, UIPanelTBSCharacterActionItem, UIMonoTBSCharacterActionItem>
    {

        private List<UIPanelTBSCharacterActionItem> _m_infoItemList;//item列表

        private List<TBSActorInfo> _m_actorInfoList;
        public UIPanelTBSCharacterActionContainer(UIMonoTBSCharacterActionContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }

        public override void BeforeDiscard()
        {
            if (_m_infoItemList != null)
            {
                foreach (var item in _m_infoItemList)
                    item.Discard();
            }
            _m_infoItemList.Clear();
            _m_infoItemList = null;
        }

        public override void AfterInitialize()
        {
            _m_infoItemList = new List<UIPanelTBSCharacterActionItem>();
        }

        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_CHG, onTBSActorChg);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);

            if (_m_infoItemList != null)
            {
                foreach (var item in _m_infoItemList)
                    item.HidePanel();
            }
        }


        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_CHG, onTBSActorChg);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);

        }

        protected override GameObject creatItemGO()
        {
            return ResourcesHelper.LoadGameObject(mono.prefabItemObjName);
        }

        protected override UIPanelTBSCharacterActionItem creatItemPanel(UIMonoTBSCharacterActionItem _mono)
        {
            return new UIPanelTBSCharacterActionItem(_mono, SCUIShowType.INTERNAL);
        }

        public void SetInfoList(List<TBSActorInfo> _actorInfoList)
        {
            if (_actorInfoList == null)
                return;
            if (_m_infoItemList == null)
                return;

            _m_actorInfoList = _actorInfoList;
            refreshShow();
        }


        private void refreshShow()
        {
            if (_m_actorInfoList == null)
                return;
            int i = 0, count = 0;
            UIPanelTBSCharacterActionItem item = null;
            for (i = 0; i < _m_actorInfoList.Count; i++)
            {
                //if (_m_actorInfoList[i] == null || _m_actorInfoList[i].hasDead)
                //    continue;
                if (i < _m_infoItemList.Count)
                {
                    item = _m_infoItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoTBSCharacterActionItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_infoItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_m_actorInfoList[i]);
                item.ShowPanel();

                count++;
            }
            //隐藏多余的
            for (i = count; i < _m_infoItemList.Count; i++)
            {
                item = _m_infoItemList[i];
                if (item == null)
                    continue;
                item.HidePanel();
            }
        }
        private void onTBSActorChg()
        {
            if (_m_actorInfoList == null || _m_actorInfoList.Count == 0)
                return;
            SetInfoList(_m_actorInfoList);
        }

        private void onTBSActorDie(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            List<TBSActorInfo> ready2RemoveInfoList = new List<TBSActorInfo>();
            int tmpCount = _m_actorInfoList.Count;
            for(int i =0;i< tmpCount; i++)
            {
                if (_m_actorInfoList[i].hasDead && _m_actorInfoList[i].isEnemy)
                    ready2RemoveInfoList.Add(_m_actorInfoList[i]);
            }
            for(int i =0;i<ready2RemoveInfoList.Count;i++)
            {
                _m_actorInfoList.Remove(ready2RemoveInfoList[i]);
            }
            refreshShow();
        }
    }
}
