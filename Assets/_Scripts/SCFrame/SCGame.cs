using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    public class SCGame : SingletonPersistent<SCGame>
    {
        public Canvas mainCanvas;

        public Camera gameCamera;

        public Camera uiCamera;
    }
}
