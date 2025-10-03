using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    /// <summary>
    /// Container»ùÀà½Å±¾
    /// </summary>
    public abstract class UIPanelContainerBase<TMONO,TITEMPANEL,TITEMMONO> : _ASCUIPanelBase<TMONO> 
        where TMONO:_ASCUIMonoBase 
        where TITEMPANEL : _ASCUIPanelBase<TITEMMONO>
        where TITEMMONO : _ASCUIMonoBase
    {
        public UIPanelContainerBase(TMONO _mono, SCUIShowType _showType = SCUIShowType.INTERNAL) : base(_mono, _showType)
        {
        }

        protected abstract TITEMPANEL creatItemPanel(TITEMMONO _mono);

        protected abstract GameObject creatItemGO();

    }
}
