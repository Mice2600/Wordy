using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Accessibility;
using Sirenix.OdinInspector;
using Base;
using Base.Word;
using Base.Dialog;
using System;


public class DiscretionObject : ContentObject
{
    public System.Action OnClose;
    public static DiscretionObject Show(Content Content, System.Action OnClose = null) 
    {

        DiscretionObject N = Instantiate(Content.DiscretioVewe, null).GetComponentInChildren<DiscretionObject>();
        N.Content = Content;
        N.OnClose = OnClose;
        return N;
    }
    public void DestroyUrself() 
    {
        Destroy(transform.root.gameObject);
        OnClose?.Invoke();
    }
}