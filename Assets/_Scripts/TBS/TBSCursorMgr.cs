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

        /// <summary>
        /// 设置光标到世界坐标转化的ui位置
        /// </summary>
        /// <param name="_worldPos"></param>
        public void SetSelectionCursorPos(Vector3 _worldPos,bool _needShow = true)
        {
            if (_m_selectionCursor == null)
                _m_selectionCursor = ResourcesHelper.LoadGameObject(GameCommon.GetUIResObjPath(GameConst.SELECTION_CURSOR),
                    SCGame.instance.topLayerRoot.transform);
            _m_selectionCursor.GetRectTransform().localPosition =
                SCUICommon.WorldPointToUIPoint(SCGame.instance.topLayerRoot.GetRectTransform(),
                _worldPos);

            if(_needShow)
                ShowSelectionCursor();
        }

        /// <summary>
        /// 隐藏光标
        /// </summary>
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

        public void ShowSelectionCursor()
        {
            if (_m_selectionCursor == null)
                return;
            float chgTime = SCRefDataMgr.instance.gameGeneralRefObj.tbsTargetHighLightChgTime;
            Tween tween_scale = _m_selectionCursor.transform.DOScale(Vector3.one, chgTime);
            Tween tween_alpha = _m_selectionCursor.GetImage().DOFade(1, chgTime).OnStart(() =>
            {
                SCCommon.SetGameObjectEnable(_m_selectionCursor, true);
            });
            _m_tweenContainer.RegDoTween(tween_scale);
            _m_tweenContainer.RegDoTween(tween_alpha);
        }

        
        /// <summary>
        /// 移动光标到世界坐标转化的ui位置
        /// </summary>
        /// <param name="_worldPos"></param>
        public void MoveCursor2Pos(Vector3 _worldPos)
        {
            if (_m_selectionCursor == null)
                return;

            Tween moveTween = _m_selectionCursor.GetRectTransform().
                DOLocalMove(SCUICommon.WorldPointToUIPoint(SCGame.instance.topLayerRoot.GetRectTransform(), _worldPos), 0.2f);
            Tween scaleChgTween = _m_selectionCursor.GetRectTransform().DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                _m_selectionCursor.GetRectTransform().DOScale(Vector3.one, 0.1f);
            });
            _m_tweenContainer.RegDoTween(moveTween);
            _m_tweenContainer.RegDoTween(scaleChgTween);


        }
    }
}
