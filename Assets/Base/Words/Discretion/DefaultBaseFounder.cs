using Base;
using Base.Dialog;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
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
        void OnResultat(Content d)
        {
            Founded++;
            textMesh.text = $"{Founded} dialog founded in default base";
        }
        GetComponent<Button>().onClick.AddListener(OnButton);
    }

    // Update is called once per frame
    
    public void FindContentsFromString(string ToDiagnost, System.Action<Content> OnFound)
    {
        StartCoroutine(FindContentsFromStringCoroutine(ToDiagnost, OnFound));
        IEnumerator FindContentsFromStringCoroutine(string Todiagnist, System.Action < Content > Founded)
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
        transform.GetComponentInParent<DiscretionObject>().DestroyUrself();
        SceneComands.OpenSceneBaseSearch(Content.EnglishSource, DefaultDialogSceneName);
    }

}


