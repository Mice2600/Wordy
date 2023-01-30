using Sirenix.OdinInspector;
using System;
namespace Base.Word
{
    [System.Serializable]
    public partial class Word : Content, IDiscreption, ISpeeker  // Utulity
    {
        string IDiscreption.EnglishDiscretion { get => this.EnglishDiscretion; set => this.EnglishDiscretion = value; } 
        string IDiscreption.RusianDiscretion { get => this.RusianDiscretion; set => this.RusianDiscretion = value; }
        string ISpeeker.SpeekText => EnglishSource;
        public Word(string EnglishSource, string RussianSource, float Score, bool Active, string EnglishDiscretion, string RusianDiscretion) : base(EnglishSource, RussianSource, Score, Active)
        {
            EnglishSource.ToUpper();
            this.EnglishDiscretion = EnglishDiscretion;
            this.RusianDiscretion = RusianDiscretion;
        }
    }
}