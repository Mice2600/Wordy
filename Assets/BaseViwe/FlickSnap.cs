﻿using System.Collections;
using UnityEngine;
using EnhancedUI.EnhancedScroller;
using Servises.BaseList;
using UnityEngine.UIElements;

namespace EnhancedScrollerDemos.FlickSnap
{
    [RequireComponent(typeof(EnhancedScroller))]
    public class FlickSnap : MonoBehaviour
    {
        private EnhancedScroller scroller => _scroller ??= GetComponent<EnhancedScroller>();
        private EnhancedScroller _scroller;
        public System.Action<BaseListViwe> OnNewViweOpend;
        public EnhancedScroller.TweenType snapTweenType;

        public float snapTweenTime;

        public int MaxDataElements { get; set; }


        private Vector3 _dragStartPosition = Vector3.zero;

        public int _currentIndex = 0;

        private void Start()
        {
            StartCoroutine(AfterFrem());
            IEnumerator AfterFrem() 
            {
                yield return new WaitUntil(() => scroller.GetCellViewAtDataIndex(0) != null);
                OnNewViweOpend?.Invoke(scroller.GetCellViewAtDataIndex(0).GetComponent<BaseListViwe>());
            }
        }

        public void OnBeginDrag()
        {
            _dragStartPosition = TInput.mousePosition(0);
        }
        private void Update()
        {
            if(TInput.GetMouseButtonDown(0, true)) OnBeginDrag();
            if(TInput.GetMouseButtonUp(0, true)) OnEndDrag();

        }
        public void OnEndDrag()
        {
            Vector3 delta = TInput.mousePosition(0) - _dragStartPosition;
            int OldIndex = _currentIndex;

            if (Vector2.Distance(_dragStartPosition, TInput.mousePosition(0)) < 100f ||
                (Mathf.Abs(delta.y) > Mathf.Abs(delta.x) && !scroller.IsTweening)) 
            {
                scroller.JumpToDataIndex(_currentIndex, tweenType: snapTweenType, tweenTime: snapTweenTime);
                return; 
            }


            if (delta.x < 0)
            {
                _currentIndex = Mathf.Clamp(_currentIndex + 1, 0, MaxDataElements - 1);
            }
            else if (delta.x > 0)
            {
                _currentIndex = Mathf.Clamp(_currentIndex - 1, 0, MaxDataElements - 1);
            }
            
            scroller.JumpToDataIndex(_currentIndex, tweenType: snapTweenType, tweenTime: snapTweenTime);
            
            if (_currentIndex != OldIndex) StartCoroutine(WaitUntilActivation());
            IEnumerator WaitUntilActivation() 
            {
                yield return new WaitUntil(()=> scroller.GetCellViewAtDataIndex(_currentIndex) != null);
                OnNewViweOpend?.Invoke(scroller.GetCellViewAtDataIndex(_currentIndex).GetComponent<BaseListViwe>());
            }

            
        }


        

    }
}
