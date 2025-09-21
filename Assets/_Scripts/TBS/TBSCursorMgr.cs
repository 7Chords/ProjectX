using SCFrame;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSCursorMgr : Singleton<TBSCursorMgr>
    {
        private GameObject _m_selectionCuror;

        public void SetSelectionCursorPos(Transform _target)
        {
            if (_m_selectionCuror == null)
                _m_selectionCuror = ResourcesHelper.LoadGameObject("selection_cursor",SCGame.instance.topLayerRoot.transform);
            SCCommon.SetGameObjectEnable(_m_selectionCuror,true);
            _m_selectionCuror.GetRectTransform().localPosition =
                SCUICommon.WorldPointToUIPoint(_m_selectionCuror.GetRectTransform(),
                _target.transform.position);
        }

        public void HideSelectionCursor()
        {
            if (_m_selectionCuror == null)
                return;
            SCCommon.SetGameObjectEnable(_m_selectionCuror, false);
        }
    }
}
