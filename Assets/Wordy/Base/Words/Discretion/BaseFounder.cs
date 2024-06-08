using Base;
using Base.Dialog;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class BaseFounder : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    private Content Content;
    void Start()
    {
        int Founded = 0;
        Content = transform.GetComponentInParent<ContentObject>().Content;
        FindContentsFromString(Content.EnglishSource, OnResultat);
        void OnResultat(Dialog d)
        {
            Founded++;
            textMesh.text = $"{Founded} dialog founded in base";
        }
        GetComponent<Button>().onClick.AddListener(OnButton);
    }
    public void FindContentsFromString(string ToDiagnost, System.Action<Dialog> OnFound)
    {
        StartCoroutine(FindContentsFromStringCoroutine(ToDiagnost, OnFound));
        IEnumerator FindContentsFromStringCoroutine(string Todiagnist, System.Action<Dialog> Founded)
        {
            int TCount = 0;
            for (int i = 0; i < DialogBase.Dialogs.Count; i++)
            {
                if (DialogBase.Dialogs[i].EnglishSource.Contains(Todiagnist, StringComparison.OrdinalIgnoreCase)) Founded?.Invoke(DialogBase.Dialogs[i]);
                if (TCount > 100) { yield return null; TCount = 0; }
            }
        }
    }
    public void OnButton()
    {
        transform.GetComponentInParent<DiscretionObject>().DestroyUrself();
        
    }
}
