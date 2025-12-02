using SCFrame.UI;
using SCFrame;
using UnityEngine.EventSystems;
using System;
using UnityEngine;
using DG.Tweening;
using GameCore.Util;
using GameCore.TBS;
using System.Collections.Generic;

namespace GameCore.UI
{
    public class UIPanelTBSMain : _ASCUIAnimPanelBase<UIMonoTBSMain>
    {
        //private TweenContainer _m_tweenContainer;
        public UIPanelTBSMain(UIMonoTBSMain _mono, SCUIShowType _showType) : base(_mono, _showType)
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

            mono.btnNormalAttack.RemoveClickDown(onBtnNormalAttackClickDown);
            mono.btnSkill.RemoveClickDown(onBtnSkillClickDown);
            mono.btnItem.RemoveClickDown(onBtnItemClickDown);
            mono.btnDefence.RemoveClickDown(onBtnDefenceClickDown);
        }



        public override void OnShowPanel()
        {
            mono.btnNormalAttack.AddClickDown(onBtnNormalAttackClickDown);
            mono.btnSkill.AddClickDown(onBtnSkillClickDown);
            mono.btnItem.AddClickDown(onBtnItemClickDown);
            mono.btnDefence.AddClickDown(onBtnDefenceClickDown);

            //todo:优化

            SCModel.instance.tbsModel.selectTargetType = SCModel.instance.tbsModel.getCurActorInfo().targetType;

            //重新展示敌人hud
            GameCoreMgr.instance.uiCoreMgr.ShowNode(nameof(UINodeTBSEnemyHud));

            //重新设置光标
            List<Vector3> worldPosList = new List<Vector3>();
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                worldPosList.Add(SCModel.instance.tbsModel.getCurSingleSelectTargetActor().GetCursorPos());
            else if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
            {
                foreach (var module in SCModel.instance.tbsModel.enemyActorModuleList)
                {
                    worldPosList.Add(module.GetCursorPos());
                }
            }
            TBSCursorMgr.instance.SetSelectionCursor(worldPosList);


            refreshPanelShow();
        }

        private void onBtnNormalAttackClickDown(PointerEventData _eventData, object[] _args)
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ATTACK_INPUT);
        }
        private void onBtnSkillClickDown(PointerEventData data, object[] arg2)
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_SKILL_INPUT);
        }
        private void onBtnItemClickDown(PointerEventData data, object[] arg2)
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_ITEM_INPUT);
        }
        private void onBtnDefenceClickDown(PointerEventData data, object[] arg2)
        {
            SCMsgCenter.SendMsgAct(SCMsgConst.TBS_DEFEND_INPUT);
        }

        private void refreshPanelShow()
        {
            TBSActorInfo actorInfo = SCModel.instance.tbsModel.getCurActorInfo();
            if (actorInfo == null)
                return;
            mono.imgCharacterHead.sprite = ResourcesHelper.LoadAsset<Sprite>(actorInfo.characterRefObj.assetHeadIconObjName);
        }

    }
}
