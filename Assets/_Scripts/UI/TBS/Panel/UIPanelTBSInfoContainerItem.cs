using DG.Tweening;
using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSInfoContainerItem : _ASCUIPanelBase<UIMonoTBSInfoContainerItem>
    {

        private TBSActorInfo _m_actorInfo;

        private TweenContainer _m_tweenContainer;

        private UIPanelTBSInfoBuffContainer _m_buffContainer;
        
        public UIPanelTBSInfoContainerItem(UIMonoTBSInfoContainerItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }
        public override void BeforeDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;

            _m_buffContainer?.Discard();
        }

        public override void AfterInitialize()
        {
            _m_tweenContainer = new TweenContainer();

            _m_buffContainer = new UIPanelTBSInfoBuffContainer(mono.buffContainerMono);
            _m_buffContainer.Initialize();
        }
        public override void OnHidePanel()
        {
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, onTBSEnemyActorInfoChg);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_GET_BUFF, onTBSActorGetBuff);
            SCMsgCenter.UnregisterMsg(SCMsgConst.TBS_ACTOR_REMOVE_BUFF, onTBSActorRemoveBuff);

        }
        public override void OnShowPanel()
        {
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_INFO_CHG, onTBSEnemyActorInfoChg);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_GET_BUFF, onTBSActorGetBuff);
            SCMsgCenter.RegisterMsg(SCMsgConst.TBS_ACTOR_REMOVE_BUFF, onTBSActorRemoveBuff);

            if (_m_buffContainer != null)
            {
                _m_buffContainer.ShowPanel();
            }
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
            refreshBuffContainer();
        }

        private void refreshBuffContainer()
        {
            List<TBSGameBuffInfo> buffInfoList = SCModel.instance.tbsModel.GetActorByRunningId(_m_actorInfo.runningId).GetBuffInfoList();
            if (buffInfoList == null)
                return;
            _m_buffContainer?.SetListInfo(buffInfoList);
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
                long runningId = (long)_objs[0];
                if (_m_actorInfo.runningId == runningId)
                    refreshPanelShow(true);
            }
        }

        private void onTBSActorGetBuff(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            TBSGameBuffInfo buffInfo = _objs[0] as TBSGameBuffInfo;
            if (buffInfo == null)
                return;
            if (buffInfo.targetActor.actorInfo.runningId == _m_actorInfo.runningId)
            {
                refreshBuffContainer();
            }
        }

        private void onTBSActorRemoveBuff(object[] _objs)
        {
            if (_objs == null || _objs.Length == 0)
                return;
            TBSGameBuffInfo buffInfo = _objs[0] as TBSGameBuffInfo;
            if (buffInfo == null)
                return;
            if (buffInfo.targetActor.actorInfo.runningId == _m_actorInfo.runningId)
            {
                refreshBuffContainer();
            }
        }
    }
}
