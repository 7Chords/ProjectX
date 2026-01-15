using SCFrame.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore.UI
{
    public class UIMonoTBSDetail : _ASCUIMonoBase
    {
        [Header("带等级的名字")]
        public Text txtNameWithLv;
        [Header("角色描述")]
        public Text txtCharacterDesc;
        [Header("角色3D图")]
        public RawImage rawImgCharacter;
        [Header("血量bar")]
        public Image imgHpBar;
        [Header("魔量bar")]
        public Image imgMpBar;
        [Header("血量文本")]
        public Text txtHp;
        [Header("魔量文本")]
        public Text txtMp;
        [Header("角色属性组mono")]
        public UIMonoTBSDetailProps monoDetailPorps;
        [Header("角色头像容器mono")]
        public UIMonoCommonContainer monoHeaderContainer;
        [Header("角色buff容器mono")]
        public UIMonoCommonContainer monoBuffContainer;
        [Header("关闭按钮")]
        public Button btnClose;
        [Header("是玩家详情时显示的物体列表")]
        public List<GameObject> goIsPlayerActorShowList;
    }
}
