using System;
using System.Collections.Generic;
using UnityEngine;
using SCFrame;
using GameCore.RefData;
using GameCore.Util;

namespace GameCore.OW
{
    [Serializable]
    public class PickItem
    {
        public long itemId;
        public int itemAmount;
    }
    public class CommonPickItem : MonoBehaviour
    {
        [Header("获得道具")]
        public List<PickItem> boxItemList;


        private bool _m_hasEnterOpenArea;


        private void Start()
        {

            SCMsgCenter.RegisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
            this.AddTriggerEnter(onTriggerEnter);
            this.AddTriggerExit(onTriggerExit);
        }
        private void OnDisable()
        {
            SCMsgCenter.UnregisterMsgAct(SCMsgConst.OW_INTERACT_INPUT, onInteractInput);
            this.RemoveTriggerEnter(onTriggerEnter);
            this.RemoveTriggerExit(onTriggerExit);
        }

        private void onTriggerEnter(Collider _coll, object[] _objs)
        {
            if (_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {
                _m_hasEnterOpenArea = true;
                GameCommon.ShowInteractText("捡起", transform);
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
            for (int i = 0; i < boxItemList.Count; i++)
            {
                ItemRefObj itemRefObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == boxItemList[i].itemId);
                if (itemRefObj == null)
                    continue;
                SCDataMgr.instance.GetItem(boxItemList[i].itemId, boxItemList[i].itemAmount);
                TipQueueDealer.instance.EnqueueCommonTopTip("获得" + LanguageHelper.instance.GetTextTranslate(itemRefObj.itemName)
                    + "×" + boxItemList[i].itemAmount);
            }
            GameCommon.DiscardCurrentInteractText();
            SCCommon.DestoryGameObject(gameObject);
        }

    }
}
