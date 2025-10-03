using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSInfoContainer : UIPanelContainerBase<UIMonoCommonContainer, UIPanelTBSInfoContainerItem, UIMonoTBSInfoContainerItem>
    {

        private List<UIPanelTBSInfoContainerItem> _m_infoItemList;//item列表

        public UIPanelTBSInfoContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }


        protected override GameObject creatItemGO()
        {
            return ResourcesHelper.LoadGameObject(mono.prefabItemObjName);
        }

        protected override UIPanelTBSInfoContainerItem creatItemPanel(UIMonoTBSInfoContainerItem _mono)
        {
            return new UIPanelTBSInfoContainerItem(_mono, SCUIShowType.INTERNAL);
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
            if (_m_infoItemList != null)
            {
                foreach (var item in _m_infoItemList)
                    item.HidePanel();
            }
        }

        public override void OnInitialize()
        {
            _m_infoItemList = new List<UIPanelTBSInfoContainerItem>();

        }

        public override void OnShowPanel()
        {

        }


        public void SetListInfo(List<TBSActorInfo> _actorInfoList)
        {
            if (_actorInfoList == null)
                return;
            if (_m_infoItemList == null)
                return;

            int i = 0, count = 0;
            UIPanelTBSInfoContainerItem item = null;
            for (i = 0; i < _actorInfoList.Count; i++)
            {
                if (i < _m_infoItemList.Count)
                {
                    item = _m_infoItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoTBSInfoContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_infoItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_actorInfoList[i]);
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
    }
}
