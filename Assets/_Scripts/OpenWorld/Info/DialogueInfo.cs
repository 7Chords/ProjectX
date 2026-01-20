using GameCore.RefData;
using System.Collections.Generic;

namespace GameCore.OW
{
    public class DialogueInfo
    {
        public List<DialogueRefObj> dialogueList;

        public DialogueInfo(List<DialogueRefObj> _refList)
        {
            dialogueList = _refList;
        }
    }
}
