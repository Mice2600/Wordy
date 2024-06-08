using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TMP_WordHightLighter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler, IPointerDownHandler
{


    public UnityEventWithString OnClick;
    public UnityEventWithString OnLongClick;
    public UnityEventWithString DubleClick;

    private TMP_Text M_text;
    private RectTransform rectTransform;
    private Canvas canvas;


    public float doubleClickThreshold = 0.2f; 
    

    private float lastClickTime = 0;



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



            if (Time.time - lastClickTime < doubleClickThreshold)
            {
                DubleClick.Invoke(M_text.textInfo.wordInfo[index].GetWord()); 
                lastClickTime = Time.time;
                return;
            }

            lastClickTime = Time.time; // Update last click time


            //Debug.Log(MathF.Abs(Time.time - PressedTime));
            if (MathF.Abs(Time.time - PressedTime) > .7f) OnLongClick?.Invoke(M_text.textInfo.wordInfo[index].GetWord());
            else OnClick?.Invoke(M_text.textInfo.wordInfo[index].GetWord());

        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
    }

    public void OnPointerExit(PointerEventData eventData)
    {
    }


    float PressedTime;

    public void OnPointerUp(PointerEventData eventData)
    {
       
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PressedTime = Time.time;
        
    }
}
//public class UnityEventWithString : UnityEvent<string> { }