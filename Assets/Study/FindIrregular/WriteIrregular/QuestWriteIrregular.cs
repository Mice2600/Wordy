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
        [FoldoutGroup("Quest/WriteIlrregular")]
        public GameObject QuestWriteIlrregularPrefab;
        [FoldoutGroup("Quest/WriteIlrregular")]
        public string QuestWriteIlrregularName;
        [FoldoutGroup("Quest/WriteIlrregular")]
        public StudyScoreValumes QuestWriteIlrregularValumes;
    }
}

public class QuestWriteIrregular : Quest, IQuestStarterWithIrregularList
{
    public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestWriteIlrregularPrefab;
    public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestWriteIlrregularName;
    public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestWriteIlrregularValumes.AddScoreDialog;
    public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestWriteIlrregularValumes.RemoveScoreDialog;
    public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestWriteIlrregularValumes.AddScoreWord;
    public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestWriteIlrregularValumes.RemoveScoreWord;
    public override int AddScoreIrregular => ProjectSettings.ProjectSettings.Mine.QuestWriteIlrregularValumes.AddScoreIrregular;
    public override int RemoveScoreIrregular => ProjectSettings.ProjectSettings.Mine.QuestWriteIlrregularValumes.RemoveScoreIrregular;
    public List<Irregular> NeedIrregulars
    {
        get
        {
            if (_NeedIrregulars == null) _NeedIrregulars = IrregularBase.Irregulars.GetContnetList(MinimalCount);
            return _NeedIrregulars;
        }
        set => _NeedIrregulars = value;
    }
    private List<Irregular> _NeedIrregulars;
    public int MinimalCount => 8;

    

    public void CreatQuest(List<Irregular> Irregulars) => NeedIrregulars = Irregulars;
}
