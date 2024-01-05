using Base.Dialog;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SystemBox;
using Traonsletor;
using UnityEngine;

namespace Base.Dialog
{
    public class DialogBase : DataList<Dialog>
    {
        static DialogBase()
        {

            if (Application.isEditor && false) 
            {
                if (PlayerPrefs.GetInt(ID + "default set") == 0)
                {
                    PlayerPrefs.SetString(ID, ProjectSettings.ProjectSettings.Mine.DefalultDialogs.text);
                    PlayerPrefs.SetInt(ID + "default set", 1);
                }
            }
            DefaultBase = new List<DialogDefoult>(JsonHelper.FromJson<DialogDefoult>(ProjectSettings.ProjectSettings.Mine.DefalultDialogs.text));
            Dialogs = new DialogBase();
        }
        public Dialog this[Dialog index]
        {
            get
            {
                int fIndex = IndexOf(new Dialog(index.EnglishSource, "", 0, false));
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                return this[fIndex];
            }
            private set
            {
                int fIndex = IndexOf(new Dialog(index.EnglishSource, "", 0, false));
                if (fIndex < 0) throw Tools.ExceptionThrow(new System.IndexOutOfRangeException(), 2);
                this[fIndex] = value;
            }
        }
        public static DialogBase Dialogs { get; set; }
        private static string ID => "DialogBase";
        public override string DataID => ID;

        public static TList<DialogDefoult> DefaultBase;

        public static new void Sort()
        {
            Dialogs.Save();
            Dialogs = new DialogBase();
        }
        public static void AddAllToDefaultBase()
        {
            ProjectSettings.ProjectSettings.Mine.AddDialog(Dialogs);
        }

        public static void AddAll(string D) 
        {
            string[] DD = D.Split(" format_quote ");
            Debug.Log(DD.Length);
            MonoBehaviour MM = GameObject.FindObjectOfType<MonoBehaviour>();
            for (int i = 0; i < DD.Length; i++) TTR(DD[i]);


            void TTR(string ID)
            {
                MM.StartCoroutine(Translator.Process("en", "ru", ID, onWordTransleted));
                
                void onWordTransleted(string tt)
                {
                    Dialog dND = new Dialog(ID, tt, 0, false);
                    if (DialogBase.Dialogs.Contains(dND)) return;
                    Debug.Log(ID);
                    DialogBase.Dialogs.Add(dND);
                }
            }
        }
        public override Dialog tryCreat(string Id) => new Dialog(Id, "", 0, false);
        public override Dialog tryCreat(Content Id)
        {
            if (Id is Dialog) return Id as Dialog;
            if (Id is DialogDefoult) return new Dialog(Id.EnglishSource, Id.RussianSource, 0, false);
            else return tryCreat(Id.EnglishSource);
        }
    }
}
namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        //[BoxGroup("Defalult Base/Dialog")]/
        [HorizontalGroup("DefalultBase")]//
        public TextAsset DefalultDialogs;


        public void AddDialog(TList<Dialog> Dialogs) 
        {
            TList<DialogDefoult> NN = new TList<DialogDefoult>();
            Dialogs.ForEach(d => NN.Add(new DialogDefoult(d.EnglishSource, d.RussianSource) ));
            AddDialog(NN);
        }
        public void AddDialog(TList<DialogDefoult> Dialogs)
        {
#if UNITY_EDITOR
            string All = "";
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Dialog.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Dialog.txt");
            else Directory.CreateDirectory("Assets/Base/Resources");
            TList<DialogDefoult> AllList = JsonHelper.FromJson<DialogDefoult>(All);
            TList<DialogDefoult> New = new TList<DialogDefoult>();
            AllList.ForEach(AddOne);
            Dialogs.ForEach(AddOne);

            void AddOne(DialogDefoult a) 
            {
                if (string.IsNullOrEmpty(a.EnglishSource)) return;
                a.EnglishSource = a.EnglishSource.ToUpper().Replace("!", "").Replace("?", "").Replace(",", "").Replace(".", "");
                New.AddIfDirty(a);
            }
            Array.Sort(New);
            Debug.Log(New.Count);
            string SD = JsonHelper.ToJson(New.ToArray());
            SD = SD.Replace("{", "\n{");
            SD = SD.Replace("},", "\n},");
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Dialog.txt", SD);
            DefalultDialogs = UnityEngine.Resources.Load("Default Dialog", typeof(TextAsset)) as TextAsset;
#endif
        }
    }
}