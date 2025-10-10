using GameCore.TBS;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSEnemyHudItem : _ASCUIPanelBase<UIMonoTBSEnemyHudItem>
    {
        public UIPanelTBSEnemyHudItem(UIMonoTBSEnemyHudItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void OnDiscard()
        {
        }

        public override void OnHidePanel()
        {
        }

        public override void OnInitialize()
        {
        }

        public override void OnShowPanel()
        {
        }

        public void SetInfo(TBSActorInfo _info)
        {

        }
    }
}
