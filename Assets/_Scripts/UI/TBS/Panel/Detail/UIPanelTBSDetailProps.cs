using GameCore.TBS;
using GameCore.Util;
using SCFrame;
using SCFrame.UI;

namespace GameCore.UI
{
    public class UIPanelTBSDetailProps : _ASCUIPanelBase<UIMonoTBSDetailProps>
    {
        private TBSActorInfo _m_actorInfo;
        public UIPanelTBSDetailProps(UIMonoTBSDetailProps _mono, SCUIShowType _showType) : base(_mono, _showType)
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

        public void SetInfo(TBSActorInfo _info)
        {
            _m_actorInfo = _info;
            refreshShow();
        }

        private void refreshShow()
        {
            mono.txtAttack.text = LanguageHelper.instance.GetTextTranslate("#1_detail_attack", _m_actorInfo.attack);
            mono.txtDefend.text = LanguageHelper.instance.GetTextTranslate("#1_detail_defend", _m_actorInfo.defend);
            mono.txtMiss.text = LanguageHelper.instance.GetTextTranslate("#1_detail_miss", _m_actorInfo.missChance);
            mono.txtCritical.text = LanguageHelper.instance.GetTextTranslate("#1_detail_critical", _m_actorInfo.criticalChance);
            mono.txtPhysicsLevel.text = LanguageHelper.instance.GetTextTranslate("#1_detail_physics_level",Enum2StrFactory.CreateLocalStrByPhysicalLevelEnum(_m_actorInfo.attackPhysicalLevel));
            mono.txtAttackAttribute.text = LanguageHelper.instance.GetTextTranslate("#1_detail_attack_attribute",Enum2StrFactory.CreateLocalStrByMagicAttributeEnum(_m_actorInfo.attackMagicAttribute));
            mono.txtMagicResistence.text = LanguageHelper.instance.GetTextTranslate("#1_detail_magic_resistence_level",Enum2StrFactory.CreateLocalStrByMagicResistenceLevelEnum(_m_actorInfo.magicResistanceLevel));
            mono.txtArmor.text = LanguageHelper.instance.GetTextTranslate("#1_detail_armor_level",Enum2StrFactory.CreateLocalStrByArmorLevelEnum(_m_actorInfo.armorLevel));
            mono.txtFire.text = LanguageHelper.instance.GetTextTranslate("#1_detail_fire",Enum2StrFactory.CreateLocalStrByMagicRestraintEnum(GameCommon.GetMagicRestraintType(EMagicAttributeType.FIRE, _m_actorInfo)));
            mono.txtWater.text = LanguageHelper.instance.GetTextTranslate("#1_detail_water", Enum2StrFactory.CreateLocalStrByMagicRestraintEnum(GameCommon.GetMagicRestraintType(EMagicAttributeType.WATER, _m_actorInfo)));
            mono.txtWood.text = LanguageHelper.instance.GetTextTranslate("#1_detail_wood", Enum2StrFactory.CreateLocalStrByMagicRestraintEnum(GameCommon.GetMagicRestraintType(EMagicAttributeType.WOOD, _m_actorInfo)));
            mono.txtAttackType.text = LanguageHelper.instance.GetTextTranslate("#1_detail_attack_damage_type", Enum2StrFactory.CreateLocalStrByDamageEnum(_m_actorInfo.attackDamageType));
        }
    }
}
