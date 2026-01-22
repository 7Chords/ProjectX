using GameCore.RefData;
using GameCore.TBS;
using GameCore.UI;
using GameCore.Util;
using SCFrame;
using UnityEngine;

namespace GameCore.OW
{
    public static class DialogueHandler
    {
        public static void LoadDialogue(DialogueInfo _dialogueInfo)
        {
            Cursor.visible = true;
            SCGame.instance.owCamera.m_YAxis.m_MaxSpeed = 0;
            SCGame.instance.owCamera.m_XAxis.m_MaxSpeed = 0;
            PlayerController.instance.SetCanControl(false);
            PlayerController.instance.ChangeState(PlayerStateType.IDLE);
            SCModel.instance.owModel.dialogueInfo = _dialogueInfo;
            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeDialogue(SCFrame.UI.SCUIShowType.FULL));

        }

        public static void UnloadDialogue()
        {
            Cursor.visible = false;
            SCGame.instance.owCamera.m_YAxis.m_MaxSpeed = 3;
            SCGame.instance.owCamera.m_XAxis.m_MaxSpeed = 400;
            GameCoreMgr.instance.uiCoreMgr.CloseTopNode();
            PlayerController.instance.SetCanControl(true);
        }

        public static void DealDialogueEffect(DialogueEffectObj _effectObj)
        {
            if (_effectObj == null)
                return;
            switch (_effectObj.effectType)
            {
                case EDialogueEffectType.NONE:
                    break;
                case EDialogueEffectType.CHARACTER_JOIN:
                    {
                        long characterId = SCCommon.ParseLong(_effectObj.effectParamList[0].ToString());
                        TBSActorInfo info = new TBSActorInfo();
                        ActorData data = new ActorData();
                        data.InitNew(characterId);
                        info.Init(data, false);
                        SCDataMgr.instance.playerActorInfo.Add(info);
                        CharacterRefObj characterRefObj = SCRefDataMgr.instance.characterRefList.refDataList.Find(x => x.id == characterId);
                        TipQueueDealer.instance.EnqueueCommonTopTip(LanguageHelper.instance.GetTextTranslate(characterRefObj.characterName) + "加入了队伍");
                    }
                    break;
                case EDialogueEffectType.CHARACTER_LEAVE:
                    break;
                case EDialogueEffectType.ITEM_GET:
                    break;
                case EDialogueEffectType.ITEM_LOST:
                    break;
            }
        }
    }
}

