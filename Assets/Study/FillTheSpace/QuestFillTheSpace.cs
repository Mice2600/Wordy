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
        [FoldoutGroup("Quest/FillTheSpace")]
        public GameObject QuestFillTheSpacePrefab;
        [FoldoutGroup("Quest/FillTheSpace")]
        public string QuestFillTheSpaceName;
        [FoldoutGroup("Quest/FillTheSpace")]
        public StudyScoreValumes QuestFillTheSpaceValumes;
    }
}
public class QuestFillTheSpace : Quest, IQuestStarterWithWord
{
    public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestFillTheSpacePrefab;
    public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestFillTheSpaceName;
    public override int AddScoreDialog => throw new System.NotImplementedException();
    public override int RemoveScoreDialog => throw new System.NotImplementedException();
    public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFillTheSpaceValumes.AddScoreWord;
    public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFillTheSpaceValumes.RemoveScoreWord;
    public override int AddScoreIrregular => throw new System.NotImplementedException();
    public override int RemoveScoreIrregular => throw new System.NotImplementedException();

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
