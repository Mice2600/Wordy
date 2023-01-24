using Base;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscretionResultat : DiscretionViwe
{
    [SerializeField]
    [Required]
    private ContentGropper ScoreContentGropper;
    [SerializeField]
    [Required]
    private GameObject ChengingContentObject;
    private List<Content> ChangedContents;
    public void AddChangin(Content content, int ChangedScore) 
    {
        if (ChangedContents == null) ChangedContents = new List<Content>();
        if (ChangedContents.Contains(content)) return;
        ChangedContents.Add(content);
        GameObject N = Instantiate(ChengingContentObject, transform.root);
        N.GetComponent<ScoreChanginInfo>().Set(content, ChangedScore);
        ScoreContentGropper.AddNewContent(N.transform);
    }
    public void AddChangin(List<(Content content, int ChangedScore)> values) => values.ForEach(v => { AddChangin(v.content, v.ChangedScore); });
}
