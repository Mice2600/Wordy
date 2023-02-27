using Base;
using Base.Dialog;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DefaultBaseFounder : MonoBehaviour
{
    public string DefaultDialogSceneName;
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
            textMesh.text = $"{Founded} dialog founded in default base";
        }
        GetComponent<Button>().onClick.AddListener(OnButton);
    }

    // Update is called once per frame
    
    public void FindContentsFromString(string ToDiagnost, System.Action<Dialog> OnFound)
    {
        StartCoroutine(FindContentsFromStringCoroutine(ToDiagnost, OnFound));
        IEnumerator FindContentsFromStringCoroutine(string Todiagnist, System.Action < Dialog > Founded)
        {
            int TCount = 0;
            for (int i = 0; i < DialogBase.DefaultBase.Count; i++)
            {
                if (DialogBase.DefaultBase[i].EnglishSource.Contains(Todiagnist, StringComparison.OrdinalIgnoreCase)) Founded?.Invoke(DialogBase.DefaultBase[i]);
                if (TCount > 100) { yield return null; TCount = 0; }
            }
        }
    }
    public void OnButton()
    {
        transform.GetComponentInParent<DiscretionViwe>().DestroyUrself();
        SceneComands.OpenSceneBaseSearch(Content.EnglishSource, DefaultDialogSceneName);
    }

}


