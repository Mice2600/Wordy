using Base;
using Base.Word;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Word_Founder : MonoBehaviour
{
    public GameObject SingleWord;
    void Start()
    {
        StartCoroutine(FindContentsFromStringCoroutine(GetComponentInParent<ContentObject>().Content.EnglishSource, AddChangin));
    }
    public void AddChangin(Content content)
    {
        List<Content> ChangedContents = new List<Content>();
        if (ChangedContents.Contains(content)) return;
        ChangedContents.Add(content);
        GameObject N = Instantiate(SingleWord);
        N.GetComponent<ScoreChanginInfo>().Set(content, 0);
        var LLD = GetComponent<ContentGropper>();
        LLD.AddNewContent(N.transform);
        LLD.MaualUpdate = true;
    }

    private IEnumerator FindContentsFromStringCoroutine(string Todiagnist, System.Action<Content> Founded)
    {
        int TCount = 0;
        for (int i = 0; i < WordBase.DefaultBase.Count; i++)
        {
            if (Todiagnist.Contains(WordBase.DefaultBase[i].EnglishSource, StringComparison.OrdinalIgnoreCase)) Founded?.Invoke(WordBase.DefaultBase[i]);
            if (TCount > 100) {
                
                yield return null;

                var LLD = GetComponent<ContentGropper>();
                LLD.MaualUpdate = true;
                LLD.FixPos();

                TCount = 0; }
        }
    }
}
