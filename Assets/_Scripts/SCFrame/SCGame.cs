using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{

    /// <summary>
    /// 功能相当于全局Mono
    /// </summary>
    public class SCGame : SingletonPersistent<SCGame>
    {
        [Header("Player")]
        public GameObject playerGO;

        [Header("UI")]
        public Canvas mainCanvas;
        public GameObject fullLayerRoot;
        public GameObject additionLayerRoot;
        public GameObject topLayerRoot;


        [Header("Camera")]
        public Camera gameCamera;
        public Camera uiCamera;
        public CinemachineVirtualCamera virtualCamera;
        public CinemachineBrain cinemachineBrain;

    }
}
