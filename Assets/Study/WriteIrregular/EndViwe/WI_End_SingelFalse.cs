using Base;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WI_End_SingelFalse : ContentObject
{
    public TMPro.TextMeshProUGUI ScoreAdd;
    public TMPro.TextMeshProUGUI WrongText;
    public void Set(int AddScore, string WrongTextTT)
    {
        ScoreAdd.text = Math.Abs(AddScore) + "-";
        WrongText.text = WrongTextTT;
    }
}
