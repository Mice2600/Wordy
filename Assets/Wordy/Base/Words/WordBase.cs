using Base.Dialog;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SystemBox;
using Traonsletor;
using UnityEngine;

namespace Base.Word
{
    public class WordBase : DataList<Word>
    {
        static WordBase() 
        {
            if (Application.isEditor && false ) 
            {
                if (PlayerPrefs.GetInt(ID + " defaul") == 0) 
                    PlayerPrefs.SetString(ID, ProjectSettings.ProjectSettings.Mine.DefalultWords.text);
                PlayerPrefs.SetInt(ID + " defaul", 1);
            }
            DefaultBase = new List<WordDefoult>(JsonHelper.FromJson<WordDefoult>(ProjectSettings.ProjectSettings.Mine.DefalultWords.text));
            Wordgs = new WordBase();
        }
        public Word this[Word index]
        {
            get
            {
                int fIndex = IndexOf(new Word(index.EnglishSource, "", 0, false, "", ""));
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                return this[fIndex];
            }
            private set
            {
                int fIndex = IndexOf(new Word(index.EnglishSource, "", 0,false, "", ""));
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                this[fIndex] = value;
            }
        }
        public static WordBase Wordgs { get; set; }
        public static TList<WordDefoult> DefaultBase;
        private static string ID => "WordBase";
        public override string DataID => ID;
        public static new void Sort()
        {
            Wordgs.Save();
            Wordgs = new WordBase();
        }//
        public override Word tryCreat(string EnglishID) => new Word(EnglishID, "", 0, false, "", "");
        public override Word tryCreat(Content Id)
        {
            if(Id is Word)return Id as Word;
            if (Id is WordDefoult) return new Word(Id.EnglishSource, Id.RussianSource, 0, false, (Id as IDiscreption).EnglishDiscretion, (Id as IDiscreption).RusianDiscretion);
            else return tryCreat(Id.EnglishSource);
        }


    }
}
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        //[BoxGroup("Defalult Base/Dialog")]
        [HorizontalGroup("DefalultBaseWord")]//
        public TextAsset DefalultWords;
        //
#if UNITY_EDITOR
        public void AddWords(TList<Word> Words) 
        {
            TList<WordDefoult> NN = new TList<WordDefoult>();
            Words.ForEach(d => NN.Add(new WordDefoult(d.EnglishSource, d.RussianSource, d.EnglishDiscretion,d.RusianDiscretion)));
            AddWords(NN);
        }
        public void AddWords(TList<WordDefoult> Words) 
        {
            string All = "";//
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Words.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Words.txt");
            else Directory.CreateDirectory("Assets/Base/Resources");
            TList<WordDefoult> AllList = JsonHelper.FromJson<WordDefoult>(All);
            TList<WordDefoult> New = new TList<WordDefoult>();
            AllList.ForEach(AddOne);
            Words.ForEach(AddOne);

            void AddOne(WordDefoult a)
            {
                if (string.IsNullOrEmpty(a.EnglishSource)) return;
                a.EnglishSource = a.EnglishSource.ToUpper().Replace("!", "").Replace("?", "").Replace(",", "").Replace(".", "");
                New.AddIfDirty(a);
            }
            Debug.Log(New.Count);
            string SD = JsonHelper.ToJson(New.ToArray());
            SD = SD.Replace("{", "\n{");
            SD = SD.Replace("},", "\n},");
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Words.txt", SD);
            DefalultWords = UnityEngine.Resources.Load("Default Words", typeof(TextAsset)) as TextAsset;
        }
#endif
    }
}