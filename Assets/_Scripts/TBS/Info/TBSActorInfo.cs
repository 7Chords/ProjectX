using GameCore.RefData;
using SCFrame;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSActorInfo
    {

        public string characterName;

        public EProfessionType professionType;

        public string assetGroupName;
        public string assetObjName;


        public void InitNewInfo(CharacterRefObj _characterRefObj)
        {
            characterName = _characterRefObj.characterName;
            ProfessionRefObj professioRefObj = SCRefDataMgr.instance.professionRefList.refDataList.Find(x => x.id == _characterRefObj.characterProfession);
            if(professioRefObj == null)
            {
                Debug.LogError("¶ÁÈ¡professioRefObjÊ±³ö´í£¡£¡£¡");
                return;
            }
            assetGroupName = _characterRefObj.assetGroupName;
            assetObjName = _characterRefObj.assetObjName;
            professionType = professioRefObj.professionType;
        }
    }
}
