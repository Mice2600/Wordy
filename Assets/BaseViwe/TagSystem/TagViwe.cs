using Base.Word;
using Servises.BaseList;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;

public class TagViwe : BaseListWithFillter
{
    public TagViwe() 
    {
        TagFillterValues = new Dictionary<string, bool>();
        TagSystem.GetAllTagIdes().ForEach((a) => {TagFillterValues.Add(a, true);});
    }
    public static Dictionary<string, bool> TagFillterValues;

    public GameObject FillterViwe;
    public void OnFilterButton() 
    {
        Instantiate(FillterViwe).GetComponent<TagFilterViwe>().OnDone += () =>Lode(0);
    }
    private protected override void Start()
    {
        _AllContents = new List<Content>();
        TagSystem.GetAllTagIdes().Where((s) => TagFillterValues[s]).ForEach((id) => { TagSystem.GetAllContentsFromTag(id).ForEach((cc) => _AllContents.AddIfDirty(cc)); });
        base.Start();
    }
    public TList<Content> _AllContents;
    public override List<Content> AllContents => _AllContents;

}
