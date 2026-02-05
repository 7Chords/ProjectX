using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSWinExpContainer : UIPanelContainerBase<UIMonoCommonContainer, UIPanelTBSWinExpItem, UIMonoTBSWinExpItem>
    {
        private List<UIPanelTBSWinExpItem> _m_winExpItemList;//item列表

        public UIPanelTBSWinExpContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            _m_winExpItemList = new List<UIPanelTBSWinExpItem>();
        }

        public override void BeforeDiscard()
        {
            foreach (var item in _m_winExpItemList)
                item?.Discard();
        }

        public override void OnHidePanel()
        {
            foreach (var item in _m_winExpItemList)
                item?.HidePanel();
        }
        public override void OnShowPanel()
        {
            foreach (var item in _m_winExpItemList)
                item?.ShowPanel();
        }

        protected override GameObject creatItemGO()
        {
            return ResourcesHelper.LoadGameObject(mono.prefabItemObjName);
        }

        protected override UIPanelTBSWinExpItem creatItemPanel(UIMonoTBSWinExpItem _mono)
        {
            return new UIPanelTBSWinExpItem(_mono, SCUIShowType.INTERNAL);

        }

        public void SetListInfo(List<TBSActorInfo> _skillList,List<bool> _hasLevelUpStateList)
        {
            if (_skillList == null || _hasLevelUpStateList == null)
                return;
            if (_m_winExpItemList == null)
                return;

            int i = 0, count = 0;
            UIPanelTBSWinExpItem item = null;
            for (i = 0; i < _skillList.Count; i++)
            {
                if (i < _m_winExpItemList.Count)
                {
                    item = _m_winExpItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoTBSWinExpItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_winExpItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_skillList[i], _hasLevelUpStateList[i]);
                item.ShowPanel();

                count++;
            }
            //隐藏多余的
            for (i = count; i < _m_winExpItemList.Count; i++)
            {
                item = _m_winExpItemList[i];
                if (item == null)
                    continue;
                item.HidePanel();
            }

        }
    }
}
