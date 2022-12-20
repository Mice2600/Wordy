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
    public static void ShowWord(Word word, InspectorButtons InspectorButtons) 
    {

        DiscretionViwe N = Instantiate(ProjectSettings.ProjectSettings.Mine.DiscretionViwe, null).GetComponentInChildren<DiscretionViwe>();
        N.Content = word;
        if(N.RemoveButton != null) N.RemoveButton.SetActive(InspectorButtons.HasFlag(InspectorButtons.RemoveButton));
        if (N.SoundButton != null) N.SoundButton.SetActive(InspectorButtons.HasFlag(InspectorButtons.SoundButton));
        if (N.Score != null) N.Score.SetActive(InspectorButtons.HasFlag(InspectorButtons.Score));
        if (N.EditButton != null) N.EditButton.SetActive(InspectorButtons.HasFlag(InspectorButtons.EditButton));
    }
    public static void ShowDialog(Dialog dialog, InspectorButtons InspectorButtons)
    {
        DiscretionViwe N = Instantiate(ProjectSettings.ProjectSettings.Mine.DiscretionDialog, null).GetComponentInChildren<DiscretionViwe>();
        N.Content = dialog;
        if (N.RemoveButton != null) N.RemoveButton.SetActive(InspectorButtons.HasFlag(InspectorButtons.RemoveButton));
        if (N.SoundButton != null) N.SoundButton.SetActive(InspectorButtons.HasFlag(InspectorButtons.SoundButton));
        if (N.Score != null) N.Score.SetActive(InspectorButtons.HasFlag(InspectorButtons.Score));
        if (N.EditButton != null) N.EditButton.SetActive(InspectorButtons.HasFlag(InspectorButtons.EditButton));
    }
    public void DestroyUrself() 
    {
        Destroy(transform.root.gameObject);
    }
    
    public GameObject SoundButton, RemoveButton, Score, EditButton;
}
public enum InspectorButtons 
{
    SoundButton, RemoveButton, Score, EditButton
}