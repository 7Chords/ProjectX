using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCFrame
{
    /// <summary>
    /// 封装一些UI通用方法
    /// </summary>
    public static class SCUICommon
    {
        public static Vector2 ScreenPointToUIPoint(RectTransform _rt, Vector2 _screenPoint)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rt.parent as RectTransform,
                _screenPoint,
                null,
                out localPoint
            );
            return localPoint;
        }

        public static Vector2 WorldPointToUIPoint(RectTransform _rt, Vector3 _worldPoint)
        {
            //Vector2 screenPoint = SCGame.instance.virtualCamera.VirtualCameraGameObject.GetComponent<Camera>().WorldToScreenPoint(_worldPoint);
            Vector2 screenPoint = SCGame.instance.gameCamera.WorldToScreenPoint(_worldPoint);
            return ScreenPointToUIPoint(_rt, screenPoint);
        }


    }
}
