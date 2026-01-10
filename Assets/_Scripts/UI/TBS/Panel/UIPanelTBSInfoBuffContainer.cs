using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSInfoBuffContainer : UIPanelContainerBase<UIMonoCommonContainer, UIPanelTBSInfoBuffContainerItem, UIMonoTBSInfoBuffContainerItem>
    {
        private List<UIPanelTBSInfoBuffContainerItem> _m_infoBuffItemList;//item列表

        public UIPanelTBSInfoBuffContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }
        protected override GameObject creatItemGO()
        {
            return ResourcesHelper.LoadGameObject(mono.prefabItemObjName);
        }

        protected override UIPanelTBSInfoBuffContainerItem creatItemPanel(UIMonoTBSInfoBuffContainerItem _mono)
        {
            return new UIPanelTBSInfoBuffContainerItem(_mono, SCUIShowType.INTERNAL);
        }
        public override void AfterInitialize()
        {
            _m_infoBuffItemList = new List<UIPanelTBSInfoBuffContainerItem>();
        }

        public override void BeforeDiscard()
        {
            if (_m_infoBuffItemList != null)
            {
                foreach (var item in _m_infoBuffItemList)
                    item.Discard();
            }
            _m_infoBuffItemList.Clear();
            _m_infoBuffItemList = null;
        }

        public override void OnHidePanel()
        {
            if (_m_infoBuffItemList != null)
            {
                foreach (var item in _m_infoBuffItemList)
                    item.HidePanel();
            }
        }

        public override void OnShowPanel()
        {
        }

        public void SetListInfo(List<TBSGameBuffInfo> _infoList)
        {
            if (_infoList == null)
                return;
            if (_m_infoBuffItemList == null)
                return;

            int i = 0, count = 0;
            UIPanelTBSInfoBuffContainerItem item = null;
            for (i = 0; i < _infoList.Count; i++)
            {
                if (i < _m_infoBuffItemList.Count)
                {
                    item = _m_infoBuffItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoTBSInfoBuffContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_infoBuffItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_infoList[i]);
                item.ShowPanel();
                count++;
            }
            //隐藏多余的
            for (i = count; i < _m_infoBuffItemList.Count; i++)
            {
                item = _m_infoBuffItemList[i];
                if (item == null)
                    continue;
                item.HidePanel();
            }

        }

        public void RefreshContainerShow(List<TBSGameBuffInfo> _buffInfoList)
        {
            int i = 0;
            UIPanelTBSInfoBuffContainerItem item = null;
            for (i = 0; i < _buffInfoList.Count; i++)
            {
                if (i < _m_infoBuffItemList.Count)
                {
                    item = _m_infoBuffItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoTBSInfoBuffContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_infoBuffItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_buffInfoList[i]);
            }
        }

    }
}

