using DG.Tweening;
using GameCore.Util;
using SCFrame.UI;
using SCFrame;
using GameCore.TBS;

namespace GameCore.UI
{
    public class UIPanelTBSConfirm : _ASCUIPanelBase<UIMonoTBSConfirm>
    {
        //private TweenContainer _m_tweenContainer;

        private SCUIConfirmType _m_confirmType;//确认类型
        public UIPanelTBSConfirm(UIMonoTBSConfirm _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }


        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.TBS_ACTOR_CONFIRM_RELEASE, onTBSActorConfirmRelease);
        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.TBS_ACTOR_CONFIRM_RELEASE, onTBSActorConfirmRelease);
        }

        public override void BeforeDiscard()
        {
        }

        public override void AfterInitialize()
        {
        }

        public void SetInfo(SCUIConfirmType _confirmType)
        {
            _m_confirmType = _confirmType;
        }

        private void onTBSActorConfirmRelease()
        {

            switch(_m_confirmType)
            {
                case SCUIConfirmType.SKILL:
                    {
                        TBSActorInfo actorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
                        if (actorInfo == null)
                            return;
                        long curSkillId = actorInfo.skillList[SCModel.instance.tbsModel.curSelectSkillIdx];
                        SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL, curSkillId);
                    }
                    break;
                case SCUIConfirmType.ITEM:
                    {
                        TBSActorInfo actorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
                        if (actorInfo == null)
                            return;
                        long curItemId = SCDataMgr.instance.itemDataList[SCModel.instance.tbsModel.curSelectItemIdx].itemId;
                        SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ITEM, curItemId);
                    } 
                    break;
                default:
                    break;
            }
        }
    }
}
