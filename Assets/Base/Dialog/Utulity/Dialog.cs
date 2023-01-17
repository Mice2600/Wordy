using Sirenix.OdinInspector;
using System;
namespace Base.Dialog
{
    public partial struct Dialog : IContent, IComparable // Utulity
    {
        public Dialog(string EnglishSource, string RussianSource, float Score, bool Active)
        {
            _EnglishSource = EnglishSource;
            _RussianSource = RussianSource;
            _Score = Score;
            _Active = Active;
        }
        public string EnglishSource => _EnglishSource;
        public string RussianSource => _RussianSource;
        public float Score => _Score;
        public bool Active => _Active;

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj is not IContent) return false;
            return (obj as IContent).EnglishSource == this.EnglishSource;
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
        public void AddToDefaultBase() => ProjectSettings.ProjectSettings.Mine.AddDialog(this);
#endif
    }
}