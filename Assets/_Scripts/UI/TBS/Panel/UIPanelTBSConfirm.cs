using DG.Tweening;
using GameCore.Util;
using SCFrame.UI;
using SCFrame;
using GameCore.TBS;
using GameCore.RefData;

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
                        TBSActorSkillRefObj skillRefObj = SCModel.instance.tbsModel.GetCurSkillRefObj();
                        if(skillRefObj == null)
                        {
                            SCDebugHelper.LogError("skillRefObj为空!!!");
                            return;
                        }
                        SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_SKILL, skillRefObj.id);
                    }
                    break;
                case SCUIConfirmType.ITEM:
                    {
                        ItemRefObj itemRefObj = SCModel.instance.tbsModel.GetCurItemRefObj();
                        if (itemRefObj == null)
                        {
                            SCDebugHelper.LogError("itemRefObj为空!!!");
                            return;
                        }
                        SCMsgCenter.SendMsg(SCMsgConst.TBS_ACTOR_ITEM, itemRefObj.id);
                    } 
                    break;
                default:
                    break;
            }
        }


    }
}
