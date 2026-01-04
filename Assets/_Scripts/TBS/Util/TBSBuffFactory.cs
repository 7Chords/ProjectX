using GameCore.RefData;
using SCFrame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public static class TBSBuffFactory
    {
        public static TBSGameBuffInfo CreateBuffInfo(long _buffId)
        {
            TBSBuffRefObj buffRefObj = SCRefDataMgr.instance.tbsBuffRefList.refDataList.Find(x => x.id == _buffId);
            if(buffRefObj == null)
            {
                SCDebugHelper.LogError("找不到id为" + _buffId + "的buff配表数据！！！");
                return null;
            }
            return null;
        }



    }

}