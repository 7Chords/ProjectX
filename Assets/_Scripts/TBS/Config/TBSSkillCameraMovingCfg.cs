using Cinemachine;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.TBS
{
    [System.Serializable]
    public class TBSSkillCameraMovingItem
    {
        public float timestamp;
        public Transform target;
        public Transform follow;
        public Vector3 offset;
        public CinemachineBlendDefinition.Style blendStyle;
        
    }
    [CreateAssetMenu(fileName = "new TBSSkillCameraMovingCfg", menuName = "MBC≈‰÷√/TBS/TBSSkillCameraMovingCfg")]
    public class TBSSkillCameraMovingCfg : ScriptableObject
    {
        public List<TBSSkillCameraMovingItem> skillCameraMovingItemList;
    }
}
