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
        [FoldoutGroup("Quest/TwoWord")]
        public GameObject QuestTwoWordSystemPrefab;
        [FoldoutGroup("Quest/TwoWord")]
        public string QuestTwoWordSystemSceneName;
        [FoldoutGroup("Quest/TwoWord")]
        public StudyScoreValumes QuestTwoWordSystemValumes;
    }
}
namespace Study.TwoWordSystem
{
    public class QuestTwoWord : Quest
    {
        public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemValumes.AddScoreDialog;
        public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemValumes.RemoveScoreDialog;
        public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemValumes.AddScoreWord;
        public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemValumes.RemoveScoreWord;
        private protected override void Start()
        {
            base.Start();
            OnFineshed += () => Instantiate(ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemPrefab);
        }
    }
}