using GameCore.TBS;
using SCFrame;

namespace GameCore.RefData
{
    public class BuffEffectObj : _AEffectObjBase
    {
        public long buffRefObjId;
        public int continueTurn;
        protected override void OnDeserialize(string _str)
        {
            string[] strArr = _str.Split(":");
            if (strArr == null || strArr.Length < 2)
                return;
            buffRefObjId = SCCommon.ParseLong(strArr[0]);
            continueTurn = SCCommon.ParseInt(strArr[1]);
        }

        protected override string OnSerialise()
        {
            string str = buffRefObjId + ":" + continueTurn;
            return str;
        }
    }
}
