using Base.Dialog;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.IO;
using SystemBox;
using UnityEngine;

namespace Base.Word
{
    public class WordBase : DataList<Word>
    {

        static WordBase() 
        {

            if (PlayerPrefs.GetInt(ID + " defaul") == 0) 
                PlayerPrefs.SetString(ID, ProjectSettings.ProjectSettings.Mine.DefalultWords.text);
            PlayerPrefs.SetInt(ID + " defaul", 1);
            Wordgs = new WordBase(); 
        }
        public Word this[IContent index]
        {
            get
            {
                int fIndex = IndexOf(new Word(index.EnglishSource, "", 0, "", "") );
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                return this[fIndex];
            }
            set
            {
                int fIndex = IndexOf(new Word(index.EnglishSource, "", 0, "", ""));
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                this[fIndex] = value;
            }
        }
        public static WordBase Wordgs { get; private set; }
        private static string ID => "WordBase";
        protected override string DataID => ID;
        public static new void Sort()
        {
            Wordgs.Save();
            Wordgs = new WordBase();
        }//
        public static void AddAllToDefaultBase() 
        {
            ProjectSettings.ProjectSettings.Mine.AddWords(Wordgs);
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

#if UNITY_EDITOR
        
        public void AddWords(TList<Word> Words) 
        {
            string All = "";//
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Words.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Words.txt");
            else Directory.CreateDirectory("Assets/Base/Resources");
            TList<Word> AllList = JsonHelper.FromJson<Word>(All);
            TList<Word> New = new TList<Word>();
            AllList.ForEach(a =>
            {
                a = new Word(a.EnglishSource.ToUpper(), a.RussianSource, a.Score, a.EnglishDiscretion, a.RusianDiscretion);
                if (!string.IsNullOrEmpty(a.EnglishSource)) New.AddIfDirty(a);
            });
            Words.ForEach(a => 
            {
                a = new Word(a.EnglishSource.ToUpper(), a.RussianSource, a.Score, a.EnglishDiscretion, a.RusianDiscretion);
                if (!string.IsNullOrEmpty(a.EnglishSource)) New.AddIfDirty(a);
            });
            Debug.Log(New.Count);
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Words.txt", JsonHelper.ToJson(New.ToArray()));
            DefalultWords = UnityEngine.Resources.Load("Default Words", typeof(TextAsset)) as TextAsset;
        }
        [Button]
        [HorizontalGroup("DefalultBaseWord")]////
        public void AddWord(Word Word)
        {
            string All = "";
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Words.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Words.txt");
            else Directory.CreateDirectory("Assets/Base/Resources");
            TList<Word> AllList = JsonHelper.FromJson<Word>(All);///514
            if(!string.IsNullOrEmpty( Word.EnglishSource )) AllList.AddIfDirty(Word);
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Words.txt", JsonHelper.ToJson(AllList.ToArray()));
            DefalultWords = UnityEngine.Resources.Load("Default Words", typeof(TextAsset)) as TextAsset;
        }
#endif
    }
}