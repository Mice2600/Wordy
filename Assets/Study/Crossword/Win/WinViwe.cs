using Base.Word;
using Study.TwoWordSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinViwe : MonoBehaviour
{
    private QuestCrossword QuestCrosswor => _QuestCrosswor ??= FindObjectOfType<QuestCrossword>();
    private QuestCrossword _QuestCrosswor;
    public GameObject SingelScorePrefab;
    void Start()
    {
        List<Content> contents = new List<Content>();
        Builder.ToBuild.AllContentIDes.ForEach(a => contents.Add(WordBase.Wordgs.GetContent(a)));
        ContentGropper LLD = GetComponentInChildren<ContentGropper>();
        contents.ForEach(a => {
            GameObject G = Instantiate(SingelScorePrefab);
            G.GetComponent<ScoreChanginInfo>().Set(a, QuestCrosswor.AddScoreWord);
            LLD.AddNewContent(G.transform);
        });
    }


    public void DestroyUrself() 
    {
        Destroy(gameObject);
        QuestCrosswor.OnFineshed.Invoke();
    }

}
