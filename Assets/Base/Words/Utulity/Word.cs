using Sirenix.OdinInspector;
using System;
namespace Base.Word
{
    public partial class Word : Content, IDiscreption  // Utulity
    {
        string IDiscreption.EnglishDiscretion { get => this.EnglishDiscretion; set => this.EnglishDiscretion = value; } 
        string IDiscreption.RusianDiscretion { get => this.RusianDiscretion; set => this.RusianDiscretion = value; }
        public Word(string EnglishSource, string RussianSource, float Score, bool Active, string EnglishDiscretion, string RusianDiscretion) : base(EnglishSource, RussianSource, Score, Active)
        {
            EnglishSource.ToUpper();
            this.EnglishDiscretion = EnglishDiscretion;
            this.RusianDiscretion = RusianDiscretion;
        }
    }
}