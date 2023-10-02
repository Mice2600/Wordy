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
        public class QuestTwoWord : StudyContentData, IWordScorer, IQuestStarterWithWordList
        {
            [field: SerializeField] public int AddScoreWord { get; set; }
            [field: SerializeField] public int RemoveScoreWord { get; set; }
            public int MinimalCount => 15;
        }
        [FoldoutGroup("questTwoWord")]
        public QuestTwoWord questTwoWord;
    }
}
namespace Study.TwoWordSystem
{
    public class QuestTwoWord : Quest
    {
       
    }
}