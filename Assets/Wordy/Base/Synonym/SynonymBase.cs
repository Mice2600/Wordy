using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using System.Linq;
using SystemBox;
using UnityEngine;
namespace Base.Synonym
{
    
    public class SynonymBase : DataList<Synonym>
    {
        static SynonymBase()
        {
            //if (Application.isEditor && false)
            //{
            //    if (PlayerPrefs.GetInt(ID + " defaul") == 0)
            //        PlayerPrefs.SetString(ID, ProjectSettings.ProjectSettings.Mine.DefalultSynonym.text);
            //    PlayerPrefs.SetInt(ID + " defaul", 1);
            //}
            //DefaultBase = new List<WordDefoult>(JsonHelper.FromJson<WordDefoult>(ProjectSettings.ProjectSettings.Mine.DefalultWords.text));
            Synonyms = new SynonymBase();
        }

        public SynonymBase()  : base() 
        {

            List<Synonym> dd = JsonHelper.FromJson<Synonym>(PlayerPrefs.GetString(DataID)).ToList();
            dd = dd.Where(d => d.attachments.Count > 0).ToList();
            //Sirenix.Utilities.StringExtensions.CalculateLevenshteinDistance("aa", "aa");
            string NewData = JsonHelper.ToJson(dd);
                //JsonHelper.ToJson(JsonHelper.FromJson<Synonym>(PlayerPrefs.GetString(DataID)).Where(d => d.attachments.Count > 0));
            PlayerPrefs.SetString(DataID, NewData);

            /*for (int i = 0; i < Synonyms.Count; i++)
                if (Synonyms[i].attachments.Count == 0) 
                {
                    Synonyms.RemoveAt(i);
                    i = i - 1;
                }*/
        }

        public Synonym this[Content index]
        {
            get
            {
                int fIndex = IndexOf(new Synonym(index.EnglishSource, "", null));
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                return this[fIndex];
            }
            private set
            {
                int fIndex = IndexOf(new Synonym(index.EnglishSource, "", null));
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                this[fIndex] = value;
            }
        }
        [Searchable]
        public static SynonymBase Synonyms { get; set; }
        public int UsebleCount { 
            get {
                return Synonyms.Where(a => Word.WordBase.Wordgs.Contains(a)).Count();
            } 
        }
        public override string DataID => "SynonymBase";

        public override System.Collections.Generic.List<Synonym> ActiveItems => Synonyms.Where(s =>
        {

            int I = Word.WordBase.Wordgs.IndexOf(s);
            if (I > -1)
            {
                return (Word.WordBase.Wordgs[I] as IPersanalData).Active == true;
            }
            return false;
        }).ToList();

        public static void AddSynonym(Content Source, string Synonyms) => AddSynonym(Source, new TList<string>(Synonyms));
        public static void AddSynonym(Content Source, TList<string> Synonyms)
        {
            for (int i = 0; i < Synonyms.Count; i++) Synonyms[i] = Synonyms[i].ToUpper();
            Synonym SynonymWord = SynonymBase.Synonyms.Find(d => d.EnglishSource.ToUpper() == Source.EnglishSource.ToUpper());


            if (SynonymWord == null) 
            {
                SynonymBase.Synonyms.Add(new Synonym(Source.EnglishSource.ToUpper(), Source.RussianSource, Synonyms));
            }
            else SynonymWord.Attach(Synonyms);


            for (int i = 0; i < Synonyms.Count; i++)
            {
                Synonym SynonymOne = SynonymBase.Synonyms.Find(d => d.EnglishSource.ToUpper() == Synonyms[i]);
                if (SynonymOne == null)
                    SynonymBase.Synonyms.Add(new Synonym(Synonyms[i], "", new TList<string>(Source.EnglishSource.ToUpper())));
                else SynonymOne.Attach(Source.EnglishSource.ToUpper());
            }
            SynonymBase.Synonyms.Save();
        }


        public static void RemoveSynonym(string Synonym) 
        { 
            Synonym = Synonym.ToUpper();
            Synonym SynonymWord = SynonymBase.Synonyms.Find(d => d.EnglishSource.ToUpper() == Synonym);
            if (SynonymWord != null) {SynonymBase.Synonyms.Remove(SynonymWord);}
            Synonyms.ForEach(Ot =>{Ot.attachments.Remove(Synonym);});
            SynonymBase.Synonyms.Save();
        }

        public override Synonym tryCreat(string EnglishID) => new Synonym(EnglishID, "", null);
        public override Synonym tryCreat(Content Id)
        {
            if (Id is Synonym) return Id as Synonym;
            if (Id is Base.Word.Word) 
            {
                int fIndex = IndexOf(new Synonym(Id.EnglishSource, "", null));
                if (fIndex < 0) return new Synonym(Id.EnglishSource, Id.RussianSource, null);
                return this[fIndex];
            }
            else return tryCreat(Id.EnglishSource);
        }


        public static string LogData() 
        {
            return PlayerPrefs.GetString(SynonymBase.Synonyms.DataID);
        }
        public static void SetData(string d)
        {
            PlayerPrefs.SetString(SynonymBase.Synonyms.DataID, d);
            Synonyms = new SynonymBase();
        }

    }
}
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        //[BoxGroup("Defalult Base/Dialog")]
        //[HorizontalGroup("DefalultBaseSynonym")]//
        //public TextAsset DefalultSynonym;
        //
#if UNITY_EDITOR
        //public void AddWords(TList<Word> Words)
        //{
        //    TList<WordDefoult> NN = new TList<WordDefoult>();
        //    Words.ForEach(d => NN.Add(new WordDefoult(d.EnglishSource, d.RussianSource, d.EnglishDiscretion, d.RusianDiscretion)));
        //    AddWords(NN);
        //}
        //public void AddWords(TList<WordDefoult> Words)
        //{
        //    string All = "";//
        //    if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Words.txt"))
        //        All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Words.txt");
        //    else Directory.CreateDirectory("Assets/Base/Resources");
        //    TList<WordDefoult> AllList = JsonHelper.FromJson<WordDefoult>(All);
        //    TList<WordDefoult> New = new TList<WordDefoult>();
        //    AllList.ForEach(AddOne);
        //    Words.ForEach(AddOne);

        //    void AddOne(WordDefoult a)
        //    {
        //        if (string.IsNullOrEmpty(a.EnglishSource)) return;
        //        a.EnglishSource = a.EnglishSource.ToUpper().Replace("!", "").Replace("?", "").Replace(",", "").Replace(".", "");
        //        New.AddIfDirty(a);
        //    }
        //    Debug.Log(New.Count);
        //    string SD = JsonHelper.ToJson(New.ToArray());
        //    SD = SD.Replace("{", "\n{");
        //    SD = SD.Replace("},", "\n},");
        //    System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Words.txt", SD);
        //    DefalultWords = UnityEngine.Resources.Load("Default Words", typeof(TextAsset)) as TextAsset;
        //}
#endif
    }
}