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


    public class QuestFindOns : Quest, IQuestStarterWithWordList
    {
        public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestFindOnsPrefab;
        public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.AddScoreDialog;
        public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.RemoveScoreDialog;
        public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.AddScoreWord;
        public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.RemoveScoreWord;
        public List<Word> NeedWords
        {
            get
            {
                if (_NeedWords == null || _NeedWords.Count < 2) _NeedWords = WordBase.Wordgs.GetContnetList(MinimalCount);
                return _NeedWords;
            }
            set
            {
                _NeedWords = value;
            }
        }
        public List<Word> _NeedWords;
        public int MinimalCount => 8;
        public void CreatQuest(List<Word> Words)
        {
            NeedWords = Words;
        }
    }
}