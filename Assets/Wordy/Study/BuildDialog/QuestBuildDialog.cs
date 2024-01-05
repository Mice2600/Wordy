using Base.Dialog;
using Sirenix.OdinInspector;
using Study.aSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectSettings 
{
    public partial class ProjectSettings 
    {
        [Serializable]
        public class QuestBuildDialog : StudyContentData, IDialogScorer, IWordScorer, IQuestStarterWithDialog
        {
            [field: SerializeField] public int AddScoreWord { get; set; }
            [field: SerializeField] public int RemoveScoreWord { get; set; }
            [field: SerializeField] public int AddScoreDialog { get; set; }
            [field: SerializeField] public int RemoveScoreDialog { get; set; }
        }
        [FoldoutGroup("BuildDialog")]
        public QuestBuildDialog BuildDialog;
        

    }
}
namespace Study.BuildDialog
{
    public class QuestBuildDialog : Quest
    {

    }
    
}