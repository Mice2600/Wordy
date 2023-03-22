using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EnhancedUI.EnhancedScroller;

namespace EnhancedScrollerDemos.FlickSnap
{
    public class FlickSnap : MonoBehaviour
    {
        public EnhancedScroller scroller;

        public EnhancedScroller.TweenType snapTweenType;

        public float snapTweenTime;

        public int MaxDataElements { get; set; }


        private Vector3 _dragStartPosition = Vector3.zero;

        public int _currentIndex = 0;

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
            if (Mathf.Abs(delta.y) > 10f && !scroller.IsTweening) 
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
        }
    }
}

public static class TInput
{


    private static bool Starter = false;
    public static bool Is_Using_Touch = true;

    private static void Start()
    {
        if (Starter) return;
        Starter = true;
        Is_Using_Touch = true;
#if UNITY_EDITOR
        Is_Using_Touch = false;
#elif UNITY_STANDALONE
        Is_Using_Touch = false;
#endif

    }

    public static bool GetMouseButton(int Item, bool Fill_If_One = false)
    {
        Start();

        if (Is_Using_Touch)
        {
            if (Fill_If_One && Input.touchCount > 1) return false;
            if (Input.touchCount <= Item) return false;
            else
            {
                if (Input.touches[Item].phase == TouchPhase.Began) return false;
                if (Input.touches[Item].phase == TouchPhase.Ended) return false;

            }
            return true;
        }
        else return Input.GetMouseButton((Item == 0) ? 0 : 1);


    }

    public static bool GetMouseButtonDown(int Item, bool Fill_If_One = false)
    {
        Start();

        if (Is_Using_Touch)
        {
            if (Fill_If_One && Input.touchCount > 1) return false;
            if (Input.touchCount <= Item) return false;
            else
            {
                if (Input.touches[Item].phase == TouchPhase.Began) return true;
                if (Input.touches[Item].phase == TouchPhase.Ended) return false;
            }
            return false;
        }
        else return Input.GetMouseButtonDown((Item == 0) ? 0 : 1);

    }
    public static bool GetMouseButtonUp(int Item, bool Fill_If_One = false)
    {
        Start();

        if (Is_Using_Touch)
        {
            if (Fill_If_One && Input.touchCount > 1) return false;
            if (Input.touchCount <= Item) return false;
            else
            {
                if (Input.touches[Item].phase == TouchPhase.Began) return false;
                if (Input.touches[Item].phase == TouchPhase.Ended) return true;
            }
            return false;
        }
        else
        {
            return Input.GetMouseButtonUp((Item == 0) ? 0 : 1);
        }

    }

    public static Vector3 mousePosition(int Item)
    {
        if (Is_Using_Touch)
        {
            if (Input.touches.Length > 0) return Input.touches[Item].position;
            return Input.mousePosition;
        }
        return Input.mousePosition;
    }

    public static Vector3 mouseWorldPoint(int Item)
    {
        if (Is_Using_Touch)
        {
            if (Input.touches.Length > 0) return Camera.main.ScreenToWorldPoint(Input.touches[Item].position);
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    public static Vector3 mouseWorldPoint(int Item, Camera camera)
    {
        if (Is_Using_Touch)
        {
            if (Input.touches.Length > 0) return camera.ScreenToWorldPoint(Input.touches[Item].position);
            return camera.ScreenToWorldPoint(Input.mousePosition);
        }
        return camera.ScreenToWorldPoint(Input.mousePosition);
    }

    public static bool IsPointerOverGameObject()
    {
        // Check mouse
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return true;
        }

        // Check touches
        for (int i = 0; i < Input.touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return true;
                }
            }
        }

        return false;
    }

}