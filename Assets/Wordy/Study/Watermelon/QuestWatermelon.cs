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
        public class QuestWatermelon : StudyContentData, IWordScorer, IQuestStarterWithWordList
        {
            [field: SerializeField] public int AddScoreWord { get; set; }
            [field: SerializeField] public int RemoveScoreWord { get; set; }
            public int MinimalCount => 25;
        }
        [FoldoutGroup("Watermelon")]
        public QuestWatermelon watermelon;
    }
}


namespace Study.Watermelon
{
    public class QuestWatermelon : Quest
    {
    }
}