using Base;
using Base.Dialog;
using Base.Word;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;

public class CellViewTag : CellView
{
    public GameObject DialogObject;
    public GameObject WordObject;
    public GameObject IrruglarObject;
    public override void SetData(Content data)
    {
        if (data == null) Debug.Log("dd");
        ContentObject.Content = data;
        DialogObject.SetActive(data is Dialog);
        WordObject.SetActive(data is Word);
        IrruglarObject.SetActive(data is Irregular);
        queueUpdates.ForEach(action => { if ((action as MonoBehaviour).isActiveAndEnabled) action.TurnUpdate(); });
    }
}