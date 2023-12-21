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
        public class QuestAntonymMatch : StudyContentData, IWordScorer, IQuestStarterWithAntonymList
        {
            [field: SerializeField] public int AddScoreWord { get; set; }
            [field: SerializeField] public int RemoveScoreWord { get; set; }
            public int MinimalCount => 25;
        }
        [FoldoutGroup("AntonymMatch")]
        public QuestAntonymMatch antonymMatch;
    }
}

namespace Study.CopleFinder.AntonymMatch
{
    public class QuestAntonymMatch : Quest
    {

    }
}