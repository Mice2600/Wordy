using Base.Word;
using Servises;
using Study.FindOns;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ApplayButton : MonoBehaviour
{
    private WriteWordSystem WriteWordSystem;
    private TMP_Text text => _text ??= GetComponentInChildren<TMP_Text>();
    private TMP_Text _text;
    private ColorChanger ColorChanger => _ColorChanger ??= GetComponentInChildren<ColorChanger>();
    private ColorChanger _ColorChanger;
    public Color ColorChooseOne;
    public Color ColorApplay;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButton);
        WriteWordSystem = GetComponentInParent<WriteWordSystem>();
    }
    void Update()
    {
        if (string.IsNullOrEmpty(WrotenText))
        {
            text.text = "Write";
            ColorChanger.SetColor(ColorChooseOne);
        }
        else
        {
            text.text = "Applay";
            ColorChanger.SetColor(ColorApplay);
        }
    }
    public string WrotenText;
    public void OnStringChanged(string Value) 
    {
        WrotenText = Value;
    }
    private bool Done;
    public void OnButton()
    {
        if (string.IsNullOrEmpty(WrotenText)) return;
        if (Done) return;
        Done = true;
        
        WriteWordSystem.TrayToComplate(WrotenText.ToUpper());
    }
}
