using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeDetector : MonoBehaviour
{
    private Vector2 fingerDownPos;
    private Vector2 fingerUpPos;

    public bool detectSwipeAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;
    bool IsMCounting = false;

    // Update is called once per frame
    void Update()
    {
        if (!Application.isEditor)
        {


            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    fingerUpPos = touch.position;
                    fingerDownPos = touch.position;
                    IsMCounting = true;
                }

                //Detects Swipe while finger is still moving on screen
                if (IsMCounting && touch.phase == TouchPhase.Moved)
                {
                    if (!detectSwipeAfterRelease)
                    {
                        fingerDownPos = touch.position;
                        DetectSwipe();
                    }
                }

                //Detects swipe after finger is released from screen
                if (touch.phase == TouchPhase.Ended)
                {
                    fingerDownPos = touch.position;
                    DetectSwipe();
                }
            }
        }
        else 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                fingerUpPos = Input.mousePosition;
                fingerDownPos = Input.mousePosition;
                IsMCounting = true;
            }
            else if (IsMCounting && Input.GetMouseButton(0) && Vector3.Distance(fingerDownPos, Input.mousePosition) > 10f )
            {
                if (!detectSwipeAfterRelease)
                {
                    fingerDownPos = Input.mousePosition;
                    DetectSwipe();
                }
            }

            if (Input.GetMouseButtonUp(0)) 
            {
                fingerDownPos = Input.mousePosition;
                DetectSwipe();
            }
        }
    }

    void DetectSwipe()
    {

        if (VerticalMoveValue() > SWIPE_THRESHOLD && VerticalMoveValue() > HorizontalMoveValue())
        {
            //Debug.Log("Vertical Swipe Detected!");
            if (fingerDownPos.y - fingerUpPos.y > 0)
            {
                OnSwipeUp();
            }
            else if (fingerDownPos.y - fingerUpPos.y < 0)
            {
                OnSwipeDown();
            }
            fingerUpPos = fingerDownPos;

        }
        else if (HorizontalMoveValue() > SWIPE_THRESHOLD && HorizontalMoveValue() > VerticalMoveValue())
        {
            //Debug.Log("Horizontal Swipe Detected!");
            if (fingerDownPos.x - fingerUpPos.x > 0)
            {
                OnSwipeRight();
            }
            else if (fingerDownPos.x - fingerUpPos.x < 0)
            {
                OnSwipeLeft();
            }
            fingerUpPos = fingerDownPos;

        }
        else
        {
            //Debug.Log("No Swipe Detected!");
        }
    }

    float VerticalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.y - fingerUpPos.y);
    }

    float HorizontalMoveValue()
    {
        return Mathf.Abs(fingerDownPos.x - fingerUpPos.x);
    }

    public System.Action OnSwipeUpAction;
    void OnSwipeUp()
    {
        OnSwipeUpAction?.Invoke();
        IsMCounting = false;
        //Do something when swiped up
    }
    public System.Action OnSwipeDownAction;
    void OnSwipeDown()
    {
        OnSwipeDownAction?.Invoke();
        IsMCounting = false;
        //Do something when swiped down
    }
    public System.Action OnSwipeLeftAction;
    void OnSwipeLeft()
    {
        OnSwipeLeftAction?.Invoke();
        IsMCounting = false;
        //Do something when swiped left
    }
    public System.Action OnSwipeRightAction;
    void OnSwipeRight()
    {
        OnSwipeRightAction?.Invoke();
        IsMCounting = false;
        //Do something when swiped right
    }
}