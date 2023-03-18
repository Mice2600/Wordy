using Base;
using Base.Word;
using Sirenix.OdinInspector;
using UnityEngine;
[System.Serializable]
public partial class WordDefoult : Content, IDiscreption, ISpeeker // Utulity
{
    string IDiscreption.EnglishDiscretion { get => this.EnglishDiscretion; set => this.EnglishDiscretion = value; }
    string IDiscreption.RusianDiscretion { get => this.RusianDiscretion; set => this.RusianDiscretion = value; }
    string ISpeeker.SpeekText => EnglishSource;
    public override IDataListComands BaseCommander => WordBase.Wordgs;
    public override GameObject DiscretioVewe => ProjectSettings.ProjectSettings.Mine.DiscretionWordDefoult;
    public override GameObject ContentObject => ProjectSettings.ProjectSettings.Mine.ContentObjectWordDefoult;

    public WordDefoult(string EnglishSource, string RussianSource, string EnglishDiscretion, string RusianDiscretion) : base(EnglishSource, RussianSource)
    {
        EnglishSource.ToUpper();
        this.EnglishDiscretion = EnglishDiscretion;
        this.RusianDiscretion = RusianDiscretion;
    }
    public override bool Equals(object obj) => base.Equals(obj) && (obj is Word || obj is WordDefoult);
    public override int GetHashCode() => base.GetHashCode();

}

namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [FoldoutGroup("Discretion"), Required]
        public GameObject DiscretionWordDefoult;
        [FoldoutGroup("ContentObject"), Required]
        public GameObject ContentObjectWordDefoult;
    }
}