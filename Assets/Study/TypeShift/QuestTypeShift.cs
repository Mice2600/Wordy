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
        [FoldoutGroup("Quest/TypeShift")]
        public GameObject QuestTypeShiftPrefab;
        [FoldoutGroup("Quest/TypeShift")]
        public string QuestTypeShiftSceneName;
        [FoldoutGroup("Quest/TypeShift")]
        public StudyScoreValumes QuestTypeShiftValumes;
    }
}
public class QuestTypeShift : Quest, IQuestStarterWithWordList
{
    public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestTypeShiftPrefab;
    public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestTypeShiftSceneName;
    public override int AddScoreDialog => throw new System.NotImplementedException();
    public override int RemoveScoreDialog => throw new System.NotImplementedException();
    public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestTypeShiftValumes.AddScoreWord;
    public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestTypeShiftValumes.RemoveScoreWord;
    public override int AddScoreIrregular => throw new System.NotImplementedException();

    public override int RemoveScoreIrregular => throw new System.NotImplementedException();

    public List<Word> NeedWords
    {
        get
        {
            if (_NeedWords == null || _NeedWords.Count < MinimalCount) _NeedWords = WordBase.Wordgs.ActiveItems;
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