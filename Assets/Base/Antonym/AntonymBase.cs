using Base.Synonym;
using System.Linq;
using SystemBox;
namespace Base.Antonym
{
    public class AntonymBase : DataList<Antonym>
    {
        static AntonymBase()
        {
            //if (Application.isEditor && false)
            //{
            //    if (PlayerPrefs.GetInt(ID + " defaul") == 0)
            //        PlayerPrefs.SetString(ID, ProjectSettings.ProjectSettings.Mine.DefalultSynonym.text);
            //    PlayerPrefs.SetInt(ID + " defaul", 1);
            //}
            //DefaultBase = new List<WordDefoult>(JsonHelper.FromJson<WordDefoult>(ProjectSettings.ProjectSettings.Mine.DefalultWords.text));
            Antonyms = new AntonymBase();
        }
        public Antonym this[Content index]
        {
            get
            {
                int fIndex = IndexOf(new Antonym(index.EnglishSource, "", null));
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                return this[fIndex];
            }
            private set
            {
                int fIndex = IndexOf(new Antonym(index.EnglishSource, "", null));
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                this[fIndex] = value;
            }
        }
        public static AntonymBase Antonyms { get; set; }
        public override string DataID => "AntonymBase";


        public int UsebleCount
        {
            get
            {
                return Antonyms.Where(a => Word.WordBase.Wordgs.Contains(a)).Count();
            }
        }

        public override System.Collections.Generic.List<Antonym> ActiveItems => Antonyms.Where(s =>
        {

            int I = Word.WordBase.Wordgs.IndexOf(s);
            if (I > -1)
            {
                return (Word.WordBase.Wordgs[I] as IPersanalData).Active == true;
            }
            return false;
        }).ToList();

        public static TList<Antonym> AntonymOf(Content Content) => AntonymOf(Content.EnglishSource);
        public static TList<Antonym> AntonymOf(string Source)
        {
            TList<Antonym> dd = Antonyms.FindAll(d => d.attachments.Contains(Source));
            if (dd == null) return new TList<Antonym>();
            return dd;
        }


        public static void AddAntonym(Content Source, string Antonyms) => AddAntonym(Source, new TList<string>(Antonyms));
        public static void AddAntonym(Content Source, TList<string> Antonyms)
        {
            for (int i = 0; i < Antonyms.Count; i++) Antonyms[i] = Antonyms[i].ToUpper();
            Antonym AntonymWord = AntonymBase.Antonyms.Find(d => d.EnglishSource.ToUpper() == Source.EnglishSource.ToUpper());


            if (AntonymWord == null)
            {
                AntonymBase.Antonyms.Add(new Antonym(Source.EnglishSource.ToUpper(), Source.RussianSource, Antonyms));
            }
            else AntonymWord.Attach(Antonyms);


            for (int i = 0; i < Antonyms.Count; i++)
            {
                Antonym AntonymOne = AntonymBase.Antonyms.Find(d => d.EnglishSource.ToUpper() == Antonyms[i]);
                if (AntonymOne == null)
                    AntonymBase.Antonyms.Add(new Antonym(Antonyms[i], "", new TList<string>(Source.EnglishSource.ToUpper())));
                else AntonymOne.Attach(Source.EnglishSource.ToUpper());
            }
            AntonymBase.Antonyms.Save();
        }


        public static void RemoveAntonym(string Antonym)
        {
            Antonym = Antonym.ToUpper();
            Antonym SynonymWord = AntonymBase.Antonyms.Find(d => d.EnglishSource.ToUpper() == Antonym);
            if (SynonymWord != null) { AntonymBase.Antonyms.Remove(SynonymWord); }
            Antonyms.ForEach(Ot => { Ot.attachments.Remove(Antonym); });
            AntonymBase.Antonyms.Save();
        }

        public override Antonym tryCreat(string EnglishID) => new Antonym(EnglishID, "", null);
        public override Antonym tryCreat(Content Id)
        {
            if (Id is Antonym) return Id as Antonym;
            if (Id is Base.Word.Word)
            {
                int fIndex = IndexOf(new Antonym(Id.EnglishSource, "", null));
                if (fIndex < 0) return new Antonym(Id.EnglishSource, Id.RussianSource, null);
                return this[fIndex];
            }
            else return tryCreat(Id.EnglishSource);
        }
    }
}