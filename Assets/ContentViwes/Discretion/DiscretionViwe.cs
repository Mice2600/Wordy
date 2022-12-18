using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Accessibility;
using Sirenix.OdinInspector;
using Base;
using Base.Word;
using Base.Dialog;
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
    public static void ShowWord(Word word) 
    {
        Instantiate(ProjectSettings.ProjectSettings.Mine.DiscretionViwe, null).GetComponentInChildren<ContentObject>().Content = word;
    }
    public static void ShowDialog(Dialog word) 
    {
        Instantiate(ProjectSettings.ProjectSettings.Mine.DiscretionDialog, null).GetComponentInChildren<ContentObject>().Content = word;
    }
    public void DestroyUrself() 
    {
        Destroy(transform.root.gameObject);
    }
}
