using Base.Word;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Base.Synonym
{
    public partial class Synonym : Content, ISpeeker, IMultiTranslation // Utulity
    {
        public Synonym(string EnglishSource, string RussianSource, List<string> attachments) : base(EnglishSource, RussianSource)
        {
            if(attachments == null) attachments = new List<string>();
            this.attachments = attachments;
        }

        public void Attach(List<string> Words) 
        {
            Words.ForEach(s => Attach(s));
        }
        public void Attach(string Word) 
        {
            if (!this.attachments.Contains(Word)) 
            {
                this.attachments.Add(Word);
            }
                
        }

        string ISpeeker.SpeekText => EnglishSource;
        public override IDataListComands BaseCommander => WordBase.Wordgs;
        public override GameObject DiscretioVewe => null;
        public override GameObject ContentObject => null;
        public override bool Equals(object obj)
        {
            if (base.Equals(obj)) return true;
            if ((obj is Base.Word.Word && attachments.Contains((obj as Base.Word.Word).EnglishSource)))
            {
                return true;
            }
            return false;
        }
        public override int GetHashCode() => base.GetHashCode();
    }
}