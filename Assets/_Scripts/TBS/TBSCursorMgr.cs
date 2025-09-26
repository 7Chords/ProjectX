using DG.Tweening;
using GameCore.Util;
using SCFrame;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSCursorMgr : Singleton<TBSCursorMgr>
    {
        private GameObject _m_selectionCursor;

        private TweenContainer _m_tweenContainer;

        public override void OnInitialize()
        {
            _m_tweenContainer = new TweenContainer();
        }

        public override void OnDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
        }

        public void SetSelectionCursorPos(Vector3 _pos)
        {
            if (_m_selectionCursor == null)
                _m_selectionCursor = ResourcesHelper.LoadGameObject("selection_cursor",SCGame.instance.topLayerRoot.transform);
            SCCommon.SetGameObjectEnable(_m_selectionCursor,true);
            _m_selectionCursor.GetRectTransform().localPosition =
                SCUICommon.WorldPointToUIPoint(_m_selectionCursor.GetRectTransform(),
                _pos);

            Tween tween = _m_selectionCursor.GetImage().DOFade(1, 0.5f).OnStart(() =>
            {
                _m_selectionCursor.GetImage().color = new Color(1, 1, 1, 0);
            });
            _m_tweenContainer?.RegDoTween(tween);
        }

        public void HideSelectionCursor()
        {
            if (_m_selectionCursor == null)
                return;
            SCCommon.SetGameObjectEnable(_m_selectionCursor, false);
        }
    }
}
