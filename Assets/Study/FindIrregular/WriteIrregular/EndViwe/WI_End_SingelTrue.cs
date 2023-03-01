using Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WI_End_SingelTrue : ContentObject
{
    public TMPro.TextMeshProUGUI ScoreAdd;
    public void Set(int AddScore) 
    {
        ScoreAdd.text = Math.Abs(AddScore) + "+";
    }
}
