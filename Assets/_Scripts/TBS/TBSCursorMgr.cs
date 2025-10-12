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
                SCUICommon.WorldPointToUIPoint(SCGame.instance.topLayerRoot.GetRectTransform(),
                _pos);
            float chgTime = SCRefDataMgr.instance.gameGeneralRefObj.tbsTargetHighLightChgTime;
            Tween tween_scale = _m_selectionCursor.transform.DOScale(Vector3.one, chgTime);
            Tween tween_alpha = _m_selectionCursor.GetImage().DOFade(1, chgTime);
            _m_tweenContainer.RegDoTween(tween_scale);
            _m_tweenContainer.RegDoTween(tween_alpha);
        }

        public void HideSelectionCursor()
        {
            if (_m_selectionCursor == null)
                return;
            float chgTime = SCRefDataMgr.instance.gameGeneralRefObj.tbsTargetHighLightChgTime;
            Tween tween_scale = _m_selectionCursor.transform.DOScale(Vector3.one * 1.5f, chgTime);
            Tween tween_alpha = _m_selectionCursor.GetImage().DOFade(0, chgTime).OnComplete(() =>
            {
                SCCommon.SetGameObjectEnable(_m_selectionCursor, false);
            });
            _m_tweenContainer.RegDoTween(tween_scale);
            _m_tweenContainer.RegDoTween(tween_alpha);

        }
    }
}
