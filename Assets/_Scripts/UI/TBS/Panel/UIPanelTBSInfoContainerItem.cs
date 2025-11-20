using DG.Tweening;
using GameCore.TBS;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSInfoContainerItem : _ASCUIPanelBase<UIMonoTBSInfoContainerItem>
    {

        private TBSActorInfo _m_actorInfo;

        private TweenContainer _m_tweenContainer;
        public UIPanelTBSInfoContainerItem(UIMonoTBSInfoContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }

        public override void OnDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }

        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, onTBSEnemyActorInfoChg);
        }

        public override void OnInitialize()
        {
            _m_tweenContainer = new TweenContainer();
        }

        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, onTBSEnemyActorInfoChg);
        }

        public void SetInfo(TBSActorInfo _info)
        {
            _m_actorInfo = _info;
            refreshPanelShow();
        }

        private void refreshPanelShow(bool _needBarFade = false)
        {
            if (_m_actorInfo == null)
                return;
            mono.imgCharacterHead.sprite = ResourcesHelper.LoadAsset<Sprite>(_m_actorInfo.characterRefObj.assetHeadIconObjName);
            mono.txtHp.text = LanguageHelper.instance.GetTextTranslate("#2_{0}/{1}", _m_actorInfo.curHp, _m_actorInfo.maxHp);
            mono.txtMp.text = LanguageHelper.instance.GetTextTranslate("#2_{0}/{1}", _m_actorInfo.curMp, _m_actorInfo.maxMp);
            if (!_needBarFade)
            {
                mono.imgHpBar.fillAmount = (float)_m_actorInfo.curHp / _m_actorInfo.maxHp;
                mono.imgMpBar.fillAmount = (float)_m_actorInfo.curMp / _m_actorInfo.maxMp;
            }
            else
            {
                _m_tweenContainer.RegDoTween(mono.imgHpBar.DOFillAmount((float)_m_actorInfo.curHp / _m_actorInfo.maxHp, mono.barFadeDuration));
                _m_tweenContainer.RegDoTween(mono.imgMpBar.DOFillAmount((float)_m_actorInfo.curMp / _m_actorInfo.maxMp, mono.barFadeDuration));
            }

        }

        private void onTBSEnemyActorInfoChg(object[] _objs)
        {
            {
                if (_objs == null || _objs.Length == 0)
                    return;
                if (_m_actorInfo == null)
                {
                    Debug.LogError("onTBSEnemyActorInfoChg 调用时actorinfo为null！！！");
                    return;
                }
                long characterId = (long)_objs[0];
                if (_m_actorInfo.characterRefObj.id == characterId)
                    refreshPanelShow(true);
            }
        }
    }
}
