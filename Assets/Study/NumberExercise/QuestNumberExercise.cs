using Base.Word;
using Sirenix.OdinInspector;
using Study.aSystem;
using System.Collections;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [BoxGroup("Quest")]
        [FoldoutGroup("Quest/NumberExercise")]
        public GameObject QuestNumberExercisePrefab;
        [FoldoutGroup("Quest/NumberExercise")]
        public string QuestNumberExerciseSceneName;
    }
}

public class QuestNumberExercise : Quest, IQuestStarter
{
    public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestNumberExercisePrefab;
    public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestNumberExerciseSceneName;
    public override int AddScoreDialog => throw new System.NotImplementedException();
    public override int RemoveScoreDialog => throw new System.NotImplementedException();
    public override int AddScoreWord => throw new System.NotImplementedException();
    public override int RemoveScoreWord => throw new System.NotImplementedException();
    public override int AddScoreIrregular => throw new System.NotImplementedException();
    public override int RemoveScoreIrregular => throw new System.NotImplementedException();
    public void Done() 
    {
        OnFineshed.Invoke();
    }
    public void CreatQuest()
    {
    }
}
