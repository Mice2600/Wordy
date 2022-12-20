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

public class DiscretionIncorrectContent : DiscretionViwe
{
    public static void ShowIncorrectContent(IContent TrueContent, IContent WrongContent, int RemovedScore)
    {
        DiscretionIncorrectContent N = Instantiate(ProjectSettings.ProjectSettings.Mine.DiscretionViwe, null).GetComponentInChildren<DiscretionIncorrectContent>();
        N.RemovedScoreText.text = "-" + RemovedScore;
        N.Content = TrueContent;
        N.WrongContentVewe.Content = WrongContent;
    }
    [SerializeField]
    [Required]
    private ContentObject WrongContentVewe;
    [SerializeField]
    [Required]
    private TMP_Text RemovedScoreText;
}
