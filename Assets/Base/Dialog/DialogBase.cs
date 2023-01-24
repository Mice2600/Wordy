using Base.Dialog;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using SystemBox;
using UnityEngine;

namespace Base.Dialog
{
    public class DialogBase : DataList<Dialog>
    {
        static DialogBase()
        {
            if (PlayerPrefs.GetInt(ID + "default set") == 0) 
            {
                PlayerPrefs.SetString(ID, ProjectSettings.ProjectSettings.Mine.DefalultDialogs.text);
                PlayerPrefs.SetInt(ID + "default set", 1);
            }
            
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
        public static DialogBase Dialogs { get; private set; }
        private static string ID => "DialogBase";
        protected override string DataID => ID;
        public static new void Sort()
        {
            Dialogs.Save();
            Dialogs = new DialogBase();
        }
        public static void AddAllToDefaultBase()
        {
            ProjectSettings.ProjectSettings.Mine.AddDialog(Dialogs);
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
#if UNITY_EDITOR
            string All = "";
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Dialog.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Dialog.txt");
            else Directory.CreateDirectory("Assets/Base/Resources");
            TList<Dialog> AllList = JsonHelper.FromJson<Dialog>(All);
            TList<Dialog> New = new TList<Dialog>();
            AllList.ForEach(AddOne);
            Dialogs.ForEach(AddOne);

            void AddOne(Dialog a) 
            {
                if (string.IsNullOrEmpty(a.EnglishSource)) return;
                a.Score = 0;
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
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Dialog.txt", JsonHelper.ToJson(New.ToArray()));
            DefalultDialogs = UnityEngine.Resources.Load("Default Dialog", typeof(TextAsset)) as TextAsset;
#endif
        }
    }
}