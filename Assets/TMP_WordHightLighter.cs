using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TMP_WordHightLighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{


    public UnityEventWithString OnClick;

    private TMP_Text M_text;
    private RectTransform rectTransform;
    private Canvas canvas;




    private bool IsHoveringObject = false;

    private void Awake()
    {
        M_text= GetComponent<TMP_Text>();   
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponent<Canvas>();
    }




    public void OnPointerClick(PointerEventData eventData)
    {


        int index = TMP_TextUtilities.FindIntersectingWord(M_text, Input.mousePosition, null);
        if (index > -1) //
        {
            OnClick?.Invoke(M_text.textInfo.wordInfo[index].GetWord());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        IsHoveringObject= true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        IsHoveringObject= false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

}
