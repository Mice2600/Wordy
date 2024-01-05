using Base.Word;
using Sirenix.OdinInspector;
using Study.aSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [Serializable]
        public class QuestNumberExercise : StudyContentData,  IQuestStarter
        {

        }
        [FoldoutGroup("questNumberExercise")]
        public QuestNumberExercise questNumberExercise;
    }
}
namespace Study.NumberExercise
{
    public class QuestNumberExercise : Quest
    {
        public void Done()
        {
            OnFineshed.Invoke();
        }

    }
}