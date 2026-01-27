using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.OW
{
    public class OWEntityMgr : Singleton<OWEntityMgr>
    {
        private List<_ASCLifeGameObjBase> _m_entityList;


        public override void OnInitialize()
        {
            _m_entityList = new List<_ASCLifeGameObjBase>();
        }

        public override void OnDiscard()
        {
            if(_m_entityList != null)
            {
                _ASCLifeGameObjBase entityGO = null;
                for (int i =0;i<_m_entityList.Count;i++)
                {
                    entityGO = _m_entityList[i];
                    if(entityGO != null)
                        entityGO.Discard();
                }
            }
        }

        public void RegisterEntity(_ASCLifeGameObjBase  _entity)
        {
            if (_m_entityList == null)
            {
                _m_entityList = new List<_ASCLifeGameObjBase>();
            }
            if(!_m_entityList.Contains(_entity))
                _m_entityList.Add(_entity);
        }
        public void UnRegisterEntity(_ASCLifeGameObjBase  _entity)
        {
            if (_m_entityList == null)
                return;
            if (_m_entityList.Contains(_entity))
                _m_entityList.Remove(_entity);
        }
    }
}

