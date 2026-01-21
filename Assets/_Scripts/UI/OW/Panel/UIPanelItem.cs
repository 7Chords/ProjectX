using GameCore.RefData;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelItem : _ASCUIPanelBase<UIMonoItem>
    {
        private UIPanelItemContainer _m_itemContainer;//µÀ¾ßcontainer
        private List<ItemData> _m_itemDataList;
        private int _m_curSelectItemIdx;
        public UIPanelItem(UIMonoItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            if (mono.monoContainer != null)
                _m_itemContainer = new UIPanelItemContainer(mono.monoContainer);
        }

        public override void BeforeDiscard()
        {

            if (_m_itemContainer != null)
                _m_itemContainer.Discard();
        }

        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_SWITCH_TO_UP_INPUT, onOWItemHighLightUp);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_SWITCH_TO_DOWN_INPUT, onOWItemHighLightDown);
            SCMsgCenter.UnregisterMsg(SCMsgConst.OW_ITEM_MOUSE_HIGHLIGHT, onOWItemMouseHighLight);

            if (_m_itemContainer != null)
                _m_itemContainer.HidePanel();
        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_UP_INPUT, onOWItemHighLightUp);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_DOWN_INPUT, onOWItemHighLightDown);
            SCMsgCenter.RegisterMsg(SCMsgConst.OW_ITEM_MOUSE_HIGHLIGHT, onOWItemMouseHighLight);


            _m_itemDataList = SCDataMgr.instance.itemDataList;

            setSelectItemIdx();

            if (_m_itemContainer != null)
            {
                _m_itemContainer.ShowPanel();
                if (_m_itemDataList == null)
                    return;
                _m_itemContainer.SetListInfo(_m_itemDataList, _m_curSelectItemIdx);

            }

            refreshPanel();
        }
        private void refreshPanel()
        {
            refreshHasItemShow();
            refreshItemContainer();
            refreshCurItemDesc();
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

        private void refreshCurItemDesc()
        {
            if (_m_itemDataList == null || _m_curSelectItemIdx < 0 || _m_curSelectItemIdx >= _m_itemDataList.Count)
                return;
            ItemData itemData = _m_itemDataList[_m_curSelectItemIdx];
            ItemRefObj itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == itemData.itemId);
            if (itemRefObj == null)
                return;
            mono.txtItemDesc.text = GameCommon.GetItemDescTranslate(itemRefObj.id);
        }

        private void onOWItemHighLightUp()
        {
            _m_curSelectItemIdx = Mathf.Max(_m_curSelectItemIdx - 1, 0);
            SCModel.instance.tbsModel.curSelectItemIdx = _m_curSelectItemIdx;
            refreshPanel();
        }

        private void onOWItemHighLightDown()
        {
            _m_curSelectItemIdx = Mathf.Min(_m_curSelectItemIdx + 1, _m_itemDataList.Count - 1);
            SCModel.instance.tbsModel.curSelectItemIdx = _m_curSelectItemIdx;
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
                    SCModel.instance.tbsModel.curSelectItemIdx = _m_curSelectItemIdx;

                    break;
                }
            }
            refreshPanel();
        }

        private void setSelectItemIdx()
        {
            if (_m_itemDataList == null || _m_itemDataList.Count == 0)
                return;
            int itemTypeCount = _m_itemDataList.Count;

            if (itemTypeCount <= SCModel.instance.tbsModel.curSelectItemIdx)
            {
                _m_curSelectItemIdx = itemTypeCount - 1;
                SCModel.instance.tbsModel.curSelectItemIdx = _m_curSelectItemIdx;
            }
            else
                _m_curSelectItemIdx = SCModel.instance.tbsModel.curSelectItemIdx;
        }
    }
}
