using Base.Dialog;
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
        [FoldoutGroup("Quest/BuildIlrregular")]
        public GameObject QuestBuildIlrregularPrefab;
        [FoldoutGroup("Quest/BuildIlrregular")]
        public string QuestBuildIlrregularPrefabSceneName;
        [FoldoutGroup("Quest/BuildIlrregular")]
        public StudyScoreValumes QuestBuildIlrregularScorevalumes;
    }
}

public class QuestBuildIlrregular : Quest, IQuestStarterWithIrregularList
{

    public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestBuildIlrregularPrefab;
    public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestBuildIlrregularScorevalumes.AddScoreDialog;
    public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestBuildIlrregularScorevalumes.RemoveScoreDialog;
    public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestBuildIlrregularScorevalumes.AddScoreWord;
    public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestBuildIlrregularScorevalumes.RemoveScoreWord;
    public override int AddScoreIrregular => ProjectSettings.ProjectSettings.Mine.QuestBuildIlrregularScorevalumes.AddScoreIrregular;
    public override int RemoveScoreIrregular => ProjectSettings.ProjectSettings.Mine.QuestBuildIlrregularScorevalumes.RemoveScoreIrregular;
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
    public int MinimalCount => 5;



    public void CreatQuest(List<Irregular> Irregulars) => NeedIrregulars = Irregulars;
}
