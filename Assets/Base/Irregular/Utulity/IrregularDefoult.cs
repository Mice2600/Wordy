using Base.Word;
using Base;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[System.Serializable]
public partial class IrregularDefoult : Content, ISpeeker, IDiscreption, IIrregular // Utulity
{
    string ISpeeker.SpeekText => EnglishSource;
    public override IDataListComands BaseCommander => IrregularBase.Irregulars;
    public override GameObject DiscretioVewe => ProjectSettings.ProjectSettings.Mine.DiscretionIrregularDefoult;
    public override GameObject ContentObject => ProjectSettings.ProjectSettings.Mine.ContentObjectIrregularDefoult;
    string IDiscreption.EnglishDiscretion { get => this.EnglishDiscretion; set => this.EnglishDiscretion = value; }
    string IDiscreption.RusianDiscretion { get => this.RusianDiscretion; set => this.RusianDiscretion = value; }
    string IIrregular.BaseForm { get => EnglishSource; set => EnglishSource = value; }
    string IIrregular.SimplePast { get => SimplePast; set => SimplePast = value; }
    string IIrregular.PastParticiple { get => PastParticiple; set => PastParticiple = value; }

    public IrregularDefoult(string EnglishSource, string TranslateSource, string SimplePast, string PastParticiple, string EnglishDiscretion, string RusianDiscretion) : base(EnglishSource, TranslateSource)
    {
        EnglishSource.ToUpper();
        this.EnglishDiscretion = EnglishDiscretion;
        this.RusianDiscretion = RusianDiscretion;
        this.SimplePast = SimplePast;
        this.PastParticiple = PastParticiple;
    }

    public override bool Equals(object obj) => base.Equals(obj) && (obj is Irregular || obj is IrregularDefoult);
    public override int GetHashCode() => base.GetHashCode();

}
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [FoldoutGroup("Discretion"), Required]
        public GameObject DiscretionIrregularDefoult;
        [FoldoutGroup("ContentObject"), Required]
        public GameObject ContentObjectIrregularDefoult;
    }
}
