using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemBox;
using UnityEngine;
public abstract partial class Content : IComparable// Utulity
{
    
    public Content(string EnglishSource, string RussianSource)
    {
        EnglishSource.ToUpper();
        this.EnglishSource = EnglishSource.ToUpper();
        this.RussianSource = RussianSource;
    }

    public abstract IDataListComands BaseCommander { get; }
    public abstract GameObject DiscretioVewe { get; }
    public abstract GameObject ContentObject { get; }
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