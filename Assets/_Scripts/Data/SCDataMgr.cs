using SCFrame;
using System.Collections.Generic;

namespace GameCore
{
    public class SCDataMgr : Singleton<SCDataMgr>
    {
        public List<ItemData> itemDataList;


        public override void OnInitialize()
        {
            itemDataList = SCSaveSys.instance.gameData.itemDataList;
        }

        public override void OnDiscard()
        {
            
        }



    }


}