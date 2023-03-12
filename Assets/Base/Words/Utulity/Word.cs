using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Base.Word
{
    [System.Serializable]
    public partial class Word : Content, IDiscreption, ISpeeker  // Utulity
    {
        string IDiscreption.EnglishDiscretion { get => this.EnglishDiscretion; set => this.EnglishDiscretion = value; } 
        string IDiscreption.RusianDiscretion { get => this.RusianDiscretion; set => this.RusianDiscretion = value; }
        string ISpeeker.SpeekText => EnglishSource;
        public override IDataListComands BaseCommander => WordBase.Wordgs;
        public override GameObject DiscretioVewe => ProjectSettings.ProjectSettings.Mine.DiscretionWord;
        public override GameObject ContentObject => ProjectSettings.ProjectSettings.Mine.ContentObjectWord;

        public Word(string EnglishSource, string RussianSource, float Score, bool Active, string EnglishDiscretion, string RusianDiscretion) : base(EnglishSource, RussianSource, Score, Active)
        {
            EnglishSource.ToUpper();
            this.EnglishDiscretion = EnglishDiscretion;
            this.RusianDiscretion = RusianDiscretion;
        }
        public override bool Equals(object obj) => base.Equals(obj) && obj is Word;
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