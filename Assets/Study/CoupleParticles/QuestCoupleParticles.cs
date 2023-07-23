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
        [FoldoutGroup("Quest/CoupleParticles")]
        public GameObject QuestCoupleParticlesPrefab;
        [FoldoutGroup("Quest/CoupleParticles")]
        public string QuestCoupleParticlesSceneName;
        [FoldoutGroup("Quest/CoupleParticles")]
        public StudyScoreValumes QuestCoupleParticlesScorevalumes;
    }
}

namespace Study.CoupleParticles
{
    public class QuestCoupleParticles : Quest, IQuestStarterWithWordList
    {
        public override GameObject QuestPrefab => ProjectSettings.ProjectSettings.Mine.QuestCoupleParticlesPrefab;
        public override string QuestName => ProjectSettings.ProjectSettings.Mine.QuestCoupleParticlesSceneName;
        public override int AddScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestCoupleParticlesScorevalumes.AddScoreDialog;
        public override int RemoveScoreDialog => ProjectSettings.ProjectSettings.Mine.QuestCoupleParticlesScorevalumes.RemoveScoreDialog;
        public override int AddScoreWord => ProjectSettings.ProjectSettings.Mine.QuestCoupleParticlesScorevalumes.AddScoreWord;
        public override int RemoveScoreWord => ProjectSettings.ProjectSettings.Mine.QuestCoupleParticlesScorevalumes.RemoveScoreWord;
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
}