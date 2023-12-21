using Base.Word;
using System.Collections.Generic;
using UnityEngine;
namespace Base.Antonym
{
    [System.Serializable]
    public partial class Antonym : Content, ISpeeker // Utulity
    {
        public Antonym(string EnglishSource, string RussianSource, List<string> attachments) : base(EnglishSource, RussianSource)
        {
            if (attachments == null) attachments = new List<string>();
            this.attachments = attachments;
        }

        public void Attach(List<string> Words)
        {
            Words.ForEach(s => Attach(s));
        }

        public void Attach(string Word)
        {
            Word = Word.ToUpper();
            if (!this.attachments.Contains(Word))
            {
                this.attachments.Add(Word);
            }

        }
        public void DetachAll()
        {
            this.attachments = new List<string>();
        }
        public void Detach(List<string> Words)
        {
            Words.ForEach(s => Detach(s));
        }
        public void Detach(string Words)
        {
            Words = Words.ToUpper();
            this.attachments.Remove(Words);
        }
        string ISpeeker.SpeekText => EnglishSource;
        public override IDataListComands BaseCommander => AntonymBase.Antonyms;
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