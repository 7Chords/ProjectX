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

        private int _m_curSelectItemIdx;
        private int _m_curActorSkillCount;
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
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP, onTBSActorItemHighLightUp);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN, onTBSActorItemHighLightDown);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_SKILL_MOUSE_HIGHLIGHT, onTBSActorItemMouseHighLight);

            if (_m_itemContainer != null)
                _m_itemContainer.Discard();
        }

        public override void OnHidePanel()
        {
            if (_m_itemContainer != null)
                _m_itemContainer.HidePanel();


            GameCoreMgr.instance.uiCoreMgr.ShowNode(nameof(UINodeTBSEnemyHud));

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
            _m_curSelectItemIdx = 0;

            refreshPanel();

            //隐藏敌人hud
            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));

            //隐藏光标
            TBSCursorMgr.instance.HideSelectionCursor();
        }

        private void refreshPanel()
        {
            refreshItemContainer();
            refreshCurItemDesc();
        }

        private void refreshItemContainer()
        {
 
        }

        private void refreshCurItemDesc()
        {

        }

        private void onTBSActorItemHighLightUp()
        {
            _m_curSelectItemIdx = Mathf.Max(_m_curSelectItemIdx - 1, 0);
            refreshPanel();
        }

        private void onTBSActorItemHighLightDown()
        {
            _m_curSelectItemIdx = Mathf.Min(_m_curSelectItemIdx + 1, _m_curActorSkillCount - 1);
            refreshPanel();
        }

        private void onTBSActorItemMouseHighLight(object[] _objs)
        {

        }
    }
}
