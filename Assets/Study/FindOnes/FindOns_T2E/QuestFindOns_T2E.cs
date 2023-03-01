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
        [FoldoutGroup("Quest/FindOns/T2E")]
        public GameObject QuestFindOns_T2E_Prefab;
        [FoldoutGroup("Quest/FindOns/T2E")]
        public string QuestFindOns_T2E_SceneName;
        [FoldoutGroup("Quest/FindOns/T2E")]
        public StudyScoreValumes QuestFindOns_T2E_Scorevalumes;
    }
}
public class QuestFindOns_T2E : Quest, IQuestStarterWithWordList
{
    public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestFindOns_T2E_Prefab;
    public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestFindOns_T2E_SceneName;
    public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestFindOns_T2E_Scorevalumes.AddScoreDialog;
    public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestFindOns_T2E_Scorevalumes.RemoveScoreDialog;
    public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFindOns_T2E_Scorevalumes.AddScoreWord;
    public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestFindOns_T2E_Scorevalumes.RemoveScoreWord;
    public override int AddScoreIrregular => throw new System.NotImplementedException();
    public override int RemoveScoreIrregular => throw new System.NotImplementedException();
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