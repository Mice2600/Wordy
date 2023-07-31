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
        [FoldoutGroup("Quest/Ring")]
        public GameObject QuestRingPrefab;
        [FoldoutGroup("Quest/Ring")]
        public string QuestRingSceneName;
        [FoldoutGroup("Quest/Ring")]
        public StudyScoreValumes QuestRingScorevalumes;
    }
}

public class QuestRing : Quest, IQuestStarterWithWordList
{
    public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestRingPrefab;
    public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestRingSceneName;
    public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestRingScorevalumes.AddScoreWord;
    public override int AddScoreIrregular => throw new System.NotImplementedException();
    public override int RemoveScoreIrregular => throw new System.NotImplementedException();
    public override int AddScoreDialog => throw new System.NotImplementedException();
    public override int RemoveScoreDialog => throw new System.NotImplementedException();
    public override int RemoveScoreWord => throw new System.NotImplementedException();
    public List<Word> NeedWords
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
        NeedWords = words;
    }
}