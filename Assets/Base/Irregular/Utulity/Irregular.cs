using Base;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public partial class Irregular : Content, IDiscreption, IIrregular, ISpeeker
{
    string IDiscreption.EnglishDiscretion { get => this.EnglishDiscretion; set => this.EnglishDiscretion = value; }
    string IDiscreption.RusianDiscretion { get => this.RusianDiscretion; set => this.RusianDiscretion = value; }
    string IIrregular.BaseForm { get => EnglishSource; set => EnglishSource = value; }
    string IIrregular.SimplePast { get => SimplePast; set => SimplePast = value; }
    string IIrregular.PastParticiple { get => PastParticiple; set => PastParticiple = value; }
    string ISpeeker.SpeekText => (this as IIrregular).BaseForm + "  " + SimplePast + "  " + PastParticiple;

    public Irregular(string EnglishSource, string RussianSource, string SimplePast, string PastParticiple, float Score, bool Active, string EnglishDiscretion, string RusianDiscretion) : base(EnglishSource, RussianSource, Score, Active)
    {
        EnglishSource.ToUpper();
        this.EnglishDiscretion = EnglishDiscretion;
        this.RusianDiscretion = RusianDiscretion;
        
        this.SimplePast = SimplePast;
        this.PastParticiple = PastParticiple;
    }
}
