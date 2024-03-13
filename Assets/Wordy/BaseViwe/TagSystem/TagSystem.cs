using Base;
using Base.Word;
using Servises;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SystemBox;
using UnityEngine;

public static class TagSystem 
{
    private static List<string> Tags 
    {
        get 
        {
            if (_Tags == null) 
            {
                _Tags = new List<string>();
                if (PlayerPrefs.GetInt("TagSystemDefaultLode") == 0) 
                {
                    PlayerPrefs.SetInt("TagSystemDefaultLode", 1);//
                    PlayerPrefs.SetString("TagSystemSaver", ProjectSettings.ProjectSettings.Mine.DefaultTags.text);
                }
                JsonHelper.FromJsonList<string>(PlayerPrefs.GetString("TagSystemSaver")).ForEach((a) => Tags.Add(a));
                if(_Tags.Find((a) => a == ActiveID) == null) _Tags.AddTo(0, ActiveID);
            }
            return _Tags;
        }
    }
    private static List<string> _Tags;
    public static string ActiveID => "Active";
    public static string VerbID => "Verb";
    public static string AdjectiveID => "Verb";
    public static string NounID => "Verb";
    public static string NounID => "Verb";
    
    public static bool IsContentBlongToTag(string Tag, string ContentID) 
    {
        string Resolt = GetTag(Tag).Contents.Find(TC => TC == ContentID);
        return  !string.IsNullOrEmpty(Resolt);
    }


    public static List<Content> WhereTegHasContent(string Tag, List<Content> Contents)
    {
        List<string> TagContents = GetTag(Tag).Contents;
        return Contents.Where(I => TagContents.Contains(I.EnglishSource)).ToList();
    }



    public static bool ContainsTag(string ID) => GetTag(ID) != null;
    public static bool CreatTag(string NewID) 
    {
       
        return true;
    }
    public static bool DestroyTag(string NewID)
    {
        return false;
    }
   
    public static void Save() 
    {
        PlayerPrefs.SetString("TagSystemSaver", JsonHelper.ToJson<string>(Tags));
        string All = "";/////
        if (System.IO.File.Exists(Application.dataPath + "/Wordy/Base/Resources/Default Tags.txt"))
            All = System.IO.File.ReadAllText(Application.dataPath + "/Wordy/Base/Resources/Default Tags.txt");
        else Directory.CreateDirectory("Assets/Wordy/Base/Resources");
        string SD = JsonHelper.ToJson(TagSystem.GetAllTags());
        System.IO.File.WriteAllText(Application.dataPath + "/Wordy/Base/Resources/Default Tags.txt", SD);
        ProjectSettings.ProjectSettings.Mine.DefaultTags = UnityEngine.Resources.Load("Default Tags", typeof(TextAsset)) as TextAsset;
    }


    public static void AddContent(string TagID, string ContentID) 
    {

        Save();
    }
    public static void RemoveContent(string TagID, string ContentID) 
    {
        Save();
    }

}

namespace ProjectSettings
{
    public partial class ProjectSettings
    {
        //[BoxGroup("Defalult Base/Dialog")]
        [HorizontalGroup("DefalultBaseWord")]//
        public TextAsset DefaultTags;
    }
}


interface Tagable
{
    public List<string> Tags { get; set; }
    public bool IsAnyThereTag() => !Tags.IsEnpty();
    public bool IsThereTag(string NeedTag) => !string.IsNullOrEmpty(Tags.Find(F => F == NeedTag));
    public bool RemoveTag(string Tag) => Tags.Remove(Tag);
    public void AddTag(string Tag) => Tags.Add(Tag);
}
