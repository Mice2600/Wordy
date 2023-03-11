using Base;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public class ContentTagViwe : MonoBehaviour
{
    private ContentObject ContentObject => _ContentObject ??= transform.GetComponentInParent<ContentObject>();
    private ContentObject _ContentObject;
    private MoveText MoveText => _MoveText ??= transform.GetComponentInChildren<MoveText>();
    private MoveText _MoveText;

    private void Start()
    {
        ContentObject.OnValueChanged += Change;
        Change(ContentObject.Content);
    }
    private void Change(Content content) 
    {
        TList<string> Tages = TagSystem.GetBlongTags(content.EnglishSource);
        string Resolt = " ";
        for (int i = 0; i < Tages.Count; i++) Resolt += Tages[i] + " ";
        Resolt = Resolt.Replace("@", " @");
        MoveText.Text = Resolt;
    }
    public void onButton() 
    {
        TagContentList.Open(ContentObject.Content, () => { Change(ContentObject.Content); });
    }


}
