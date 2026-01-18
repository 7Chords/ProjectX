using GameCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    public class SCInputListener : Singleton<SCInputListener>
    {

        private int _m_tbsFrameChecker;

        private int _m_tbsFrameInterval;

        private Ray _m_mouseRay;
        private RaycastHit _m_raycastHit;

        public bool _m_canInput;
        public override void OnInitialize()
        {
            SCTaskHelper.instance.AddUpdateListener(updateInput);
            _m_tbsFrameInterval = SCRefDataMgr.instance.gameGeneralRefObj.tbsInputFrameInterval;
        }

        public override void OnDiscard()
        {
            SCTaskHelper.instance.RemoveUpdateListener(updateInput);
        }


        private void updateInput()
        {
            if (GameCoreMgr.instance.tbsCoreMgr.tbsGameHasStarted)
            {
                if (!_m_canInput)
                    return;
                if (_m_tbsFrameChecker < _m_tbsFrameInterval)
                {
                    _m_tbsFrameChecker += 1;
                    return;
                }
                if (Input.anyKeyDown)
                    _m_tbsFrameChecker = 0;

                if (Input.GetKeyDown(KeyCode.Escape))
                    SCMsgCenter.SendMsg(SCMsgConst.ESC_INPUT);
                if (Input.GetMouseButtonDown(1))
                    SCMsgCenter.SendMsg(SCMsgConst.MOUSE_RIGHT_INPUT);
                if (Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsSkillKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SKILL_INPUT);
                if (Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsAttackKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ATTACK_INPUT);
                if (Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsDefendKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_DEFEND_INPUT);
                if (Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsItemKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_ITEM_INPUT);
                if (Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsSwitchToUpKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SWITCH_TO_UP_INPUT);
                if (Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsSwitchToDownKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SWITCH_TO_DOWN_INPUT);
                if (Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsSwitchToLeftKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SWITCH_TO_LEFT_INPUT);
                if (Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsSwitchToRightKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_SWITCH_TO_RIGHT_INPUT);
                if (Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsConfirmKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_CONFIRM_INPUT);
                if(Input.GetKeyDown(SCSettingMgr.instance.saveKeyInfo.tbsDetailKeyCode))
                    SCMsgCenter.SendMsg(SCMsgConst.TBS_DETAIL_INPUT);


                if (Input.GetMouseButtonDown(0))
                {
                    _m_mouseRay = SCGame.instance.gameCamera.ScreenPointToRay(Input.mousePosition);

                    //tips
                    //LayerMask.NameToLayer()返回的是层索引（int 类型，如 0、8、9
                    //但Physics.Raycast的layerMask参数需要的是层掩码（LayerMask），直接传入层索引会导致掩码计算错误，射线无法过滤到目标层。
                    //我的actor物体上的碰撞体是trigger 所以要设置检测类型
                    if (Physics.Raycast(_m_mouseRay,
                        out _m_raycastHit,
                        GameConst.MOUSE_RAY_MAX_DISTANCE,
                        1 << LayerMask.NameToLayer(GameConst.LAYER_CHARACTER),QueryTriggerInteraction.Collide))
                    {
                        if (_m_raycastHit.collider == null)
                            return;
                        switch (_m_raycastHit.collider.gameObject.tag)
                        {
                            case GameConst.TAG_ENEMY:
                                {
                                    SCMsgCenter.SendMsg(SCMsgConst.TBS_MOUSE_CLICK_ENEMY_INPUT, _m_raycastHit.collider.gameObject);
                                }
                                break;
                            case GameConst.TAG_PLAYER:
                                {
                                    SCMsgCenter.SendMsg(SCMsgConst.TBS_MOUSE_CLICK_PLAYER_INPUT, _m_raycastHit.collider.gameObject);
                                }
                                break;
                            default:
                                break;
                        }

                    }
                }
            }
            else
            {
                //if (Input.GetKey(SCSettingMgr.instance.saveKeyInfo.owForwardKeyCode))
                //    SCMsgCenter.SendMsg(SCMsgConst.OW_FORWARD_INPUT);
                //if (Input.GetKey(SCSettingMgr.instance.saveKeyInfo.owBackwardKeyCode))
                //    SCMsgCenter.SendMsg(SCMsgConst.OW_BACKWARD_INPUT);
                //if (Input.GetKey(SCSettingMgr.instance.saveKeyInfo.owLeftKeyCode))
                //    SCMsgCenter.SendMsg(SCMsgConst.OW_LEFT_INPUT);
                //if (Input.GetKey(SCSettingMgr.instance.saveKeyInfo.owRightKeyCode))
                //    SCMsgCenter.SendMsg(SCMsgConst.OW_RIGHT_INPUT);
            }
        }


        public void SetCanInput(bool _canInput)
        {
            _m_canInput = _canInput;
        }
    }
}
