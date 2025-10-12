using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using UnityEngine;

namespace GameCore.UI
{
    public class UIPanelTBSEnemyHudItem : _ASCUIPanelBase<UIMonoTBSEnemyHudItem>
    {

        private TBSActorInfo _m_actorInfo;
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
            if(_info == null)
            {
                Debug.LogError("UIPanelTBSEnemyHudItem setinfo ´«²ÎÊÇ¿Õ£¡£¡£¡");
                return;
            }
            _m_actorInfo = _info;
            mono.txtNameWithLv.text = SCCommon.GetCharacterNameWithLv(_m_actorInfo.characterLv, _m_actorInfo.characterName);
            mono.imgHpBar.fillAmount = (float)_m_actorInfo.curHp / _m_actorInfo.maxHp;
            mono.txtHp.text = LanguageHelper.instance.GetTextTranslate("#2_{0}/{1}", _m_actorInfo.curHp, _m_actorInfo.maxHp);
            mono.imgPhysicalArmor.sprite = TBSCommon.GetSpriteByPhysicalArmor(_m_actorInfo.armorLevel);
            mono.imgMagicResistent.sprite = TBSCommon.GetSpriteByMagicResistance(_m_actorInfo.magicResistanceLevel);
            mono.imgFireResistent.sprite = TBSCommon.GetSpriteByMagicAttributeWeak(EMagicAttributeType.FIRE,_m_actorInfo);
            mono.imgWaterResistent.sprite = TBSCommon.GetSpriteByMagicAttributeWeak(EMagicAttributeType.WATER, _m_actorInfo);
            mono.imgWoodResistent.sprite = TBSCommon.GetSpriteByMagicAttributeWeak(EMagicAttributeType.WOOD, _m_actorInfo);
        }
    }
}
