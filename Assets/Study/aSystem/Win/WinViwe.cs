using Base.Word;
using Study.aSystem;
using Study.Crossword;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinViwe : MonoBehaviour
{
    [System.NonSerialized]
    public List<Content> contents;
    private Quest Quest => _Quest ??= FindObjectOfType<Quest>();
    private Quest _Quest;
    public GameObject SingelScorePrefab;
    void Start()
    {

        Quest d = FindObjectOfType<Quest>();
        contents.ForEach(ss => d.OnWordWin?.Invoke(ss as Word));
        d.OnGameWin?.Invoke();

        ContentGropper LLD = GetComponentInChildren<ContentGropper>();
        contents.ForEach(a =>
        {
            GameObject G = Instantiate(SingelScorePrefab);
            G.GetComponent<ScoreChanginInfo>().Set(a, (Quest.QuestData as IWordScorer).AddScoreWord);
            LLD.AddNewContent(G.transform);
        });
    }
    public void DestroyUrself()
    {
        Destroy(gameObject);
        Quest.OnFineshed.Invoke();
    }

}
