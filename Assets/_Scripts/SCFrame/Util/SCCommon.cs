using UnityEngine;
using UnityEditor;

namespace SCFrame
{
    /// <summary>
    /// 一些适用于全局的通用方法
    /// </summary>
    public static class SCCommon
    {

        /// <summary>
        /// 设置游戏物体是否被激活
        /// </summary>
        /// <param name="_obj"></param>
        /// <param name="_isEnable"></param>
        public static void SetGameObjectEnable(GameObject _obj, bool _isEnable)
        {
            if (_obj == null)
                return;
            _obj.SetActive(_isEnable);
        }

        /// <summary>
        /// 生成游戏物体
        /// </summary>
        /// <param name="_obj"></param>
        /// <returns></returns>
        public static GameObject InstantiateGameObject(GameObject _obj)
        {
            if (_obj == null)
                return null;
            GameObject go = GameObject.Instantiate(_obj);
            return go;
        }

        /// <summary>
        /// 销毁游戏物体
        /// </summary>
        /// <param name="_obj"></param>
        public static void DestoryGameObject(GameObject _obj)
        {
            if (_obj == null)
                return;
            GameObject.Destroy(_obj);
        }

        /// <summary>
        /// 编辑器当前是否处于预制体编辑模式
        /// </summary>
        /// <returns></returns>
        public static bool isInPrefabStage()
        {
            var stage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
            return stage != null;
        }

    }
}
