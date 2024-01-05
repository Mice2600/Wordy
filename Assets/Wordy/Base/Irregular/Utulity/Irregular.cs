using Base;
using Base.Dialog;
using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class Irregular : Content, IDiscreption, IIrregular, ISpeeker, IPersanalData
{
    string IDiscreption.EnglishDiscretion { get => this.EnglishDiscretion; set => this.EnglishDiscretion = value; }
    string IDiscreption.RusianDiscretion { get => this.RusianDiscretion; set => this.RusianDiscretion = value; }
    string IIrregular.BaseForm { get => EnglishSource; set => EnglishSource = value; }
    string IIrregular.SimplePast { get => SimplePast; set => SimplePast = value; }
    string IIrregular.PastParticiple { get => PastParticiple; set => PastParticiple = value; }
    string ISpeeker.SpeekText => (this as IIrregular).BaseForm + "  " + SimplePast + "  " + PastParticiple;
    float IPersanalData.Score { get => this.Score; set => this.Score = value; }
    bool IPersanalData.Active { get => this.Active; set => this.Active = value; }

    public override IDataListComands BaseCommander => IrregularBase.Irregulars;
    public override GameObject DiscretioVewe => ProjectSettings.ProjectSettings.Mine.DiscretionIrregular;
    public override GameObject ContentObject => ProjectSettings.ProjectSettings.Mine.ContentObjectIrregular;

    public Irregular(string EnglishSource, string RussianSource, string SimplePast, string PastParticiple, float Score, bool Active, string EnglishDiscretion, string RusianDiscretion) : base(EnglishSource, RussianSource)
    {
        EnglishSource.ToUpper();
        this.EnglishDiscretion = EnglishDiscretion;
        this.RusianDiscretion = RusianDiscretion;
        
        this.SimplePast = SimplePast;
        this.PastParticiple = PastParticiple;

        this.Score = Score;
        this.Active = Active;
    }
    

    
    public override bool Equals(object obj) => base.Equals(obj) && (obj is Irregular || obj is IrregularDefoult);
    public override int GetHashCode() => base.GetHashCode();
}
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        [FoldoutGroup("Discretion"), Required]
        public GameObject DiscretionIrregular;
        [FoldoutGroup("ContentObject"), Required]
        public GameObject ContentObjectIrregular;
    }
}
