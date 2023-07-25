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
        [FoldoutGroup("Quest/WordSurf")]
        public GameObject QuestWordSurfPrefab;
        [FoldoutGroup("Quest/WordSurf")]
        public string QuestWordSurfSceneName;
        [FoldoutGroup("Quest/WordSurf")]
        public StudyScoreValumes QuestWordSurfScorevalumes;
    }
}

public class QuestWordSurf : Quest, IQuestStarterWithWordList
{
    public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestWordSurfPrefab;
    public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestWordSurfSceneName;
    public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestWordSurfScorevalumes.AddScoreDialog;
    public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestWordSurfScorevalumes.RemoveScoreDialog;
    public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestWordSurfScorevalumes.AddScoreWord;
    public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestWordSurfScorevalumes.RemoveScoreWord;
    public override int AddScoreIrregular => throw new System.NotImplementedException();

    public override int RemoveScoreIrregular => throw new System.NotImplementedException();
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