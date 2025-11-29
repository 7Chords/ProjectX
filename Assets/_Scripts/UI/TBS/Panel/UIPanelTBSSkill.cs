using DG.Tweening;
using GameCore.RefData;
using GameCore.TBS;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSSkill : _ASCUIPanelBase<UIMonoTBSSkill>
    {
        //private TweenContainer _m_tweenContainer;

        private UIPanelTBSSkillContainer _m_skillContainer;//技能container

        private int _m_curSelectSkillIdx;
        private int _m_curActorSkillCount;
        public UIPanelTBSSkill(UIMonoTBSSkill _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void BeforeDiscard()
        {
            //SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_SKILL_SELECT, onTBSActorSkillSelect);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP, onTBSActorSkillHighLightUp);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN, onTBSActorSkillHighLightDown);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_SKILL_MOUSE_HIGHLIGHT, onTBSActorSkillMouseHighLight);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_RELEASE, onTBSActorSkillRelease);

            if (_m_skillContainer != null)
                _m_skillContainer.Discard();

        }

        public override void AfterInitialize()
        {
            //SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_SKILL_SELECT, onTBSActorSkillSelect);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_UP, onTBSActorSkillHighLightUp);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_HIGHTLIGHT_DOWN, onTBSActorSkillHighLightDown);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_SKILL_MOUSE_HIGHLIGHT, onTBSActorSkillMouseHighLight);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_SKILL_RELEASE, onTBSActorSkillRelease);

            if (mono.monoContainer != null)
                _m_skillContainer = new UIPanelTBSSkillContainer(mono.monoContainer);
        }


        public override void OnHidePanel()
        {

            if (_m_skillContainer != null)
                _m_skillContainer.HidePanel();

            GameCoreMgr.instance.uiCoreMgr.ShowNode(nameof(UINodeTBSEnemyHud));
            TBSCursorMgr.instance.ShowSelectionCursor();
        }


        public override void OnShowPanel()
        {

            _m_curSelectSkillIdx = 0;

            _refreshPanel();

            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
            TBSCursorMgr.instance.HideSelectionCursor();
        }


        private void _refreshPanel()
        {
            _refreshSkillContainer();
            _refreshCurSkillDesc();
        }

        private void _refreshSkillContainer()
        {
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.getCurActorInfo();
            if (actorInfo == null)
                return;
            _m_curActorSkillCount = actorInfo.skillList.Count;
            _m_skillContainer.SetListInfo(actorInfo.skillList, _m_curSelectSkillIdx);
            _m_skillContainer.ShowPanel();
        }

        private void _refreshCurSkillDesc()
        {
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.getCurActorInfo();
            if (actorInfo == null)
                return;
            long curSkillId = actorInfo.skillList[_m_curSelectSkillIdx];
            TBSActorSkillRefObj skillRefObj = SCRefDataMgr.instance.tbsActorSkillRefList.refDataList.Find(x => x.id == curSkillId);


            //根据不同的伤害类型这里要做描述特殊处理
            string damageDescStr = "";
            switch (skillRefObj.damageType)
            {
                case EDamageType.MAGIC:
                    damageDescStr = Enum2StrFactory.CreateLocalStrByMagicAttributeEnum(skillRefObj.magicAttributeType);
                    break;
                case EDamageType.PHYSICAL:
                    damageDescStr = Enum2StrFactory.CreateLocalStrByPhysicalLevelEnum(skillRefObj.physicsLevelType);
                    break;
                default:
                    break;
            }

            mono.txtSkillDesc.text = LanguageHelper.instance.GetTextTranslate(
                skillRefObj.skillDesc,
                Enum2StrFactory.CreateLocalStrByDamageTargetEnum(skillRefObj.damageTargetType),
                Enum2StrFactory.CreateLocalStrByDamageAmountEnum(skillRefObj.damageAmountType),
                damageDescStr,
                Enum2StrFactory.CreateLocalStrByDamageEnum(skillRefObj.damageType));
        }


        private void onTBSActorSkillHighLightUp()
        {
            _m_curSelectSkillIdx = Mathf.Max(_m_curSelectSkillIdx - 1, 0);
            _refreshPanel();
        }

        private void onTBSActorSkillHighLightDown()
        {
            _m_curSelectSkillIdx = Mathf.Min(_m_curSelectSkillIdx + 1, _m_curActorSkillCount - 1);
            _refreshPanel();
        }
        private void onTBSActorSkillMouseHighLight(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            long skillId = (long)_objs[0];
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.getCurActorInfo();
            if (actorInfo == null)
                return;
            for(int i =0;i< actorInfo.skillList.Count;i++)
            {
                if (actorInfo.skillList[i] == skillId)
                {
                    _m_curSelectSkillIdx = i;
                    break;
                }
            }
            _refreshPanel();
        }

        //技能释放回调
        private void onTBSActorSkillRelease()
        {

            TBSActorInfo actorInfo = SCModel.instance.tbsModel.getCurActorInfo();
            if (actorInfo == null)
                return;
            long curSkillId = actorInfo.skillList[_m_curSelectSkillIdx];
            SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL, curSkillId);
        }
    }
}
