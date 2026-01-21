using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameCore.OW
{
    public class MiniMapCamera : MonoBehaviour
    {
        [Header("Íæ¼ÒÄ£ÐÍ")]
        public GameObject playerModel;

        private void LateUpdate()
        {
            transform.localRotation = Quaternion.Euler(new Vector3(90, playerModel.transform.eulerAngles.y, 0));
        }
    }
}
