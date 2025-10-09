using SCFrame;
using System;
using UnityEngine;

namespace GameCore.TBS
{

    /// <summary>
    /// TBS战斗中的攻击飞行物通用脚本
    /// </summary>
    [RequireComponent(typeof(SCEventListener))]
    public class AttackFlyObj : MonoBehaviour
    {

        private GameObject _m_targetGO;

        private Action _m_onDealAttack;
        public void Initialize(GameObject _target,Action _onDealAttack)
        {
            _m_targetGO = _target;
            _m_onDealAttack = _onDealAttack;
            this.AddTriggerEnter(onTriggerEnter);
        }

        private void OnDestroy()
        {
            this.RemoveTriggerEnter(onTriggerEnter);
        }

        private void onTriggerEnter(Collider _collider, object[] _args)
        {
            if (_m_targetGO == null)
                return;
            if (_collider.gameObject == _m_targetGO)
            {
                _m_onDealAttack?.Invoke();
                //todo
                SCCommon.DestoryGameObject(gameObject);
            }
        }

    }
}
