using GameCore.RefData;
using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSItem : _ASCUIPanelBase<UIMonoTBSItem>
    {
        private UIPanelTBSItemContainer _m_itemContainer;//道具container
        private List<ItemData> _m_itemDataList;
        private int _m_curSelectItemIdx;
        public UIPanelTBSItem(UIMonoTBSItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void AfterInitialize()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_ITEM_HIGHTLIGHT_UP, onTBSActorItemHighLightUp);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_ITEM_HIGHTLIGHT_DOWN, onTBSActorItemHighLightDown);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_ITEM_MOUSE_HIGHLIGHT, onTBSActorItemMouseHighLight);

            if (mono.monoContainer != null)
                _m_itemContainer = new UIPanelTBSItemContainer(mono.monoContainer);
        }

        public override void BeforeDiscard()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_ITEM_HIGHTLIGHT_UP, onTBSActorItemHighLightUp);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_ITEM_HIGHTLIGHT_DOWN, onTBSActorItemHighLightDown);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_ITEM_MOUSE_HIGHLIGHT, onTBSActorItemMouseHighLight);

            if (_m_itemContainer != null)
                _m_itemContainer.Discard();
        }

        public override void OnHidePanel()
        {
            if (_m_itemContainer != null)
                _m_itemContainer.HidePanel();


            GameCoreMgr.instance.uiCoreMgr.ShowNodeButNotMove2Top(nameof(UINodeTBSEnemyHud));

            //重新设置光标
            List<Vector3> worldPosList = new List<Vector3>();
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                worldPosList.Add(SCModel.instance.tbsModel.GetCurSingleSelectTargetActor().GetCursorPos());
            else if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
            {
                foreach (var module in SCModel.instance.tbsModel.enemyActorModuleList)
                {
                    worldPosList.Add(module.GetCursorPos());
                }
            }
            TBSCursorMgr.instance.SetSelectionCursor(worldPosList);
        }

        public override void OnShowPanel()
        {
            _m_itemDataList = SCDataMgr.instance.itemDataList;

            _m_curSelectItemIdx = 0;

            refreshPanel();

            //隐藏敌人hud
            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));

            //隐藏光标
            TBSCursorMgr.instance.HideSelectionCursor();
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
            _m_itemContainer.SetListInfo(_m_itemDataList, _m_curSelectItemIdx);
            _m_itemContainer.ShowPanel();
        }

        private void refreshCurItemDesc()
        {
            if (_m_itemDataList == null || _m_curSelectItemIdx < 0 || _m_curSelectItemIdx>= _m_itemDataList.Count)
                return;
            ItemData itemData = _m_itemDataList[_m_curSelectItemIdx];
            ItemRefObj itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == itemData.itemId);
            if (itemRefObj == null)
                return;
            mono.txtItemDesc.text = GameCommon.GetItemDescTranslate(itemRefObj.id);
        }

        private void onTBSActorItemHighLightUp()
        {
            _m_curSelectItemIdx = Mathf.Max(_m_curSelectItemIdx - 1, 0);
            SCModel.instance.tbsModel.curSelectItemIdx = _m_curSelectItemIdx;
            refreshPanel();
        }

        private void onTBSActorItemHighLightDown()
        {
            _m_curSelectItemIdx = Mathf.Min(_m_curSelectItemIdx + 1, _m_itemDataList.Count - 1);
            SCModel.instance.tbsModel.curSelectItemIdx = _m_curSelectItemIdx;
            refreshPanel();
        }

        private void onTBSActorItemMouseHighLight(object[] _objs)
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
    }
}
