using GameCore.RefData;
using GameCore.TBS;
using SCFrame;
using System.Collections.Generic;

namespace GameCore
{
    public class SCDataMgr : Singleton<SCDataMgr>
    {
        public List<ItemData> itemDataList;
        public List<TBSActorInfo> playerActorInfoList;
        public long money;
        public Dictionary<long, StoreData> storeDataDict;
        public override void OnInitialize()
        {
            itemDataList = SCSaveSys.instance.gameData.itemDataList;
            //todo:测试道具面板
            playerActorInfoList = new List<TBSActorInfo>();
            for(int i =0;i<SCSaveSys.instance.gameData.playerActorDataList.Count;i++)
            {
                TBSActorInfo info = new TBSActorInfo();
                info.Init(SCSaveSys.instance.gameData.playerActorDataList[i],false);
                playerActorInfoList.Add(info);
            }
            money = 1000;

            storeDataDict = SCSaveSys.instance.gameData.storeDataDict;
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
                data.itemAmount += _itemAmount;

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

        public bool AddCharacter(long _characterId)
        {
            if (playerActorInfoList.Find(x => x.characterRefObj.id == _characterId) != null)
                return false;
            TBSActorInfo info = new TBSActorInfo();
            ActorData data = new ActorData();
            data.InitNew(_characterId,1);
            info.Init(data, false);
            playerActorInfoList.Add(info);
            return true;
        }

        public bool RemoveCharacter(long _characterId)
        {
            return false;
        }

        public bool UseMoney(long _money)
        {
            if (money < _money)
                return false;
            money -= _money;
            return true;
        }

        public bool GetMoney(long _money)
        {
            money += _money;
            return true;
        }


        public bool GetExp(long _characterId,int _exp)
        {
            List<bool> hasLevelUpStateList = new List<bool>();
            TBSActorInfo info = playerActorInfoList.Find(x => x.characterRefObj.id == _characterId);
            if (info == null)
            {
                SCDebugHelper.LogError("没有找到id为" + _characterId + "的角色信息！！！");
                return false;
            }

            //更新经验和等级相关信息
            info.curExp += _exp;

            bool res = false;
            if (info.curExp >= info.levelFullExp)
                res = true;
            else
                res = false;

            while (info.curExp >= info.levelFullExp)
            {
                if (info.characterLv == GameConst.CHARACTER_MAX_LEVEL)
                    break;
                info.characterLv++;
                info.curExp -= info.levelFullExp;
                LevelRefObj levelRefObj = SCRefDataMgr.instance.levelRefList.refDataList.Find(x => (x.characterId == _characterId && x.characterLevel == info.characterLv));
                info.levelFullExp = levelRefObj.needExpToNextLevel;
            }
            info.ResetDataByLevel();

            return res;

        }

    }


}