using DG.Tweening;
using GameCore.TBS;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSEnemyHudItem : _ASCUIPanelBase<UIMonoTBSEnemyHudItem>
    {

        private TBSActorInfo _m_actorInfo;
        private TweenContainer _m_tweenContainer;
        public UIPanelTBSEnemyHudItem(UIMonoTBSEnemyHudItem _mono, SCUIShowType _showType) : base(_mono, _showType)
        {
        }
        public override void OnInitialize()
        {
        }
        public override void OnDiscard()
        {
        }

        public override void OnHidePanel()
        {
            Tween tween = mono.canvasGroup.DOFade(0, mono.fadeOutDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 1;
            });
            _m_tweenContainer?.RegDoTween(tween);
        }

        public override void OnShowPanel()
        {
            Tween tween = mono.canvasGroup.DOFade(1, mono.fadeInDuration).OnStart(() =>
            {
                mono.canvasGroup.alpha = 0;
            });
            _m_tweenContainer?.RegDoTween(tween);
        }

        public void SetInfo(TBSActorInfo _info)
        {
            if(_info == null)
            {
                Debug.LogError("UIPanelTBSEnemyHudItem setinfo ´«²ÎÊÇ¿Õ£¡£¡£¡");
                return;
            }
            _m_actorInfo = _info;
            mono.txtNameWithLv.text = GameCommon.GetCharacterNameWithLv(_m_actorInfo.characterLv, _m_actorInfo.characterName);
            mono.imgHpBar.fillAmount = (float)_m_actorInfo.curHp / _m_actorInfo.maxHp;
            mono.txtHp.text = LanguageHelper.instance.GetTextTranslate("#2_{0}/{1}", _m_actorInfo.curHp, _m_actorInfo.maxHp);
            mono.imgPhysicalArmor.sprite = GameCommon.GetSpriteByPhysicalArmor(_m_actorInfo.armorLevel);
            mono.imgMagicResistent.sprite = GameCommon.GetSpriteByMagicResistance(_m_actorInfo.magicResistanceLevel);
            mono.imgFireResistent.sprite = GameCommon.GetSpriteByMagicAttributeWeak(EMagicAttributeType.FIRE,_m_actorInfo);
            mono.imgWaterResistent.sprite = GameCommon.GetSpriteByMagicAttributeWeak(EMagicAttributeType.WATER, _m_actorInfo);
            mono.imgWoodResistent.sprite = GameCommon.GetSpriteByMagicAttributeWeak(EMagicAttributeType.WOOD, _m_actorInfo);
        }
    }
}
