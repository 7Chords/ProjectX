using SCFrame;
using UnityEngine;

namespace GameCore
{
    public class GameInit : MonoBehaviour
    {

        public void Start()
        {
            SCSystem.instance.Initialize();
            SCPlayer.instance.Initialize();
        }
    }
}
