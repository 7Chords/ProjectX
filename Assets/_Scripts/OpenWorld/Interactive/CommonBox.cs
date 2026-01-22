using GameCore.RefData;
using GameCore.Util;
using SCFrame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.OW
{
    [Serializable]
    public class BoxItem
    {
        public long itemId;
        public int itemAmount;
    }
    public class CommonBox : MonoBehaviour
    {
        [Header("角色动画机")]
        public Animator animator;
        [Header("开箱动画名")]
        public string openAnimName;
        [Header("动画事件监听器")]
        public AnimationEventTrigger animationEventTrigger;
        [Header("获得道具")]
        public List<BoxItem> boxItemList;

        private SCAnimationCtl _m_animCtl;

        private bool _m_hasEnterOpenArea;


        private void Start()
        {
            _m_animCtl = new SCAnimationCtl();
            _m_animCtl.SetAnimator(animator);
            _m_animCtl.Initialize();

            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
            this.AddTriggerEnter(onTriggerEnter);
            this.AddTriggerExit(onTriggerExit);
            animationEventTrigger.AddAnimationEvent(GameConst.SHOW_OPEN_BOX_OVER, onShowOpenOver);
        }
        private void OnDisable()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
            this.RemoveTriggerEnter(onTriggerEnter);
            this.RemoveTriggerExit(onTriggerExit);
            animationEventTrigger.RemoveAnimationEvent(GameConst.SHOW_OPEN_BOX_OVER);

        }

        private void onTriggerEnter(Collider _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {
                _m_hasEnterOpenArea = true;
                GameCommon.ShowInteractText("打开", transform);
            }
        }
        private void onTriggerExit(Collider _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {
                _m_hasEnterOpenArea = false;
                GameCommon.DiscardCurrentInteractText();
            }
        }
        private void onInteractInput()
        {
            if (!_m_hasEnterOpenArea)
                return;
            for(int i = 0; i < boxItemList.Count; i++)
            {
                ItemRefObj itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == boxItemList[i].itemId);
                if (itemRefObj == null)
                    continue;
                SCDataMgr.instance.GetItem(boxItemList[i].itemId, boxItemList[i].itemAmount);
                TipQueueDealer.instance.EnqueueCommonTopTip("获得" + LanguageHelper.instance.GetTextTranslate(itemRefObj.itemName)
                    + "×"+ boxItemList[i].itemAmount);
            }
            _m_animCtl.PlaySingleAniamtion(ResourcesHelper.LoadAsset<AnimationClip>(openAnimName));
        }

        private void onShowOpenOver()
        {
            GameCommon.DiscardCurrentInteractText();
            SCCommon.DestoryGameObject(gameObject);
        }

    }

}