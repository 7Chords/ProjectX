using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelItemContainer : UIPanelContainerBase<UIMonoCommonContainer, UIPanelItemContainerItem, UIMonoItemContainerItem>
    {
        private List<UIPanelItemContainerItem> _m_itemItemList;//item列表

        public UIPanelItemContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            _m_itemItemList = new List<UIPanelItemContainerItem>();

        }

        public override void BeforeDiscard()
        {
            if (_m_itemItemList != null)
            {
                foreach (var item in _m_itemItemList)
                    item.Discard();
            }
            _m_itemItemList.Clear();
            _m_itemItemList = null;
        }

        public override void OnHidePanel()
        {
            if (_m_itemItemList != null)
            {
                foreach (var item in _m_itemItemList)
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

        protected override UIPanelItemContainerItem creatItemPanel(UIMonoItemContainerItem _mono)
        {
            return new UIPanelItemContainerItem(_mono, SCUIShowType.INTERNAL);
        }
        public void SetListInfo(List<ItemData> _itemDataList, int _selectIndex)
        {
            if (_itemDataList == null)
                return;
            if (_m_itemItemList == null)
                return;

            int i = 0, count = 0;
            UIPanelItemContainerItem item = null;
            for (i = 0; i < _itemDataList.Count; i++)
            {
                if (i < _m_itemItemList.Count)
                {
                    item = _m_itemItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoItemContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_itemItemList.Add(item);
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
            for (i = count; i < _m_itemItemList.Count; i++)
            {
                item = _m_itemItemList[i];
                if (item == null)
                    continue;
                item.HidePanel();
            }

        }

        public void RefreshContainerShow(List<ItemData> _itemDataList, int _selectIndex)
        {
            int i = 0;
            UIPanelItemContainerItem item = null;
            for (i = 0; i < _itemDataList.Count; i++)
            {
                if (i < _m_itemItemList.Count)
                {
                    item = _m_itemItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoItemContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_itemItemList.Add(item);
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
