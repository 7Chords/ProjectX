using GameCore.RefData;
using SCFrame;
using SCFrame.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelStore : _ASCUIPanelBase<UIMonoStore>
    {
        private UIPanelStoreContainer _m_itemContainer;//µÀ¾ßcontainer
        private List<ItemData> _m_itemDataList;
        private int _m_curSelectItemIdx;

        private StoreRefObj _m_storeRefObj;
        public UIPanelStore(UIMonoStore _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            if (mono.monoContainer != null)
                _m_itemContainer = new UIPanelStoreContainer(mono.monoContainer);
        }

        public override void BeforeDiscard()
        {
            if (_m_itemContainer != null)
                _m_itemContainer.Discard();
        }

        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_STORE_HIGHLIGHT_UP, onOWItemHighLightUp);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_STORE_HIGHLIGHT_DOWN, onOWItemHighLightDown);
            SCMsgCenter.UnregisterMsg(SCMsgConst.OW_ITEM_MOUSE_HIGHLIGHT, onOWItemMouseHighLight);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_PURCHASE_ITEM, onOWPurchaseItem);

            if (_m_itemContainer != null)
                _m_itemContainer.HidePanel();
        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_STORE_HIGHLIGHT_UP, onOWItemHighLightUp);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_STORE_HIGHLIGHT_DOWN, onOWItemHighLightDown);
            SCMsgCenter.RegisterMsg(SCMsgConst.OW_ITEM_MOUSE_HIGHLIGHT, onOWItemMouseHighLight);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_PURCHASE_ITEM, onOWPurchaseItem);


            _m_itemDataList = SCDataMgr.instance.storeDataDict[_m_storeRefObj.id].dataList;
            _m_curSelectItemIdx = 0;

            if (_m_itemContainer != null)
            {
                _m_itemContainer.ShowPanel();
                if (_m_itemDataList == null)
                    return;
                _m_itemContainer.SetListInfo(_m_itemDataList, _m_curSelectItemIdx);

            }

            refreshPanel();
        }

        public void SetInfo(long _storeId)
        {
            StoreRefObj storeRefObj = SCRefDataMgr.instance.storeRefList.refDataList.Find(x => x.id == _storeId);
            if (storeRefObj == null)
                return;
            _m_storeRefObj = storeRefObj;
        }


        private void refreshPanel()
        {
            refreshHasItemShow();
            refreshItemContainer();
            refreshInfo();
        }
        private void refreshHasItemShow()
        {
            bool hasItem = _m_itemDataList != null && _m_itemDataList.Count > 0;
            SCCommon.SetGameObjectEnable(mono.goHasItemShowList, hasItem);
            SCCommon.SetGameObjectEnable(mono.goNoItemShowList, !hasItem);

        }
        private void refreshItemContainer()
        {
            if (_m_itemDataList == null)
                return;
            _m_itemContainer.RefreshContainerShow(_m_itemDataList, _m_curSelectItemIdx);
        }

        private void refreshInfo()
        {
            if (_m_itemDataList == null || _m_curSelectItemIdx < 0 || _m_curSelectItemIdx >= _m_itemDataList.Count)
                return;
            ItemData itemData = _m_itemDataList[_m_curSelectItemIdx];
            ItemRefObj itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == itemData.itemId);
            if (itemRefObj == null)
                return;
            mono.txtItemDesc.text = GameCommon.GetItemDescTranslate(itemRefObj.id);
            mono.txtStoreName.text = _m_storeRefObj.storeName;
            mono.txtMoney.text = LanguageHelper.instance.GetTextTranslate("#2_money_value", SCDataMgr.instance.money);
        }

        private void onOWItemHighLightUp()
        {
            _m_curSelectItemIdx = Mathf.Max(_m_curSelectItemIdx - 1, 0);
            refreshPanel();
        }

        private void onOWItemHighLightDown()
        {
            _m_curSelectItemIdx = Mathf.Min(_m_curSelectItemIdx + 1, _m_itemDataList.Count - 1);
            refreshPanel();
        }

        private void onOWItemMouseHighLight(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            long itemId = (long)_objs[0];
            for (int i = 0; i < _m_itemDataList.Count; i++)
            {
                if (_m_itemDataList[i].itemId == itemId)
                {
                    _m_curSelectItemIdx = i;

                    break;
                }
            }
            refreshPanel();
        }

        private void onOWPurchaseItem()
        {
            refreshPanel();
        }
    }

}


