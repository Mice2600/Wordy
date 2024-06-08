using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;

namespace Base.Word
{
    [System.Serializable]
    public partial class Word : Content, IDiscreption, ISpeeker , IPersanalData, IMultiTranslation, Tagable // Utulity
    {
        string IDiscreption.EnglishDiscretion { get => this.EnglishDiscretion; set => this.EnglishDiscretion = value; } 
        string IDiscreption.RusianDiscretion { get => this.RusianDiscretion; set => this.RusianDiscretion = value; }
        string ISpeeker.SpeekText => EnglishSource;
        public override IDataListComands BaseCommander => WordBase.Wordgs;
        public override GameObject DiscretioVewe => ProjectSettings.ProjectSettings.Mine.DiscretionWord;
        public override GameObject ContentObject => ProjectSettings.ProjectSettings.Mine.ContentObjectWord;

        float IPersanalData.Score { get => this.Score; set => this.Score = value; }
        bool IPersanalData.Active { get => this.Active; set => this.Active = value; }
        List<string> Tagable.Tags { get => this.Tags; set => this.Tags = value; }

        public Word(Word Clone) : base(Clone.EnglishSource, Clone.RussianSource)
        {
            this.EnglishDiscretion = Clone.EnglishDiscretion;
            this.EnglishDiscretion.ToUpper();
            this.RusianDiscretion = Clone.RusianDiscretion;
            this.RusianDiscretion.ToUpper();
            this.Score = Clone.Score;
            this.Active = Clone.Active;
        }

        public Word(WordDefoult Clone) : base(Clone.EnglishSource, Clone.RussianSource)
        {
            this.EnglishDiscretion = Clone.EnglishDiscretion;
            this.EnglishDiscretion.ToUpper();
            this.RusianDiscretion = Clone.RusianDiscretion;
            this.RusianDiscretion.ToUpper();
            this.Score = 0;
            this.Active = false;
        }

        public Word(string EnglishSource, string RussianSource, float Score, bool Active, string EnglishDiscretion, string RusianDiscretion) : base(EnglishSource, RussianSource)
        {
            EnglishSource.ToUpper();
            this.EnglishDiscretion = EnglishDiscretion;
            this.RusianDiscretion = RusianDiscretion;
            this.Score = Score;
            this.Active = Active;
        }
        public override bool Equals(object obj) => base.Equals(obj) && (obj is Word || obj is WordDefoult);
        public override int GetHashCode() => base.GetHashCode();
    }

  

}
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [FoldoutGroup("Discretion"), Required]
        public GameObject DiscretionWord;
        [FoldoutGroup("ContentObject"), Required]
        public GameObject ContentObjectWord;
    }
}