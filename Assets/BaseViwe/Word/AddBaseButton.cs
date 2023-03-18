using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBaseButton : OptimizedBehaver, IQueueUpdate
{
    private ContentObject Content
    {
        get
        {
            if (_Content != null) return _Content;
            _Content = transform.GetComponentInParent<ContentObject>();
            return _Content;
        }
    }
    private ContentObject _Content;

    private protected override void Start()
    {
        base.Start();
        GetComponent<Button>().onClick.AddListener(() =>
        {
            Content.Content.BaseCommander.Add(Content.Content);
        });
    }
    public void TurnUpdate()
    {
        if (Content.Content == null) 
        {
            gameObject.SetActive(false);
            return; 
        }
        gameObject.SetActive(!Content.Content.BaseCommander.Contains(Content.Content));
    }
}
