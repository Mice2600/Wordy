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
        [FoldoutGroup("Quest/FindOns/E2T")]
        public GameObject QuestFindOnsPrefab;
        [FoldoutGroup("Quest/FindOns/E2T")]
        public string QuestFindOnsSceneName;
        [FoldoutGroup("Quest/FindOns/E2T")]
        public StudyScoreValumes QuestFindOnsScorevalumes;
    }
}
namespace Study.FindOns
{


    public class QuestFindOns : Quest, IQuestStarterWithWordList
    {
        public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestFindOnsPrefab;
        public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestFindOnsSceneName;
        public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.AddScoreDialog;
        public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.RemoveScoreDialog;
        public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.AddScoreWord;
        public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFindOnsScorevalumes.RemoveScoreWord;
        public override int AddScoreIrregular => throw new System.NotImplementedException();

        public override int RemoveScoreIrregular => throw new System.NotImplementedException();
        public List<Word> NeedWords
        {
            get
            {
                if (_NeedWords == null || _NeedWords.Count < MinimalCount) _NeedWords = WordBase.Wordgs.GetContnetList(MinimalCount);
                return _NeedWords;
            }
            set
            {
                _NeedWords = value;
            }
        }
        private List<Word> _NeedWords;
        public int MinimalCount => 8;

        

        public void CreatQuest(List<Word> Words)
        {
            NeedWords = Words;
        }
    }
}