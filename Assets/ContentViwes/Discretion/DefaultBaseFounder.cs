using Base;
using Base.Dialog;
using BaseViwe.DialogViwe;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using SystemBox.Engine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefaultBaseFounder : MonoBehaviour
{
    public TextMeshProUGUI textMesh;
    void Start()
    {
        int Founded = 0;
        FindContentsFromString(transform.GetComponentInParent<ContentObject>().Content.EnglishSource, OnResultat);
        void OnResultat(Dialog d)
        {
            Founded++;
            textMesh.text = $"{Founded} dialog founded in default base";
        }
    }

    // Update is called once per frame
    
    public void FindContentsFromString(string ToDiagnost, System.Action<Dialog> OnFound)
    {

        MonoBehaviour s = GameObject.FindObjectOfType<MonoBehaviour>();
        s.StartCoroutine(FindContentsFromStringCoroutine(ToDiagnost, OnFound));
        IEnumerator FindContentsFromStringCoroutine(string Todiagnist, System.Action < Dialog > Founded)
        {
            int TCount = 0;
            for (int i = 0; i < DialogBase.DefaultBase.Count; i++)
            {
                if (Todiagnist.Contains(DialogBase.DefaultBase[i].EnglishSource, StringComparison.OrdinalIgnoreCase)) Founded?.Invoke(DialogBase.DefaultBase[i]);
                if (TCount > 100) { yield return null; TCount = 0; Debug.Log("sa"); }
            }
        }
    }

}

public static partial class SceneComands // WordBaseViwe 
{
    
}


