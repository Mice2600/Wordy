using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ScrollBar : UnityEngine.UI.Scrollbar
{

    public bool IsDraging;
    public System.Action OnBeginDraging;
    public System.Action OnEndDraging;
    public override void OnBeginDrag(PointerEventData eventData)
    {
        base.OnBeginDrag(eventData);
        IsDraging = true;
        OnBeginDraging?.Invoke();
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        IsDraging = false;
        OnEndDraging?.Invoke();
    }


}
