using Sirenix.OdinInspector;
using System;
namespace Base.Dialog
{
    public partial class Dialog : Content, IComparable, ISpeeker  // Utulity
    {
        string ISpeeker.SpeekText => EnglishSource;
        public Dialog(string EnglishSource, string RussianSource, float Score, bool Active) : base(EnglishSource, RussianSource, Score, Active)
        {

        }

    }
}