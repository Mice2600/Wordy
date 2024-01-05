using Base.Word;
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
        public class QuestWriteWord : StudyContentData, IWordScorer, IQuestStarterWithWord
        {
            [field: SerializeField] public int AddScoreWord { get; set; }
            [field: SerializeField] public int RemoveScoreWord { get; set; }
        }
        [FoldoutGroup("questWriteWord")]
        public QuestWriteWord questWriteWord;
    }
}
namespace Study.WriteWord
{
    public class QuestWriteWord : Quest
    {
    }
}