using GameCore.RefData;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSItemContainerItem : _ASCUIPanelBase<UIMonoTBSItemContainerItem>
    {

        private bool _m_isSelect;

        private ItemData _m_itemData;
        private ItemRefObj _m_itemRefObj;
        public UIPanelTBSItemContainerItem(UIMonoTBSItemContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {

        }

        public override void BeforeDiscard()
        {

        }

        public override void OnHidePanel()
        {

        }

        public override void OnShowPanel()
        {

        }

        public void SetInfo(ItemData _itemData)
        {
            if (_itemData == null)
                return;
            _m_itemData = _itemData;
            _m_itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == _m_itemData.itemId);
            refreshShow();
        }

        private void refreshShow()
        {
            if (_m_itemData == null || _m_itemRefObj == null)
                return;
            mono.txtItemName.text = LanguageHelper.instance.GetTextTranslate(_m_itemRefObj.itemName);
            mono.imgItemIcon.sprite = ResourcesHelper.LoadAsset<Sprite>(_m_itemRefObj.itemIconObjName);
            mono.txtItemRemain.text = LanguageHelper.instance.GetTextTranslate("#1_*{0}", _m_itemData.itemAmount);
        }

        public void SetSelect(bool _isSelect)
        {
            mono.imgItem.color = _isSelect ? mono.colorItemSelect : mono.colorItemUnSelect;
            _m_isSelect = _isSelect;
        }
    }
}
