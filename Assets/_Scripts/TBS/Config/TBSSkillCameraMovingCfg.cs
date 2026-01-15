using Cinemachine;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.TBS
{

    [System.Serializable]
    public class TBSSkillCameraMovingItem
    {
        [HideInInspector]public Transform lookAt;
        [HideInInspector]public Transform follow;
        [HideInInspector]public Vector3 offset;
        [HideInInspector]public float offsetTranslateDuration;
        
    }
    [CreateAssetMenu(fileName = "new TBSSkillCameraMovingCfg", menuName = "MBC≈‰÷√/TBS/TBSSkillCameraMovingCfg")]
    public class TBSSkillCameraMovingCfg : ScriptableObject
    {
        public List<TBSSkillCameraMovingItem> skillCameraMovingItemList;
    }
}
