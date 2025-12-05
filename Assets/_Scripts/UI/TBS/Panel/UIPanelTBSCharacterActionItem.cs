using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSCharacterActionItem : _ASCUIPanelBase<UIMonoTBSCharacterActionItem>
    {
        private TBSActorInfo _m_actorInfo;
        public UIPanelTBSCharacterActionItem(UIMonoTBSCharacterActionItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }
        public override void BeforeDiscard()
        {

        }

        public override void AfterInitialize()
        {

        }
        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);
        }
        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_DIE, onTBSActorDie);
        }

        public void SetInfo(TBSActorInfo _actorInfo)
        {
            if (_actorInfo == null)
                return;
            _m_actorInfo = _actorInfo;
            _refreshWndShow();
        }

        private void _refreshWndShow()
        {
            if (_m_actorInfo == null)
                return;
            mono.imgHead.sprite = ResourcesHelper.LoadAsset<Sprite>(_m_actorInfo.characterRefObj.assetHeadIconObjName);
            refreshIsActionShow();
            refreshHasDeadShow();
        }

        private void refreshIsActionShow()
        {
            TBSActorInfo curActionActorInfo = SCModel.instance.tbsModel.GetCurActorInfo();
            if (curActionActorInfo == null)
                return;

            bool isCurActorAction = curActionActorInfo.characterRefObj.id == _m_actorInfo.characterRefObj.id;
            mono.imgHeadBg.color = isCurActorAction ? mono.colorIsAction : mono.colorIsNotAction;
            SCCommon.SetGameObjectEnable(mono.goIsActionShowList, isCurActorAction);
            SCCommon.SetGameObjectEnable(mono.goIsActionHideList, !isCurActorAction);
            GetGameObject().transform.localScale = isCurActorAction
                ? Vector3.one * mono.scaleIsAction
                : Vector3.one * mono.scaleIsNotAction;
        }

        private void refreshHasDeadShow()
        {
            bool hasDead = _m_actorInfo.hasDead;
            SCCommon.SetGameObjectEnable(mono.goActorDeadShowList, hasDead);
            SCCommon.SetGameObjectEnable(mono.goActorDeadHideList, !hasDead);
        }

        private void onTBSActorDie(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            long characterId = (long)_objs[0];

            if (characterId == _m_actorInfo.characterRefObj.id)
                refreshHasDeadShow();

        }
    }
}
