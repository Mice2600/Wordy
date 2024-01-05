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
        public class QuestSynonymMatch : StudyContentData, IWordScorer, IQuestStarterWithSynonymList
        {
            [field: SerializeField] public int AddScoreWord { get; set; }
            [field: SerializeField] public int RemoveScoreWord { get; set; }
            public int MinimalCount => 25;
        }
        [FoldoutGroup("SynonymMatch")]
        public QuestSynonymMatch synonymMatch;
    }
}

namespace Study.CopleFinder.SynonymMatch
{
    public class QuestSynonymMatch : Quest
    {

    }
}