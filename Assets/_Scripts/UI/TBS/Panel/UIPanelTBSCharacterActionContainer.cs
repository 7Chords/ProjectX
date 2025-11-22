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

        public override void OnDiscard()
        {
            if (_m_infoItemList != null)
            {
                foreach (var item in _m_infoItemList)
                    item.Discard();
            }
            _m_infoItemList.Clear();
            _m_infoItemList = null;
        }

        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_CHG, onTBSActorChg);

            if (_m_infoItemList != null)
            {
                foreach (var item in _m_infoItemList)
                    item.HidePanel();
            }
        }

        public override void OnInitialize()
        {
            _m_infoItemList = new List<UIPanelTBSCharacterActionItem>();

        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_CHG, onTBSActorChg);

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

            int i = 0, count = 0;
            UIPanelTBSCharacterActionItem item = null;
            for (i = 0; i < _m_actorInfoList.Count; i++)
            {
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
    }
}
