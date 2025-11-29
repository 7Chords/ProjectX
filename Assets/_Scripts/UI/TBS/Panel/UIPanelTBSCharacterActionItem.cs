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
        }
        public override void OnHidePanel()
        {
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

            TBSActorInfo curActionActorInfo = SCModel.instance.tbsModel.getCurActorInfo();
            if (curActionActorInfo == null)
                return;

            bool isCurActorAction = curActionActorInfo.characterRefObj.id == _m_actorInfo.characterRefObj.id;
            mono.imgHeadBg.color = isCurActorAction ? mono.colorIsAction : mono.colorIsNotAction;
            SCCommon.SetGameObjectEnable(mono.goIsActionShowList, isCurActorAction);
            SCCommon.SetGameObjectEnable(mono.goIsActionHideList, !isCurActorAction);

        }
    }
}
