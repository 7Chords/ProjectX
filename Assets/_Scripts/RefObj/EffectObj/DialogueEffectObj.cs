using SCFrame;
using System.Collections.Generic;

namespace GameCore.RefData
{
    public class DialogueEffectObj : _AEffectObjBase
    {
        public EDialogueEffectType effectType;
        public List<object> effectParamList;
        protected override void OnDeserialize(string _str)
        {
            string[] strArr = _str.Split(":");
            if (strArr == null || strArr.Length < 2)
                return;
            effectType = (EDialogueEffectType)SCCommon.ParseEnum(strArr[0], typeof(EDialogueEffectType));
            effectParamList = SCCommon.ParseList<object>(strArr[1]);
        }

        protected override string OnSerialise()
        {
            string str = effectType.ToString();
            foreach (var obj in effectParamList)
            {
                str += obj;
            }
            return str;
        }
    }
}
