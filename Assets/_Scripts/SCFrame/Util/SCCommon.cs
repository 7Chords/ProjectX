using UnityEngine;

namespace SCFrame
{
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
    }
}
