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

#if UNITY_EDITOR


        public void AddDialog(TList<Dialog> Dialogs)
        {
            string All = "";
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Dialog.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Dialog.txt");
            else Directory.CreateDirectory("Assets/Base/Resources");
            TList<Dialog> AllList = JsonHelper.FromJson<Dialog>(All);
            TList<Dialog> New = new TList<Dialog>();
            AllList.ForEach(a =>
            {
                a = new Dialog(a.EnglishSource.ToUpper(), a.RussianSource, a.Score);
                if (!string.IsNullOrEmpty(a.EnglishSource)) New.AddIfDirty(a);
            });
            Dialogs.ForEach(a =>
            {
                a = new Dialog(a.EnglishSource.ToUpper(), a.RussianSource, a.Score);
                if (!string.IsNullOrEmpty(a.EnglishSource)) New.AddIfDirty(a);
            });
            Debug.Log(New.Count);
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Dialog.txt", JsonHelper.ToJson(New.ToArray()));
            DefalultDialogs = UnityEngine.Resources.Load("Default Dialog", typeof(TextAsset)) as TextAsset;
        }


        [Button]
        //[BoxGroup("Defalult Base/Dialog")]
        [HorizontalGroup("DefalultBase")]////
        public void AddDialog(Dialog dialog)
        {
            string All = "";
            if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Dialogs.txt"))
                All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Dialogs.txt");
            else Directory.CreateDirectory("Assets/Base/Resources");
            TList<Dialog> AllList = JsonHelper.FromJson<Dialog>(All);//++8
            if (!string.IsNullOrEmpty(dialog.EnglishSource)) AllList.AddIfDirty(dialog);
            System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Dialogs.txt", JsonHelper.ToJson(AllList.ToArray()));
            DefalultDialogs = UnityEngine.Resources.Load("Default Dialogs", typeof(TextAsset)) as TextAsset;
        }
#endif
    }
}