using Base;
using Servises;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreChanginInfo : ContentObject
{
    [SerializeField]private Color NiceColor;
    [SerializeField]private Color BadColor;
    [SerializeField]private TMP_Text ScoreText;
    public void Set(Content content, int Score) 
    {
        this.Content = content;
        ScoreText.text = (Score > 0 ? "+" : "-") + Mathf.Abs(Score);
        if (Score == 0) ScoreText.text = "";
        if(Score != 0) GetComponent<ColorChanger>().SetColor(Score > 0 ? NiceColor : BadColor);
    }

}
