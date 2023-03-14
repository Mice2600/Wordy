using Base.Dialog;
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
    
    public static Dictionary<string, bool> TagFillterValues 
    {
        get 
        {
            if (_TagFillterValues == null) 
            {
                _TagFillterValues = new Dictionary<string, bool>();
                TagSystem.GetAllTagIdes().ForEach((a) => { _TagFillterValues.Add(a, true); });
            }
            return _TagFillterValues;
        }
    }
    private static Dictionary<string, bool> _TagFillterValues;

    public GameObject FillterViwe;
    public void OnFilterButton() 
    {
        Instantiate(FillterViwe).GetComponent<TagFilterViwe>().OnDone += Start;
    }
    private protected override void Start()
    {
        _AllContents = new List<Content>();
        TagSystem.GetAllTagIdes().Where((s) => TagFillterValues[s]).ForEach((id) => { TagSystem.GetAllContentsFromTag(id).ForEach((cc) => _AllContents.AddIfDirty(cc)); });
        base.Start();
    }
    public TList<Content> _AllContents;
    public override List<Content> AllContents => _AllContents;

    private float CCSize;
    public override float GetSizeOfContent(Content content)
    {
        if (content is not Dialog) 
        {
            if (CCSize == 0f) CCSize = content.ContentObject.GetComponent<RectTransform>().rect.height;
            return CCSize;
        }
        return base.GetSizeOfContent(content);
    }


}
