using Base.Dialog;
using Base.Word;
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
        [FoldoutGroup("Quest/Crossword")]
        public GameObject QuestCrosswordPrefab;
        [FoldoutGroup("Quest/Crossword")]
        public string QuestCrosswordSceneName;
        [FoldoutGroup("Quest/Crossword")]
        public StudyScoreValumes QuestCrosswordScorevalumes;
    }
}
namespace Study.Crossword
{
    public class QuestCrossword : Quest, IQuestStarterWithWordList
    {
        public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestCrosswordPrefab;
        public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestCrosswordSceneName;
        public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestCrosswordScorevalumes.AddScoreDialog;
        public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestCrosswordScorevalumes.RemoveScoreDialog;
        public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestCrosswordScorevalumes.AddScoreWord;
        public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestCrosswordScorevalumes.RemoveScoreWord;
        public override int AddScoreIrregular => throw new System.NotImplementedException();

        public override int RemoveScoreIrregular => throw new System.NotImplementedException();
        public List<Word> NeedDialogs
        {
            get
            {
                if (_NeedDialogs == null || _NeedDialogs.Count < MinimalCount) _NeedDialogs = WordBase.Wordgs.GetContnetList(MinimalCount);
                return _NeedDialogs;
            }
            set
            {
                _NeedDialogs = value;
            }
        }
        private List<Word> _NeedDialogs;

        public int MinimalCount => 25;


        public void CreatQuest(List<Word> words)
        {
            NeedDialogs = words;
        }
    }
}