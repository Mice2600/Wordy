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

namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [Required]
        public GameObject DiscretionViwe;
        [Required]
        public GameObject DiscretionDialog;
    }
}
public class DiscretionViwe : ContentObject
{
    protected private System.Action OnClose;
    public static void ShowWord(Word word, System.Action OnClose = null) 
    {

        DiscretionViwe N = Instantiate(ProjectSettings.ProjectSettings.Mine.DiscretionViwe, null).GetComponentInChildren<DiscretionViwe>();
        N.Content = word;
        N.OnClose = OnClose;
       
    }
    public static void ShowDialog(Dialog dialog, System.Action OnClose = null)
    {
        DiscretionViwe N = Instantiate(ProjectSettings.ProjectSettings.Mine.DiscretionDialog, null).GetComponentInChildren<DiscretionViwe>();
        N.Content = dialog;
        N.OnClose = OnClose;
        
    }
    public void DestroyUrself() 
    {
        Destroy(transform.root.gameObject);
        OnClose?.Invoke();
    }
    
    public GameObject SoundButton, RemoveButton, Score, EditButton;
}