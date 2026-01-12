using SCFrame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSDetailProps : _ASCUIMonoBase
    {
        [Header("攻击力文本")]
        public Text txtAttack;
        [Header("防御力文本")]
        public Text txtDefend;
        [Header("闪避率文本")]
        public Text txtMiss;
        [Header("暴击率文本")]
        public Text txtCritical;
        [Header("普攻物理等级文本")]
        public Text txtPhysicsLevel;
        [Header("普攻属性文本")]
        public Text txtAttackAttribute;
        [Header("护甲等级文本")]
        public Text txtArmor;
        [Header("法术抗性等级文本")]
        public Text txtMagicResistence;
        [Header("火属性抗性文本")]
        public Text txtFire;
        [Header("水属性抗性文本")]
        public Text txtWater;
        [Header("木属性抗性文本")]
        public Text txtWood;
    }
}

