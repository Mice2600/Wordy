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
    private static List<Tag> Tags 
    {
        get 
        {
            if (_Tags == null) 
            {
                _Tags = new List<Tag>();
                if (PlayerPrefs.GetInt("TagSystemDefaultLode") == 0) 
                {
                    PlayerPrefs.SetInt("TagSystemDefaultLode", 1);//
                    PlayerPrefs.SetString("TagSystemSaver", ProjectSettings.ProjectSettings.Mine.DefaultTags.text);
                }
                JsonHelper.FromJsonList<Tag>(PlayerPrefs.GetString("TagSystemSaver")).ForEach((a) => Tags.Add(a));
                if(_Tags.Find((a) => a.ID == ActiveID) == null) _Tags.AddTo(0, new Tag(ActiveID));
            }
            return _Tags;
        }
    }
    public static string ActiveID => "Active";
    private static List<Tag> _Tags;
    public static TList<string> GetAllTagIdes() 
    {
        TList<string> TagNames = new TList<string>();
        for (int i = 0; i < Tags.Count; i++) TagNames.Add(Tags[i].ID);
        return TagNames;
    }
    public static Tag GetTag(string ID) => Tags.Find((a) => a.ID == ID);
    public static List<Tag> GetAllTags() => Tags;
    public static TList<string> GetBlongTags(string contentID) 
    {
        TList<string> TagNames = new TList<string>();
        for (int i = 0; i < Tags.Count; i++) TagNames.AddIf(Tags[i].ID, Tags[i].Contains(contentID));
        return TagNames;
    }
    public static TList<string> GetAllContentsFromTag(string TagID) 
    {
        if (TagID == ActiveID) 
        {
            TList<string> contents = new TList<string>();
            IDataListComands.DataLists.ForEach(
                (Lisss) =>
                {
                    int Count = Lisss.GetCount();
                    for (int i = 0; i < Count; i++)
                    {
                        Content content = Lisss.GetContent(i);
                        if ((content is IPersanalData))
                            if ((Lisss.GetContent(i) as IPersanalData).Active)
                                contents.Add(content.EnglishSource);
                    }
                }
            );
            return contents;
        }

        return GetTag(TagID).Contents;
        /*
        */
    }
    public static bool ContainsTag(string ID) => GetTag(ID) != null;
    public static bool CreatTag(string NewID) 
    {
        if (string.IsNullOrEmpty(NewID)) return false;
        if (NewID[0] != '@') NewID = "@" + NewID;
        if (ContainsTag(NewID)) return false;
        Tags.Add(new Tag(NewID));
        Save();
        return true;
    }
    public static bool DestroyTag(string NewID)
    {
        if (string.IsNullOrEmpty(NewID)) return false;
        if (NewID[0] != '@') NewID = "@" + NewID;
        if (!ContainsTag(NewID)) return true;
        Tags.Remove(GetTag(NewID));
        Save();
        return true;
    }
    public static bool ChangeTagID(string OldID, string NewID)
    {
        if (string.IsNullOrEmpty(OldID)) return false;
        if (string.IsNullOrEmpty(NewID)) return false;
        if (OldID[0] != '@') OldID = "@" + OldID;
        if (NewID[0] != '@') NewID = "@" + NewID;
        if (!ContainsTag(OldID)) return false;
        if (ContainsTag(NewID)) return false;
        GetTag(OldID).ID = NewID;
        Save();
        return true;
    }
    public static void SaveToDefault() 
    {
        Save();
    }
    public static void Save() 
    {
        PlayerPrefs.SetString("TagSystemSaver", JsonHelper.ToJson<Tag>(Tags));
        string All = "";/////
        if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Tags.txt"))
            All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Tags.txt");
        else Directory.CreateDirectory("Assets/Base/Resources");
        string SD = JsonHelper.ToJson(TagSystem.GetAllTags());
        SD = SD.Replace("{", "\n{");
        SD = SD.Replace("},", "\n},");
        SD = SD.Replace("\",\"", "\",\n\"");
        System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Tags.txt", SD);
        ProjectSettings.ProjectSettings.Mine.DefaultTags = UnityEngine.Resources.Load("Default Tags", typeof(TextAsset)) as TextAsset;
    }


    public static void AddContent(string TagID, string ContentID) 
    {
        if (!ContainsTag(TagID)) return;
        GetTag(TagID).Add(ContentID);
        Save();
    }
    public static void RemoveContent(string TagID, string ContentID) 
    {
        if (!ContainsTag(TagID)) return;
        GetTag(TagID).Remove(ContentID);
        Save();
    }
    public static void AddDeafauldText(string TagID, string Text)
    {
        
        if (!ContainsTag(TagID)) return;
        Tag tag = GetTag(TagID);
        Debug.Log(tag.Contents.Count);
        Text = Text.Replace("\n", "|").Replace("\t", "|").Replace(" ", "|").Replace("||", "|");
        Text.Split("|").ToList().ForEach((OneN)=> { if(!tag.Contents.Contains(OneN.ToUpper())) tag.Contents.Add(OneN.ToUpper()); });
        Debug.Log(Text.Split("|").Length);
        string All = "";///
        if (System.IO.File.Exists(Application.dataPath + "/Base/Resources/Default Tags.txt"))
            All = System.IO.File.ReadAllText(Application.dataPath + "/Base/Resources/Default Tags.txt");
        else Directory.CreateDirectory("Assets/Base/Resources");
        string SD = JsonHelper.ToJson(TagSystem.GetAllTags());
        SD = SD.Replace("{", "\n{");
        SD = SD.Replace("},", "\n},");
        System.IO.File.WriteAllText(Application.dataPath + "/Base/Resources/Default Tags.txt", SD);
        ProjectSettings.ProjectSettings.Mine.DefaultTags = UnityEngine.Resources.Load("Default Tags", typeof(TextAsset)) as TextAsset;
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