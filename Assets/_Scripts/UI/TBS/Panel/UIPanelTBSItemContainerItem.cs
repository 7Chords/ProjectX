using SCFrame.UI;


namespace GameCore.UI
{
    public class UIPanelTBSItemContainerItem : _ASCUIPanelBase<UIMonoTBSItemContainerItem>
    {

        private bool _m_isSelect;
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

        public void SetInfo(long _itemId)
        {

        }

        public void SetSelect(bool _isSelect)
        {
            mono.imgItem.color = _isSelect ? mono.colorItemSelect : mono.colorItemUnSelect;
            _m_isSelect = _isSelect;
        }
    }
}
