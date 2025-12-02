using DG.Tweening;
using GameCore.Util;
using SCFrame;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore.TBS
{
    public class TBSCursorMgr : Singleton<TBSCursorMgr>
    {
        private GameObject _m_singleSelectionCursor;//单体攻击的光标

        private List<GameObject> _m_allSelectionCursorList;//全体攻击的光标列表

        private TweenContainer _m_tweenContainer;
        public override void OnInitialize()
        {
            _m_tweenContainer = new TweenContainer();
            _m_allSelectionCursorList = new List<GameObject>();
        }

        public override void OnDiscard()
        {
            _m_tweenContainer?.KillAllDoTween();
            _m_tweenContainer = null;
            _m_allSelectionCursorList.Clear();
            _m_allSelectionCursorList = null;
        }


        public void SetSelectionCursor(List<Vector3> _worldPosList,bool _needShow = true)
        {
            if (_worldPosList == null || _worldPosList.Count == 0)
                return;
            if(SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                SetSelectionCursorPos_Single(_worldPosList[0], _needShow);
            else if(SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
                SetSelectionCursorPos_All(_worldPosList, _needShow);
        }

        /// <summary>
        /// 设置光标到世界坐标转化的ui位置
        /// </summary>
        /// <param name="_worldPos"></param>
        private void SetSelectionCursorPos_Single(Vector3 _worldPos,bool _needShow = true)
        {
            if (_m_singleSelectionCursor == null)
                _m_singleSelectionCursor = ResourcesHelper.LoadGameObject(GameCommon.GetUIResObjPath(GameConst.SELECTION_CURSOR),
                    SCGame.instance.topLayerRoot.transform);
            _m_singleSelectionCursor.GetRectTransform().localPosition =
                SCUICommon.WorldPointToUIPoint(SCGame.instance.topLayerRoot.GetRectTransform(),
                _worldPos);

            if(_needShow)
                ShowSelectionCursor_Single();
        }

        private void SetSelectionCursorPos_All(List<Vector3> _worldPosList, bool _needShow = true)
        {
            if (_m_allSelectionCursorList == null)
                _m_allSelectionCursorList = new List<GameObject>();


            for(int i =0;i< _worldPosList.Count;i++)
            {
                //该位置原来就存在光标
                if (_m_allSelectionCursorList.Count > i && _m_allSelectionCursorList[i] != null)
                {
                    _m_allSelectionCursorList[i].GetRectTransform().localPosition =
                        SCUICommon.WorldPointToUIPoint(SCGame.instance.topLayerRoot.GetRectTransform(),
                        _worldPosList[i]);
                }
                else
                {
                    GameObject cursorGO = ResourcesHelper.LoadGameObject(GameCommon.GetUIResObjPath(GameConst.SELECTION_CURSOR),
                        SCGame.instance.topLayerRoot.transform);

                    cursorGO.GetRectTransform().localPosition =
                        SCUICommon.WorldPointToUIPoint(SCGame.instance.topLayerRoot.GetRectTransform(),
                        _worldPosList[i]);
                    if (_m_allSelectionCursorList.Count > i)
                        _m_allSelectionCursorList[i] = cursorGO;
                    else
                        _m_allSelectionCursorList.Add(cursorGO);
                }
            }

            if (_needShow)
                ShowSelectionCursor_All();
        }



        public void ShowSelectionCursor()
        {
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                ShowSelectionCursor_Single();
            else if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
                ShowSelectionCursor_All();
        }

        public void HideSelectionCursor()
        {
            if (SCModel.instance.tbsModel.selectTargetType == ETargetType.SINGLE)
                HideSelectionCursor_Single();
            else if (SCModel.instance.tbsModel.selectTargetType == ETargetType.ALL)
                HideSelectionCursor_All();
        }



        #region Internal
        /// <summary>
        /// 隐藏单体光标
        /// </summary>
        private void HideSelectionCursor_Single()
        {
            HideOneSelectionCursor(_m_singleSelectionCursor);
        }

        /// <summary>
        /// 显示单体光标
        /// </summary>
        private void ShowSelectionCursor_Single()
        {
            ShowOneSelectionCursor(_m_singleSelectionCursor);
        }

        private void HideSelectionCursor_All()
        {
            if (_m_allSelectionCursorList == null)
                return;

            foreach(var cursor in _m_allSelectionCursorList)
            {
                if (cursor == null)
                    continue;
                HideOneSelectionCursor(cursor);
            }
        }

        private void ShowSelectionCursor_All()
        {
            if (_m_allSelectionCursorList == null)
                return;


            foreach (var cursor in _m_allSelectionCursorList)
            {
                if (cursor == null)
                    continue;
                ShowOneSelectionCursor(cursor);
            }
        }


        private void ShowOneSelectionCursor(GameObject _cursor)
        {
            if (_cursor == null)
                return;
            float chgTime = SCRefDataMgr.instance.gameGeneralRefObj.tbsTargetHighLightChgTime;
            Tween tween_scale = _cursor.transform.DOScale(Vector3.one, chgTime);
            Tween tween_alpha = _cursor.GetImage().DOFade(1, chgTime).OnStart(() =>
            {
                SCCommon.SetGameObjectEnable(_cursor, true);
            });
            _m_tweenContainer.RegDoTween(tween_scale);
            _m_tweenContainer.RegDoTween(tween_alpha);
        }

        private void HideOneSelectionCursor(GameObject _cursor)
        {
            if (_cursor == null)
                return;
            float chgTime = SCRefDataMgr.instance.gameGeneralRefObj.tbsTargetHighLightChgTime;
            Tween tween_scale = _cursor.transform.DOScale(Vector3.one * 1.5f, chgTime);
            Tween tween_alpha = _cursor.GetImage().DOFade(0, chgTime).OnComplete(() =>
            {
                SCCommon.SetGameObjectEnable(_cursor, false);
            });
            _m_tweenContainer.RegDoTween(tween_scale);
            _m_tweenContainer.RegDoTween(tween_alpha);
        }

        #endregion



        /// <summary>
        /// 移动光标到世界坐标转化的ui位置
        /// </summary>
        /// <param name="_worldPos"></param>
        public void MoveSingleCursor2Pos(Vector3 _worldPos)
        {
            if (_m_singleSelectionCursor == null)
                return;

            Tween moveTween = _m_singleSelectionCursor.GetRectTransform().
                DOLocalMove(SCUICommon.WorldPointToUIPoint(SCGame.instance.topLayerRoot.GetRectTransform(), _worldPos), 0.2f);
            Tween scaleChgTween = _m_singleSelectionCursor.GetRectTransform().DOScale(Vector3.zero, 0.1f).OnComplete(() =>
            {
                _m_singleSelectionCursor.GetRectTransform().DOScale(Vector3.one, 0.1f);
            });
            _m_tweenContainer.RegDoTween(moveTween);
            _m_tweenContainer.RegDoTween(scaleChgTween);
        }


        public void ChangeCursorShowMode(ETargetType _targetType)
        {
            if (_m_singleSelectionCursor == null || _m_allSelectionCursorList == null)
                return;
            if (_targetType == ETargetType.SINGLE)
            {
                HideSelectionCursor_All();
                ShowSelectionCursor_Single();
            }
            else
            {
                HideSelectionCursor_Single();
                ShowSelectionCursor_All();
            }
        }
    }
}
