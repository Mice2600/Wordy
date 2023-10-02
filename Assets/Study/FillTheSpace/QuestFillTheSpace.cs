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
        public class QuestFillTheSpace : StudyContentData, IWordScorer, IQuestStarterWithWord
        {
            [field: SerializeField] public int AddScoreWord { get; set; }
            [field: SerializeField] public int RemoveScoreWord { get; set; }
        }
        [FoldoutGroup("questFillTheSpace")]
        public QuestFillTheSpace questFillTheSpace;
    }
}
namespace Study.FillTheSpace
{
    public class QuestFillTheSpace : Quest
    {

    }
}