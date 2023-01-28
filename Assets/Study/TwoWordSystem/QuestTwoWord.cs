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
    public class QuestTwoWord : Quest , IQuestStarterWithWordList
    {
        public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemPrefab;
        public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemValumes.AddScoreDialog;
        public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemValumes.RemoveScoreDialog;
        public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemValumes.AddScoreWord;
        public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestTwoWordSystemValumes.RemoveScoreWord;


        public List<Word> NeedWords
        {
            get
            {
                if (_NeedWords == null || _NeedWords.Count < 2) _NeedWords = WordBase.Wordgs.GetContnetList(MinimalCount + Random.Range(0, 14));
                return _NeedWords;
            }
            set
            {
                _NeedWords = value;
            }
        }
        public List<Word> _NeedWords;

        public int MinimalCount => 15;


        public void CreatQuest(List<Word> Words)
        {
            NeedWords = Words;
        }
    }
}