using Base.Dialog;
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
    public class QuestBuildDialog : Quest, IQuestStarterWithDialogList
    {
        public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestBuildDialogPrefab;
        public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestBuildDialogScorevalumes.AddScoreDialog;
        public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestBuildDialogScorevalumes.RemoveScoreDialog;
        public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestBuildDialogScorevalumes.AddScoreWord;
        public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestBuildDialogScorevalumes.RemoveScoreWord;

        public List<Dialog> NeedDialogs 
        {
            get 
            {
                if (_NeedDialogs == null || _NeedDialogs.Count < 2) _NeedDialogs = DialogBase.Dialogs.GetContnetList(2);
                return _NeedDialogs;
            } 
            set 
            {
                _NeedDialogs = value;
            }
        }
        public List<Dialog> _NeedDialogs;

        public int MinimalCount => 2;


        public void CreatQuest(List<Dialog> dialogs)
        {
            NeedDialogs = dialogs;
        }
    }
    
}