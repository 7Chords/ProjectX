using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSDetailBuffContainer : UIPanelContainerBase<UIMonoCommonContainer, UIPanelTBSDetailBuffContainerItem, UIMonoTBSDetailBuffContainerItem>
    {
        private List<UIPanelTBSDetailBuffContainerItem> _m_buffItemList;//item列表

        public UIPanelTBSDetailBuffContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            _m_buffItemList = new List<UIPanelTBSDetailBuffContainerItem>();
        }

        public override void BeforeDiscard()
        {
            if (_m_buffItemList != null)
            {
                foreach (var item in _m_buffItemList)
                    item.Discard();
            }
            _m_buffItemList.Clear();
            _m_buffItemList = null;
        }

        public override void OnHidePanel()
        {
            if (_m_buffItemList != null)
            {
                foreach (var item in _m_buffItemList)
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

        protected override UIPanelTBSDetailBuffContainerItem creatItemPanel(UIMonoTBSDetailBuffContainerItem _mono)
        {
            return new UIPanelTBSDetailBuffContainerItem(_mono, SCUIShowType.INTERNAL);
        }

        public void SetListInfo(List<TBSGameBuffInfo> _infoList)
        {
            if (_infoList == null)
                return;
            if (_m_buffItemList == null)
                return;

            int i = 0, count = 0;
            UIPanelTBSDetailBuffContainerItem item = null;
            for (i = 0; i < _infoList.Count; i++)
            {
                if (i < _m_buffItemList.Count)
                {
                    item = _m_buffItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoTBSDetailBuffContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_buffItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_infoList[i]);
                item.ShowPanel();
                count++;
            }
            //隐藏多余的
            for (i = count; i < _m_buffItemList.Count; i++)
            {
                item = _m_buffItemList[i];
                if (item == null)
                    continue;
                item.HidePanel();
            }

        }
    }
}
