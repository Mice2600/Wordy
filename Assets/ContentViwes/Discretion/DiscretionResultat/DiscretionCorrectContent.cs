using Base;
using Base.Word;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [Required]
        public GameObject DiscretionCorrectContentViwe;
    }
}

public class DiscretionCorrectContent : DiscretionResultat
{
    public static DiscretionCorrectContent ShowCorrectContent(IContent TrueContent, int AddScore, System.Action OnClose = null)
    {
        DiscretionCorrectContent N = Instantiate(ProjectSettings.ProjectSettings.Mine.DiscretionCorrectContentViwe, null).GetComponentInChildren<DiscretionCorrectContent>();
        N.AddScoreText.text = "+" + Mathf.Abs(AddScore);
        N.Content = TrueContent;
        N.OnClose = OnClose;
        return N;
    }
    [SerializeField]
    private GameObject SingelScoreWiwe;
    [SerializeField]
    [Required]
    private TMP_Text AddScoreText;
}
