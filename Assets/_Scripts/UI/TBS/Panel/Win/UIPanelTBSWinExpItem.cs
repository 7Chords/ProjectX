using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSWinExpItem : _ASCUIPanelBase<UIMonoTBSWinExpItem>
    {
        public UIPanelTBSWinExpItem(UIMonoTBSWinExpItem _mono, SCUIShowType _showType) : base(_mono, _showType)
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

        public void SetInfo(TBSActorInfo _actorInfo,bool _hasLevelUpState)
        {
            TBSActorInfo info = SCDataMgr.instance.playerActorInfoList.Find(x => x.characterRefObj.id == _actorInfo.characterRefObj.id);
            if (info == null)
                return;
            mono.imgCharacterHead.sprite = ResourcesHelper.LoadAsset<Sprite>(_actorInfo.characterRefObj.assetHeadIconObjName);
            mono.txtCharacterLevel.text = GameCommon.GetCharacterNameWithLv(info.characterLv, _actorInfo.characterRefObj.characterName);
            mono.imgCharacterExpBar.fillAmount = (float)info.curExp / info.levelFullExp;
            SCCommon.SetGameObjectEnable(mono.goLevelUpShowList, _hasLevelUpState);
        }
    }
}
