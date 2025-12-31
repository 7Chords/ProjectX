using DG.Tweening;
using GameCore.RefData;
using GameCore.TBS;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSSkill : _ASCUIPanelBase<UIMonoTBSSkill>
    { 
        private UIPanelTBSSkillContainer _m_skillContainer;//技能container

        private int _m_curSelectSkillIdx;
        private int _m_curActorSkillCount;
        public UIPanelTBSSkill(UIMonoTBSSkill _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void BeforeDiscard()
        {
            //SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP, onTBSActorSkillHighLightUp);
            //SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN, onTBSActorSkillHighLightDown);
            //SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_SKILL_MOUSE_HIGHLIGHT, onTBSActorSkillMouseHighLight);
            //SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_CONFIRM_RELEASE, onTBSActorSkillRelease);

            if (_m_skillContainer != null)
                _m_skillContainer.Discard();

        }

        public override void AfterInitialize()
        {
            //SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP, onTBSActorSkillHighLightUp);
            //SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN, onTBSActorSkillHighLightDown);
            //SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_SKILL_MOUSE_HIGHLIGHT, onTBSActorSkillMouseHighLight);
            //SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_CONFIRM_RELEASE, onTBSActorSkillRelease);

            if (mono.monoContainer != null)
                _m_skillContainer = new UIPanelTBSSkillContainer(mono.monoContainer);
        }


        public override void OnHidePanel()
        {

            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP, onTBSActorSkillHighLightUp);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN, onTBSActorSkillHighLightDown);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_SKILL_MOUSE_HIGHLIGHT, onTBSActorSkillMouseHighLight);



            if (_m_skillContainer != null)
                _m_skillContainer.HidePanel();


            GameCoreMgr.instance.uiCoreMgr.ShowNodeButNotMove2Top(nameof(UINodeTBSEnemyHud));

            //重新设置光标
            List<Vector3> worldPosList = new List<Vector3>();
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                worldPosList.Add(SCModel.instance.tbsModel.GetCurSelectSingleEnemyTargetActor().GetCursorPos());
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

            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP, onTBSActorSkillHighLightUp);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN, onTBSActorSkillHighLightDown);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_SKILL_MOUSE_HIGHLIGHT, onTBSActorSkillMouseHighLight);

            setSelectSkillIdx();

            refreshPanel();

            //隐藏敌人hud
            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));

            //隐藏光标
            TBSCursorMgr.instance.HideSelectionCursor();
        }


        private void refreshPanel()
        {
            refreshSkillContainer();
            refreshCurSkillDesc();
        }

        private void refreshSkillContainer()
        {
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
            if (actorInfo == null)
                return;
            _m_curActorSkillCount = actorInfo.skillList.Count;
            _m_skillContainer.SetListInfo(actorInfo.skillList, _m_curSelectSkillIdx);
            _m_skillContainer.ShowPanel();
        }

        private void refreshCurSkillDesc()
        {
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
            if (actorInfo == null)
                return;
            long curSkillId = actorInfo.skillList[_m_curSelectSkillIdx];

            mono.txtSkillDesc.text = GameCommon.GetSkillDescTranslate(curSkillId);
        }


        private void onTBSActorSkillHighLightUp()
        {
            _m_curSelectSkillIdx = Mathf.Max(_m_curSelectSkillIdx - 1, 0);
            SCModel.instance.tbsModel.curSelectSkillIdx = _m_curSelectSkillIdx;
            refreshPanel();
        }

        private void onTBSActorSkillHighLightDown()
        {
            _m_curSelectSkillIdx = Mathf.Min(_m_curSelectSkillIdx + 1, _m_curActorSkillCount - 1);
            SCModel.instance.tbsModel.curSelectSkillIdx = _m_curSelectSkillIdx;
            refreshPanel();
        }
        private void onTBSActorSkillMouseHighLight(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            long skillId = (long)_objs[0];
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
            if (actorInfo == null)
                return;
            for(int i =0;i< actorInfo.skillList.Count;i++)
            {
                if (actorInfo.skillList[i] == skillId)
                {
                    _m_curSelectSkillIdx = i;
                    SCModel.instance.tbsModel.curSelectSkillIdx = _m_curSelectSkillIdx;
                    break;
                }
            }
            refreshPanel();
        }

        private void setSelectSkillIdx()
        {

            //tip：重新打开这个面板要恢复成之前选择的位置 如果这个角色技能数量不支持 就恢复为最后一个技能
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
            if (actorInfo == null)
                return;
            _m_curActorSkillCount = actorInfo.skillList.Count;

            if (_m_curActorSkillCount <= SCModel.instance.tbsModel.curSelectSkillIdx)
            {
                _m_curSelectSkillIdx = _m_curActorSkillCount - 1;
                SCModel.instance.tbsModel.curSelectSkillIdx = _m_curSelectSkillIdx;
            }
            else
                _m_curSelectSkillIdx = SCModel.instance.tbsModel.curSelectSkillIdx;
        }
    }
}
