using Sirenix.OdinInspector;
using Study.aSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectSettings 
{
    public partial class ProjectSettings 
    {
        [BoxGroup("Quest")]
        [FoldoutGroup("Quest/BuildDialog")]
        public GameObject QuestBuildDialogPrefab;
        [FoldoutGroup("Quest/BuildDialog")]
        public string QuestBuildDialogPrefabSceneName;
        [FoldoutGroup("Quest/BuildDialog")]
        public StudyScoreValumes QuestBuildDialogScorevalumes;
    }
}
namespace Study.BuildDialog
{
    public class QuestBuildDialog : Quest
    {
        public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestBuildDialogScorevalumes.AddScoreDialog;
        public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestBuildDialogScorevalumes.RemoveScoreDialog;
        public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestBuildDialogScorevalumes.AddScoreWord;
        public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestBuildDialogScorevalumes.RemoveScoreWord;
        private protected override void Start()
        {
            base.Start();
            OnFineshed += () => Instantiate(ProjectSettings.ProjectSettings.Mine.QuestBuildDialogPrefab);
        }
    }
    
}