using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.UI
{
    public class UIPanelStoreContainer : UIPanelContainerBase<UIMonoCommonContainer, UIPanelStoreContainerItem, UIMonoStoreContainerItem>
    {
        private List<UIPanelStoreContainerItem> _m_goodItemList;//item列表
        public UIPanelStoreContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            _m_goodItemList = new List<UIPanelStoreContainerItem>();
        }

        public override void BeforeDiscard()
        {
            if (_m_goodItemList != null)
            {
                foreach (var item in _m_goodItemList)
                    item.Discard();
            }
            _m_goodItemList.Clear();
            _m_goodItemList = null;
        }

        public override void OnHidePanel()
        {
            if (_m_goodItemList != null)
            {
                foreach (var item in _m_goodItemList)
                    item.HidePanel();
            }
        }

        public override void OnShowPanel()
        {
        }

        protected override GameObject creatItemGO()
        {
            return ResourcesHelper.LoadGameObject(mono.prefabItemObjName);
        }

        protected override UIPanelStoreContainerItem creatItemPanel(UIMonoStoreContainerItem _mono)
        {
            return new UIPanelStoreContainerItem(_mono, SCUIShowType.INTERNAL);
        }

        public void SetListInfo(List<ItemData> _itemDataList, int _selectIndex)
        {
            if (_itemDataList == null)
                return;
            if (_m_goodItemList == null)
                return;

            int i = 0, count = 0;
            UIPanelStoreContainerItem item = null;
            for (i = 0; i < _itemDataList.Count; i++)
            {
                if (i < _m_goodItemList.Count)
                {
                    item = _m_goodItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoStoreContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_goodItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_itemDataList[i]);
                item.ShowPanel();
                //设置技能是否选中
                if (i == _selectIndex)
                    item.SetSelect(true);
                else
                    item.SetSelect(false);

                count++;
            }
            //隐藏多余的
            for (i = count; i < _m_goodItemList.Count; i++)
            {
                item = _m_goodItemList[i];
                if (item == null)
                    continue;
                item.HidePanel();
            }

        }

        public void RefreshContainerShow(List<ItemData> _itemDataList, int _selectIndex)
        {
            int i = 0;
            UIPanelStoreContainerItem item = null;
            for (i = 0; i < _itemDataList.Count; i++)
            {
                if (i < _m_goodItemList.Count)
                {
                    item = _m_goodItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoStoreContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_goodItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_itemDataList[i]);
                //设置技能是否选中
                if (i == _selectIndex)
                    item.SetSelect(true);
                else
                    item.SetSelect(false);
            }
        }
    }
}
