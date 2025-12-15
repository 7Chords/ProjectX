using GameCore.TBS;
using SCFrame;

namespace GameCore.RefData
{
    public class BuffEffectObj : _AEffectObjBase
    {
        public long buffRefObjId;
        public int buffChgLevel;
        public int continueTurn;
        protected override void OnDeserialize(string _str)
        {
            string[] strArr = _str.Split(":");
            if (strArr == null || strArr.Length < 3)
                return;
            buffRefObjId = SCCommon.ParseLong(strArr[0]);
            buffChgLevel = SCCommon.ParseInt(strArr[1]);
            continueTurn = SCCommon.ParseInt(strArr[2]);
        }

        protected override string OnSerialise()
        {
            string str = buffRefObjId + ":" + continueTurn;
            return str;
        }
    }
}
