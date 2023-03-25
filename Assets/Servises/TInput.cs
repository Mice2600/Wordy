using UnityEngine;
using UnityEngine.EventSystems;

public static class TInput
{
    public static bool Is_Using_Touch = true;
    static TInput() 
    {
        Is_Using_Touch = true;
#if UNITY_EDITOR
        Is_Using_Touch = false;
#elif UNITY_STANDALONE
        Is_Using_Touch = false;
#endif
    }


    public static bool GetMouseButton(int Item, bool Fill_If_One = false)
    {
        if (InputBlocker.Blockers.Count > 0) return false;

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
        if (InputBlocker.Blockers.Count > 0) return false;

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
        if (InputBlocker.Blockers.Count > 0) return false;

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