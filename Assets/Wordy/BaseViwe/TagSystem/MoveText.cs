using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[RequireComponent(typeof(TMP_Text))]
public class MoveText : OptimizedBehaver
{
    private TMP_Text TMP_Text => _TMP_Text ?? GetComponent<TMP_Text>();
    private TMP_Text _TMP_Text;
    private protected override void Start()
    {
        base.Start();
        //Text = "@Hefaw @dasdj @tsadwf @fksofh @fsodiuf @dasdjikh ";
    }
    private string Orginal;
    private string ParsedText; 
    public string Text 
    {
        get => Orginal;
        set  
        { 
            Orginal = value;
            ParsedText = value;
        }
    }

    [SerializeField]
    private int Speed;
    private protected override int PerUpdateTime => Speed;

    protected override void PerUpdate()
    {
        if (TMP_Text.GetParsedText().Contains("…"))
        {
            ParsedText += ParsedText[0];
            ParsedText = ParsedText.Remove(0, 1);
        }
        TMP_Text.text = ParsedText;
    }
}
