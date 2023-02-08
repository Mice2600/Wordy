using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Base;
using Base.Word;
using Base.Dialog;

public class EditButton : Button
{
    protected override void Start()
    {
        base.Start();
        onClick.AddListener(()=>{

            Content d = transform.GetComponentInParent<ContentObject>().Content;
            if (d == null) return;
            if (d is Word) WordChanger.StartChanging(d as Word);
            if (d is Dialog) DialogChanger.StartChanging(d as Dialog);
        });
    }
}
