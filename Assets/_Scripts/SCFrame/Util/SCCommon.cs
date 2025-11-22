using UnityEngine;
using UnityEditor;
using System.Globalization;
using System.Collections.Generic;

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
        /// 设置游戏物体是否被激活
        /// </summary>
        /// <param name="_objs"></param>
        /// <param name="_isEnable"></param>
        public static void SetGameObjectEnable(GameObject[] _objs, bool _isEnable)
        {
            if (_objs == null || _objs.Length == 0)
                return;

            foreach(var go in _objs)
                go.SetActive(_isEnable);
        }

        /// <summary>
        /// 设置游戏物体是否被激活
        /// </summary>
        /// <param name="_objs"></param>
        /// <param name="_isEnable"></param>
        public static void SetGameObjectEnable(List<GameObject> _objs, bool _isEnable)
        {
            if (_objs == null || _objs.Count == 0)
                return;

            foreach (var go in _objs)
                go.SetActive(_isEnable);
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
        //public static bool IsInPrefabStage()
        //{
        //    var stage = UnityEditor.SceneManagement.PrefabStageUtility.GetCurrentPrefabStage();
        //    return stage != null;
        //}

        /// <summary>
        /// 字符串解析成float
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        public static float ParseFloat(string _str)
        {
            if(float.TryParse(_str, NumberStyles.Float, CultureInfo.InvariantCulture, out float result))
            {
                return result;
            }
            Debug.LogError("string解析成float出错！！！");
            return 0f;
        }

        /// <summary>
        /// 字符串解析成int
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        public static int ParseInt(string _str)
        {
            if(int.TryParse(_str, NumberStyles.Float, CultureInfo.InvariantCulture, out int result))
            {
                return result;
            }
            Debug.LogError("string解析成int出错！！！");
            return 0;
        }

    }
}
