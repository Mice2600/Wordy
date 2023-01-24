using Base;
using System;

public abstract partial class Content : IComparable // Utulity
{
    public Content(string EnglishSource, string RussianSource, float Score, bool Active)
    {
        EnglishSource.ToUpper();
        this.EnglishSource = EnglishSource.ToUpper();
        this.RussianSource = RussianSource;
        this.Score = Score;
        this.Active = Active;
    }
    public int CompareTo(object obj)
    {
        try
        {
            return EnglishSource.CompareTo(((Content)obj).EnglishSource);
        }
        catch (Exception XX) { throw SystemBox.Tools.ExceptionThrow(XX, 2); }
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (obj is not Content) return false;
        return (obj as Content).EnglishSource.ToUpper() == this.EnglishSource.ToUpper();
    }
    public override string ToString() => EnglishSource;
    public override int GetHashCode() => base.GetHashCode();

}