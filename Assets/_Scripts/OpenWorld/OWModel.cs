using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.OW
{
    public class OWModel
    {
        private DialogueInfo _m_dialogueInfo;//游戏是否开始
        public DialogueInfo dialogueInfo
        {
            get { return _m_dialogueInfo; }
            set
            {
                _m_dialogueInfo = value;
            }
        }
    }
}
