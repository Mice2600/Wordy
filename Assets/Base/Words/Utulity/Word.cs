using Sirenix.OdinInspector;
using System;
namespace Base.Word
{
    public partial struct Word : IContent, IDiscreption, IComparable // Utulity
    {
        public Word(string EnglishSource, string RussianSource, float Score, bool Active, string EnglishDiscretion, string RusianDiscretion)
        {
            EnglishSource.ToUpper();
            _EnglishSource = EnglishSource.ToUpper();
            _RussianSource = RussianSource;
            _Score = Score;
            _EnglishDiscretion = EnglishDiscretion;
            _RusianDiscretion = RusianDiscretion;
            _Active = Active;
        }
        public string EnglishSource => _EnglishSource;
        public string RussianSource => _RussianSource;
        public float Score => _Score;
        public bool Active => _Active;
        public string EnglishDiscretion => _EnglishDiscretion;
        public string RusianDiscretion => _RusianDiscretion;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is not IContent) return false;
            return (obj as IContent).EnglishSource.ToUpper() == this.EnglishSource.ToUpper();
        }
        public override string ToString() => EnglishSource;
        public override int GetHashCode() => base.GetHashCode();
        public int CompareTo(object obj)
        {
            try
            {
                return EnglishSource.CompareTo(((IContent)obj).EnglishSource);
            }
            catch (Exception XX) { throw SystemBox.Tools.ExceptionThrow(XX, 2); }
        }
#if UNITY_EDITOR
        [Button]
        public void AddToDefaultBase() => ProjectSettings.ProjectSettings.Mine.AddWord(this);
#endif
    }
}