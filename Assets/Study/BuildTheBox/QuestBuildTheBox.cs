using Base.Word;
using Sirenix.OdinInspector;
using Study.aSystem;
using Study.CoupleParticles;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [Serializable]
        public class QuestBuildTheBox : StudyContentData, IWordScorer, IQuestStarterWithWordList
        {
            [field: SerializeField] public int AddScoreWord { get; set; }
            [field: SerializeField] public int RemoveScoreWord { get; set; }
            public int MinimalCount => 25;
        }
        [FoldoutGroup("buildTheBox")]
        public QuestBuildTheBox buildTheBox;
    }
}
namespace Study.BuildTheBox
{
    public class QuestBuildTheBox : Quest
    {
    }
}