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
        public class QuestSpellingExercise : StudyContentData, IWordScorer, IQuestStarterWithWord
        {
            [field: SerializeField] public int AddScoreWord { get; set; }
            [field: SerializeField] public int RemoveScoreWord { get; set; }
        }
        [FoldoutGroup("questSpellingExercise")]
        public QuestSpellingExercise questSpellingExercise;
    }
}
namespace Study.SpellingExercise
{
    public class QuestSpellingExercise : Quest
    {
        public void Lost() 
        {
            OnGameLost?.Invoke();
            OnWordLost?.Invoke(NeedWord);
            OnFineshed?.Invoke();
        }
        public void Win()
        {
            OnGameWin?.Invoke();
            OnWordWin?.Invoke(NeedWord);
            OnFineshed?.Invoke();
        }
    }
}