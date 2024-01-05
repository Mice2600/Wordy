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
        public class QuestBuildIlrregular : StudyContentData, IIrregularScorer, IQuestStarterWithIrregularList
        {
            [field: SerializeField] public int AddScorIrregular { get; set; }
            [field: SerializeField] public int RemoveScoreIrregular { get; set; }
            public int MinimalCount => 5;
        }
        [FoldoutGroup("QuestBuildIlrregular")]
        public QuestBuildIlrregular questBuildIlrregular;
    }
}
namespace Study.WriteIrregular
{
    public class QuestBuildIlrregular : Quest
    {

    }
}