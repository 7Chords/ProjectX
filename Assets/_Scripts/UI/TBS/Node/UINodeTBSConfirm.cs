using Cinemachine;
using GameCore.RefData;
using GameCore.TBS;
using SCFrame;
using SCFrame.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.UI
{
    public class UINodeTBSConfirm : _ASCUINodeBase
    {
        public UINodeTBSConfirm(SCUIShowType _showType,SCUIConfirmType _confirmType, bool _isPlayerTargetConfirm) : base(_showType)
        {
            _m_confirmType = _confirmType;
            _m_isPlayerTargetConfirm = _isPlayerTargetConfirm;
        }

        public override bool needHideWhenEnterNewSameTypeNode => true;

        public override bool needShowWhenQuitNewSameTypeNode => false;
        public override bool canQuitByEsc => true;

        public override bool canQuitByMouseRight => true;
        public override bool ignoreOnUIList => false;

        private GameObject _m_panelGO;
        private UIPanelTBSConfirm _m_tbsConfirmPanel;
        private UIMonoTBSConfirm _m_tbsConfirmMono;

        private SCUIConfirmType _m_confirmType;
        private bool _m_isPlayerTargetConfirm;

        public bool isPlayerTargetConfirm=> _m_isPlayerTargetConfirm;
        public SCUIConfirmType confirmType => _m_confirmType;

        public override void OnEnterNode()
        {
            _m_panelGO = ResourcesHelper.LoadGameObject(GetResName(), GetRootTransform(), true);
            if (_m_panelGO == null)
            {
                Debug.LogError("未找到资源名为" + GetResName() + "的资源!!!");
                return;
            }
            _m_tbsConfirmMono = _m_panelGO.GetComponent<UIMonoTBSConfirm>();
            if (_m_tbsConfirmMono == null)
            {
                Debug.LogError("资源名为" + GetResName() + "的资源上不存在对应的Mono!!!");
                return;
            }

            _m_tbsConfirmPanel = new UIPanelTBSConfirm(_m_tbsConfirmMono, _m_showType);
            _m_tbsConfirmPanel.Initialize();
            
        }

        public override void OnQuitNode()
        {
            if (_m_tbsConfirmPanel == null)
                return;
            _m_tbsConfirmPanel.Discard();
        }


        public override void OnHideNode()
        {
            if (_m_tbsConfirmPanel == null)
                return;

            switch(_m_confirmType)
            {
                case SCUIConfirmType.SKILL:
                    {
                        //这里做相机操作的话只需要判断是敌人还是玩家 如果是玩家 需要等相机运动到合适的位置
                        if (isPlayerTargetConfirm)
                        {

                            GameCameraMgr.instance.SwitchToMainVirtualCamera();
                            hidePlayerHudAndCursor();
                            _m_tbsConfirmPanel.HidePanel();
                            GameCameraMgr.instance.SetCameraTarget(SCModel.instance.tbsModel.gameMono.playerLookEnemyCenterPos);
                        }
                        else
                        {
                            _m_tbsConfirmPanel.HidePanel();
                        }
                    }
                    break;
                case SCUIConfirmType.ITEM:
                    {
                        //这里做相机操作的话只需要判断是敌人还是玩家 如果是玩家 需要等相机运动到合适的位置
                        if (isPlayerTargetConfirm)
                        {

                            GameCameraMgr.instance.SwitchToMainVirtualCamera();
                            hidePlayerHudAndCursor();
                            _m_tbsConfirmPanel.HidePanel();
                            GameCameraMgr.instance.SetCameraTarget(SCModel.instance.tbsModel.gameMono.playerLookEnemyCenterPos);
                        }
                        else
                        {
                            _m_tbsConfirmPanel.HidePanel();
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        public override void OnShowNode()
        {
            if (_m_tbsConfirmPanel == null)
                return;
            _m_tbsConfirmPanel.SetInfo(_m_confirmType);
            switch (_m_confirmType)
            {
                case SCUIConfirmType.SKILL:
                    {
                        TBSActorSkillRefObj skillRefObj = SCModel.instance.tbsModel.GetCurSkillRefObj();
                        if (skillRefObj == null)
                        {
                            SCDebugHelper.LogError("skillRefObj为空!!!");
                            return;
                        }
                        //这里做相机操作的话只需要判断是敌人还是玩家 如果是玩家 需要等相机运动到合适的位置
                        if (isPlayerTargetConfirm)
                        {
                            GameCameraMgr.instance.SwitchToVirtualCamera(SCModel.instance.tbsModel.gameMono.lookAllPlayersVC);
                            //tip:放到下下帧执行 因为下一帧Cinemachine才成功设置当前激活的虚拟相机
                            SCTaskHelper.instance.DoInNextUpdate(() =>
                            {
                                SCTaskHelper.instance.DoInNextUpdate(() =>
                                {
                                    hideEnemyHudAndCursor();
                                    showUIAndCursor(true, skillRefObj.damageTargetType);
                                    _m_tbsConfirmPanel.ShowPanel();
                                });
                            });
                        }
                        else
                         {
                            _m_tbsConfirmPanel.ShowPanel();
                        }
                    }
                    break;
                case SCUIConfirmType.ITEM:
                    {
                        ItemRefObj itemRefObj = SCModel.instance.tbsModel.GetCurItemRefObj();
                        if (itemRefObj == null)
                        {
                            SCDebugHelper.LogError("itemRefObj为空!!!");
                            return;
                        }
                        //这里做相机操作的话只需要判断是敌人还是玩家 如果是玩家 需要等相机运动到合适的位置
                        if (isPlayerTargetConfirm)
                        {
                            GameCameraMgr.instance.SwitchToVirtualCamera(SCModel.instance.tbsModel.gameMono.lookAllPlayersVC);
                            SCTaskHelper.instance.DoInNextUpdate(() =>
                            {
                                SCTaskHelper.instance.DoInNextUpdate(() =>
                                {
                                    hideEnemyHudAndCursor();
                                    showUIAndCursor(true, itemRefObj.itemTargetType);
                                    _m_tbsConfirmPanel.ShowPanel();
                                });
                            });
                        }
                        else
                        {
                            _m_tbsConfirmPanel.ShowPanel();
                        }
                    }
                    break;
            }
        }

        private void hideEnemyHudAndCursor()
        {
            //tip:skill面板关闭时打开了光标 所以这里要再关掉
            //因为对于需要相机运动的情况 要先运动完再打开
            TBSCursorMgr.instance.HideSelectionCursor();
            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSEnemyHud));
        }

        private void hidePlayerHudAndCursor()
        {
            TBSCursorMgr.instance.HideSelectionCursor();
            GameCoreMgr.instance.uiCoreMgr.HideNode(nameof(UINodeTBSPlayerHud));
        }
        private void showUIAndCursor(bool _isPlayerTarget,ETargetType _targetType)
        {
            switch (_targetType)
            {
                case ETargetType.SINGLE:
                    {
                        List<Vector3> posList = new List<Vector3>();
                        if(_isPlayerTarget)
                        {
                            posList.Add(SCModel.instance.tbsModel.GetCurSelectSinglePlayerTargetActor().GetCursorPos());
                            TBSCursorMgr.instance.SetSelectionCursor(posList);
                            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSPlayerHud(SCUIShowType.ADDITION, SCModel.instance.tbsModel.playerActorModuleList));
                        }
                        else
                        {
                            posList.Add(SCModel.instance.tbsModel.GetCurSelectSingleEnemyTargetActor().GetCursorPos());
                            TBSCursorMgr.instance.SetSelectionCursor(posList);
                            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSEnemyHud(SCUIShowType.ADDITION, SCModel.instance.tbsModel.enemyActorModuleList));
                        }
                    }
                    break;
                case ETargetType.ALL:
                    {
                        List<Vector3> posList = new List<Vector3>();
                        if (_isPlayerTarget)
                        {
                            for (int i = 0; i < SCModel.instance.tbsModel.playerActorModuleList.Count; i++)
                            {
                                posList.Add(SCModel.instance.tbsModel.playerActorModuleList[i].GetCursorPos());
                            }
                            TBSCursorMgr.instance.SetSelectionCursor(posList);
                            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSPlayerHud(SCUIShowType.ADDITION, SCModel.instance.tbsModel.playerActorModuleList));
                        }
                        else
                        {
                            for (int i = 0; i < SCModel.instance.tbsModel.enemyActorModuleList.Count; i++)
                                posList.Add(SCModel.instance.tbsModel.enemyActorModuleList[i].GetCursorPos());
                            TBSCursorMgr.instance.SetSelectionCursor(posList);
                            GameCoreMgr.instance.uiCoreMgr.AddNode(new UINodeTBSEnemyHud(SCUIShowType.ADDITION, SCModel.instance.tbsModel.enemyActorModuleList));
                        }
                    }
                    break;
            }

        }






        public override string GetNodeName()
        {
            return nameof(UINodeTBSConfirm);
        }

        public override string GetResName()
        {
            return GameCommon.GetUIResObjPath(GameConst.TBS_CONFIM_PANEL);
        }

        public override void CopyData(_ASCUINodeBase _anotherNode)
        {
            if(_anotherNode is UINodeTBSConfirm)
            {
                _m_confirmType = (_anotherNode as UINodeTBSConfirm)._m_confirmType;
                _m_isPlayerTargetConfirm = (_anotherNode as UINodeTBSConfirm)._m_isPlayerTargetConfirm;

            }
        }
    }
}
