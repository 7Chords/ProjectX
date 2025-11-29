using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSSkillContainer : UIPanelContainerBase<UIMonoCommonContainer,UIPanelTBSSkillContainerItem,UIMonoTBSSkillContainerItem>
    {

        private List<UIPanelTBSSkillContainerItem> _m_skillItemList;//item列表
        public UIPanelTBSSkillContainer(UIMonoCommonContainer _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }

        protected override UIPanelTBSSkillContainerItem creatItemPanel(UIMonoTBSSkillContainerItem _mono)
        {
            return new UIPanelTBSSkillContainerItem(_mono,SCUIShowType.INTERNAL);
        }
        protected override GameObject creatItemGO()
        {
            return ResourcesHelper.LoadGameObject(mono.prefabItemObjName);
        }

        public override void BeforeDiscard()
        {
            if (_m_skillItemList != null)
            {
                foreach (var item in _m_skillItemList)
                    item.Discard();
            }
            _m_skillItemList.Clear();
            _m_skillItemList = null;
        }

        public override void AfterInitialize()
        {
            _m_skillItemList = new List<UIPanelTBSSkillContainerItem>();
        }
        public override void OnHidePanel()
        {
            if (_m_skillItemList != null)
            {
                foreach (var item in _m_skillItemList)
                    item.HidePanel();
            }
        }

        public override void OnShowPanel()
        {

        }

        public void SetListInfo(List<long> _skillList,int _selectIndex)
        {
            if (_skillList == null)
                return;
            if (_m_skillItemList == null)
                return;

            int i = 0, count = 0;
            UIPanelTBSSkillContainerItem item = null;
            for(i = 0;i< _skillList.Count;i++)
            {
                if(i< _m_skillItemList.Count)
                {
                    item = _m_skillItemList[i];
                }
                else
                {
                    GameObject itemGO = creatItemGO();
                    item = creatItemPanel(itemGO.GetComponent<UIMonoTBSSkillContainerItem>());
                    itemGO.transform.SetParent(mono.layoutGroup.transform);
                    _m_skillItemList.Add(item);
                }
                if (item == null)
                    continue;
                item.SetInfo(_skillList[i]);
                item.ShowPanel();
                //设置技能是否选中
                if (i == _selectIndex)
                    item.SetSelect(true);
                else
                    item.SetSelect(false);

                count++;
            }
            //隐藏多余的
            for(i=count;i< _m_skillItemList.Count;i++)
            {
                item = _m_skillItemList[i];
                if (item == null)
                    continue;
                item.HidePanel();
            }

        }

    }
}
