using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SCFrame.TBS
{
    public class TBSModel
    {
        public ETBSTurnType _m_curTurnType;

        public ETBSTurnType curTurnType
        {
            get { return _m_curTurnType; }
            set { _m_curTurnType = value; }
        }

        public int _m_curTurnCount;

        public int curTurnCount
        {
            get { return _m_curTurnCount; }
            set { _m_curTurnCount = value; }
        }


    }
}
