using Cinemachine;
using GameCore.OW;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SCFrame
{

    /// <summary>
    /// 功能相当于全局Mono
    /// </summary>
    public class SCGame : SingletonPersistent<SCGame>
    {
        [Header("Player")]
        public GameObject playerGO;
        public PlayerMono playerMono;

        [Header("UI")]
        public Canvas mainCanvas;
        public GameObject fullLayerRoot;
        public GameObject additionLayerRoot;
        public GameObject topLayerRoot;
        public Transform tranTipPoint;
        public Transform tranSkillNamePoint;

        [Header("Camera")]
        public Camera gameCamera;
        public Camera uiCamera;
        public Camera tbsDetailCamera;
        public CinemachineVirtualCamera virtualCamera;
        public CinemachineFreeLook owCamera;

        public CinemachineBrain cinemachineBrain;
        public CinemachineImpulseSource cinemachineImpulseSource;

    }
}
