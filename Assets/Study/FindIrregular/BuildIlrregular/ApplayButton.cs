using Servises;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ApplayButton : MonoBehaviour
{
    private BuildIlrregularSystem WriteIrregularSystem;
    private TMP_Text text => _text ??= GetComponentInChildren<TMP_Text>();
    private TMP_Text _text;
    private ColorChanger ColorChanger => _ColorChanger ??= GetComponentInChildren<ColorChanger>();
    private ColorChanger _ColorChanger;
    public Color ColorChooseOne;
    public Color ColorApplay;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButton);
        WriteIrregularSystem = GetComponentInParent<BuildIlrregularSystem>();
    }
    void Update()
    {
        List<Transform> Lis = WriteIrregularSystem.ContentParrent.Childs();
        for (int i = 0; i < Lis.Count; i++)
        {
            if (!Lis[i].gameObject.GetComponent<BI_Content>().IsEvrethingWroten)
            {
                text.text = "Write";
                ColorChanger.SetColor(ColorChooseOne);
                return;
            }
        }
        text.text = "Applay";
        ColorChanger.SetColor(ColorApplay);
    }
    private bool Done;
    public void OnButton()
    {
        List<Transform> Lis = WriteIrregularSystem.ContentParrent.Childs();
        for (int i = 0; i < Lis.Count; i++)
        {
            if (!Lis[i].gameObject.GetComponent<BI_Content>().IsEvrethingWroten)
            {
                return;
            }
        }
        if (Done) return;
        Done = true;
        WriteIrregularSystem.TrayToComplate();
    }
}