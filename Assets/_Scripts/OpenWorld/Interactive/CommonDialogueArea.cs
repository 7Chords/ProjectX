using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCFrame;
using System;
using GameCore.RefData;
using GameCore.UI;

namespace GameCore.OW
{
    public class CommonDialogueArea : MonoBehaviour
    {
        public long dialogueGroup;
        private void Start()
        {
            this.AddTriggerEnter(onTriggerEnter);
        }
        private void OnDisable()
        {
            this.RemoveTriggerEnter(onTriggerEnter);
        }

        private void onTriggerEnter(Collider _coll, object[] _objs)
        {
            if(_coll.gameObject.tag == GameConst.TAG_PLAYER)
            {

                List<DialogueRefObj> dialogueRefList = SCRefDataMgr.instance.dialogueRefList.refDataList
                    .FindAll(x=>x.group == dialogueGroup);
                DialogueInfo dialogueInfo = new DialogueInfo(dialogueRefList);
                SCModel.instance.owModel.dialogueInfo = dialogueInfo;
                GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeDialogue(SCFrame.UI.SCUIShowType.FULL));
            }
        }
    }
}
