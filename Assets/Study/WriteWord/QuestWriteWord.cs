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
        [FoldoutGroup("Quest/WriteWord")]
        public GameObject QuestWriteWordPrefab;
        [FoldoutGroup("Quest/WriteWord")]
        public string QuestWriteWordName;
        [FoldoutGroup("Quest/WriteWord")]
        public StudyScoreValumes QuestWriteWordValumes;
    }
}
public class QuestWriteWord : Quest, IQuestStarterWithWord
{
    public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestWriteWordPrefab;
    public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestWriteWordValumes.AddScoreDialog;
    public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestWriteWordValumes.RemoveScoreDialog;
    public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestWriteWordValumes.AddScoreWord;
    public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestWriteWordValumes.RemoveScoreWord;


    public Word NeedWord
    {
        get
        {
            if (_NeedWord == null) _NeedWord = WordBase.Wordgs.GetContnetList(1)[0];
            return _NeedWord;
        }
        set
        {
            _NeedWord = value;
        }
    }
    private Word _NeedWord;

    public int MinimalCount => 1;

    public void CreatQuest(Word word)
    {
        NeedWord = word;
    }
}
