using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelCharacterHeaderContainer : UIPanelContainerBase<UIMonoCommonContainer, UIPanelCharacterHeaderContainerItem, UIMonoCharacterHeaderContainerItem>
    {
        private List<UIPanelCharacterHeaderContainerItem> _m_headerItemList;//item列表

        public UIPanelCharacterHeaderContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            _m_headerItemList = new List<UIPanelCharacterHeaderContainerItem>();

        }

        public override void BeforeDiscard()
        {
            if (_m_headerItemList != null)
            {
                foreach (var item in _m_headerItemList)
                    item.Discard();
            }
            _m_headerItemList.Clear();
            _m_headerItemList = null;
        }

        public override void OnHidePanel()
        {
            if (_m_headerItemList != null)
            {
                foreach (var item in _m_headerItemList)
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

        protected override UIPanelCharacterHeaderContainerItem creatItemPanel(UIMonoCharacterHeaderContainerItem _mono)
        {
            return new UIPanelCharacterHeaderContainerItem(_mono, SCUIShowType.INTERNAL);
        }

        public void SetListInfo(List<TBSActorInfo> _infoList, int _selectIndex)
        {
            if (_infoList == null)
                return;
            if (_m_headerItemList == null)
                return;

            int i = 0, count = 0;
            UIPanelCharacterHeaderContainerItem item = null;
            for (i = 0; i < _infoList.Count; i++)
            {
                if (i < _m_headerItemList.Count)
                {
                    item = _m_headerItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoCharacterHeaderContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_headerItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.ShowPanel();
                item.SetInfo(_infoList[i]);
                //设置技能是否选中
                if (i == _selectIndex)
                    item.SetSelect(true);
                else
                    item.SetSelect(false);

                count++;
            }
            //隐藏多余的
            for (i = count; i < _m_headerItemList.Count; i++)
            {
                item = _m_headerItemList[i];
                if (item == null)
                    continue;
                item.HidePanel();
            }
        }

        public void RefreshContainerShow(List<TBSActorInfo> _infoList, int _selectIndex)
        {
            int i = 0;
            UIPanelCharacterHeaderContainerItem item = null;
            for (i = 0; i < _infoList.Count; i++)
            {
                if (i < _m_headerItemList.Count)
                {
                    item = _m_headerItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoCharacterHeaderContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_headerItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_infoList[i]);
                //设置技能是否选中
                if (i == _selectIndex)
                    item.SetSelect(true);
                else
                    item.SetSelect(false);
            }
        }
    }
}
