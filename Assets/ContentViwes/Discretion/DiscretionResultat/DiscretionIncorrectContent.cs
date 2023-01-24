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
        public GameObject DiscretionIncorrectContentViwe;
    }
}

public class DiscretionIncorrectContent : DiscretionResultat
{
    public static DiscretionIncorrectContent ShowIncorrectContent(Content TrueContent, Content WrongContent, int RemovedScore, System.Action OnClose = null)
    {
        DiscretionIncorrectContent N = Instantiate(ProjectSettings.ProjectSettings.Mine.DiscretionIncorrectContentViwe, null).GetComponentInChildren<DiscretionIncorrectContent>();        
        N.RemovedScoreText.text = "-" + Mathf.Abs(RemovedScore) ;
        N.Content = TrueContent;
        N.WrongContentVewe.Content = WrongContent;
        N.OnClose = OnClose;
        return N;
    }
    [SerializeField]
    [Required]
    private ContentObject WrongContentVewe;
    [SerializeField]
    [Required]
    private TMP_Text RemovedScoreText;
}
