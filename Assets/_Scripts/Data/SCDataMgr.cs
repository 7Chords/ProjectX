using GameCore.RefData;
using GameCore.TBS;
using SCFrame;
using System.Collections.Generic;

namespace GameCore
{
    public class SCDataMgr : Singleton<SCDataMgr>
    {
        public List<ItemData> itemDataList;
        public List<TBSActorInfo> playerActorInfo;

        public override void OnInitialize()
        {
            itemDataList = SCSaveSys.instance.gameData.itemDataList;
            //todo:测试道具面板
            playerActorInfo = new List<TBSActorInfo>();
            for(int i =0;i<SCSaveSys.instance.gameData.playerActorDataList.Count;i++)
            {
                TBSActorInfo info = new TBSActorInfo();
                info.Init(SCSaveSys.instance.gameData.playerActorDataList[i],false);
                playerActorInfo.Add(info);
            }
        }

        public override void OnDiscard()
        {
            
        }

        //todo:临时写法
        public void GetItem(long _itemId,long _itemAmount)
        {
            ItemRefObj refObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == _itemId);
            if(refObj == null)
            {
                SCDebugHelper.LogError("没有找到id为" + _itemId + "的物品");
                return;
            }
            ItemData data = itemDataList.Find(x => x.itemId == _itemId);
            if (data == null)
                itemDataList.Add(new ItemData(_itemId, _itemAmount));
            else
                data.itemAmount = _itemAmount;

            SCDebugHelper.Log("获得了"+ LanguageHelper.instance.GetTextTranslate(refObj.itemName)+"×"+_itemAmount);
        }

        public void DeleteItem(long _itemId,long _itemAmount)
        {
            ItemRefObj refObj = SCRefDataMgr.instance.itemRefList.refDataList.Find(x => x.id == _itemId);
            if (refObj == null)
            {
                SCDebugHelper.LogError("没有找到id为" + _itemId + "的物品");
                return;
            }
            ItemData data = itemDataList.Find(x => x.itemId == _itemId);
            if (data == null)
            {
                SCDebugHelper.LogError("背包里没有id为" + _itemId + "的物品");
                return;
            }
            else
            {
                data.itemAmount = System.Math.Max(data.itemAmount - _itemAmount, 0L);
                if(data.itemAmount == 0)
                {
                    itemDataList.Remove(data);
                }
            }

            SCDebugHelper.Log("删除了" + LanguageHelper.instance.GetTextTranslate(refObj.itemName) + "×" + _itemAmount);
        }



    }


}