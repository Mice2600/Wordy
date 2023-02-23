using Base.Dialog;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System.IO;
using System.Linq;
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
        public static WordBase Wordgs { get; private set; }
        private static string ID => "WordBase";
        protected override string DataID => ID;
        public static new void Sort()
        {
            Wordgs.Save();
            Wordgs = new WordBase();
        }//
#if UNITY_EDITOR
        public static void AddAllToDefaultBase() 
        {
            ProjectSettings.ProjectSettings.Mine.AddWords(Wordgs);
        }

#endif
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
            string All = "";//
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Words.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Words.txt");
            else Directory.CreateDirectory("Assets/Base/Resources");
            TList<Word> AllList = JsonHelper.FromJson<Word>(All);
            TList<Word> New = new TList<Word>();
            AllList.ForEach(AddOne);
            Words.ForEach(AddOne);
            void AddOne(Word a)
            {
                if (string.IsNullOrEmpty(a.EnglishSource)) return;
                a.ScoreConculeated = 0;
                string newID = a.EnglishSource;
                newID = newID.ToUpper();
                newID = newID.Replace("!", "");
                newID = newID.Replace("?", "");
                newID = newID.Replace(",", "");
                newID = newID.Replace(".", "");
                a.EnglishSource = newID;
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