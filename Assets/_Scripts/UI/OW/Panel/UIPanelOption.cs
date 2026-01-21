using SCFrame.UI;
using SCFrame;
using UnityEngine.EventSystems;
using System;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelOption : _ASCUIPanelBase<UIMonoOption>
    {
        public int _m_curSelectOptionIndex;
        public UIPanelOption(UIMonoOption _mono, SCUIShowType _showType) : base(_mono, _showType)
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
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_SWITCH_TO_UP_INPUT, onOWOptionHighLightUp);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_SWITCH_TO_DOWN_INPUT, onOWOptionHighLightDown);
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_CONFIRM_INPUT, onOWConfirmInput);

            for (int i = 0; i < mono.optionList.Count; i++)
            {
                switch (mono.optionList[i].optionType)
                {
                    case EOptionType.EXIT:
                        mono.optionList[i].btnOption.RemoveClickDown(onBtnExitClickDown);
                        mono.optionList[i].btnOption.RemoveMouseEnter(onBtnExitMouseEnter);
                        break;
                    case EOptionType.SETTING:
                        mono.optionList[i].btnOption.RemoveClickDown(onBtnSettingClickDown);
                        mono.optionList[i].btnOption.RemoveMouseEnter(onBtnSettingMouseEnter);
                        break;
                    case EOptionType.ITEM:
                        mono.optionList[i].btnOption.RemoveClickDown(onBtnItemClickDown);
                        mono.optionList[i].btnOption.RemoveMouseEnter(onBtnItemMouseEnter);
                        break;
                    case EOptionType.CHARACTER:
                        mono.optionList[i].btnOption.RemoveClickDown(onBtnCharacterClickDown);
                        mono.optionList[i].btnOption.RemoveMouseEnter(onBtnCharacterMouseEnter);
                        break;
                }
            }
        }


        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_UP_INPUT, onOWOptionHighLightUp);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_SWITCH_TO_DOWN_INPUT, onOWOptionHighLightDown);
            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_CONFIRM_INPUT, onOWConfirmInput);


            for (int i =0;i<mono.optionList.Count;i++)
            {
                switch(mono.optionList[i].optionType)
                {
                    case EOptionType.EXIT:
                        mono.optionList[i].btnOption.AddMouseLeftClickDown(onBtnExitClickDown);
                        mono.optionList[i].btnOption.AddMouseEnter(onBtnExitMouseEnter);
                        break;
                    case EOptionType.SETTING:
                        mono.optionList[i].btnOption.AddMouseLeftClickDown(onBtnSettingClickDown);
                        mono.optionList[i].btnOption.AddMouseEnter(onBtnSettingMouseEnter);
                        break;
                    case EOptionType.ITEM:
                        mono.optionList[i].btnOption.AddMouseLeftClickDown(onBtnItemClickDown);
                        mono.optionList[i].btnOption.AddMouseEnter(onBtnItemMouseEnter);
                        break;
                    case EOptionType.CHARACTER:
                        mono.optionList[i].btnOption.AddMouseLeftClickDown(onBtnCharacterClickDown);
                        mono.optionList[i].btnOption.AddMouseEnter(onBtnCharacterMouseEnter);
                        break;
                }
            }
            _m_curSelectOptionIndex = 0;
            refreshShow();
        }
        private void refreshShow()
        {
            for (int i = 0; i < mono.optionList.Count; i++)
            {
                if (i == _m_curSelectOptionIndex)
                {
                    SCCommon.SetGameObjectEnable(mono.optionList[i].goSelectShowList, true);
                }
                else
                {
                    SCCommon.SetGameObjectEnable(mono.optionList[i].goSelectShowList, false);
                }
            }
        }

        private void onBtnCharacterMouseEnter(PointerEventData _data, object[] _objs)
        {
            _m_curSelectOptionIndex = mono.optionList.FindIndex(x => x.optionType == EOptionType.CHARACTER);
            refreshShow();
        }
        private void onBtnItemMouseEnter(PointerEventData _data, object[] _objs)
        {
            _m_curSelectOptionIndex = mono.optionList.FindIndex(x => x.optionType == EOptionType.ITEM);
            refreshShow();
        }
        private void onBtnSettingMouseEnter(PointerEventData _data, object[] _objs)
        {
            _m_curSelectOptionIndex = mono.optionList.FindIndex(x => x.optionType == EOptionType.SETTING);
            refreshShow();
        }
        private void onBtnExitMouseEnter(PointerEventData _data, object[] _objs)
        {
            _m_curSelectOptionIndex = mono.optionList.FindIndex(x => x.optionType == EOptionType.EXIT);
            refreshShow();
        }

        private void onBtnExitClickDown(PointerEventData _data, object[] _objs)
        {
        }

        private void onBtnSettingClickDown(PointerEventData _data, object[] _objs)
        {
        }

        private void onBtnItemClickDown(PointerEventData _data, object[] _objs)
        {
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeItem(SCUIShowType.FULL));
        }

        private void onBtnCharacterClickDown(PointerEventData _data, object[] _objs)
        {
        }
        private void onOWOptionHighLightDown()
        {
            _m_curSelectOptionIndex = Mathf.Min(_m_curSelectOptionIndex + 1, mono.optionList.Count - 1);
            refreshShow();
        }

        private void onOWOptionHighLightUp()
        {
            _m_curSelectOptionIndex = Mathf.Max(_m_curSelectOptionIndex - 1, 0);
            refreshShow();

        }

        private void onOWConfirmInput()
        {
            EOptionType curOption = mono.optionList[_m_curSelectOptionIndex].optionType;
            switch (curOption)
            {
                case EOptionType.NONE:
                    break;
                case EOptionType.CHARACTER:
                    break;
                case EOptionType.ITEM:
                    {
                        GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeItem(SCUIShowType.FULL));
                    }
                    break;
                case EOptionType.SETTING:
                    break;
                case EOptionType.EXIT:
                    break;
            }
        }
    }
}
