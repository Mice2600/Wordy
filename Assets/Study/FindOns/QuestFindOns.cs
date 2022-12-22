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
        [FoldoutGroup("Quest/FindOns")]
        public GameObject QuestFindOnsPrefab;
        [FoldoutGroup("Quest/FindOns")]
        public string QuestFindOnsSceneName;
        [FoldoutGroup("Quest/FindOns")]
        public StudyScoreValumes QuestFindOnsScorevalumes;
    }
}
namespace Study.FindOns
{


    public class QuestFindOns : Quest
    {
        public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.AddScoreDialog;

        public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.RemoveScoreDialog;

        public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.AddScoreWord;

        public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.RemoveScoreWord;
        private protected override void Start()
        {
            base.Start();
            OnFineshed += ()=> Instantiate(ProjectSettings.ProjectSettings.Mine.QuestFindOnsPrefab);
        }
    }
}