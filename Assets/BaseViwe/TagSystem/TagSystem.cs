using Base;
using Servises;
using System.Collections.Generic;
using SystemBox;
using UnityEngine;

public static class TagSystem 
{
    static TagSystem() 
    {
        Tags = new List<Tag>();
        JsonHelper.FromJsonList<string>(PlayerPrefs.GetString("TagSystemIDSaver")).ForEach((a)=> Tags.Add(new Tag(a)));
    }
    private static List<Tag> Tags;
    public static TList<string> GetAllTagIdes() 
    {
        TList<string> TagNames = new TList<string>();
        for (int i = 0; i < Tags.Count; i++) TagNames.Add(Tags[i].ID);
        return TagNames;
    }
    public static Tag GetTag(string ID) => Tags.Find((a) => a.ID == ID);
    public static TList<string> GetBlongTags(string contentID) 
    {
        TList<string> TagNames = new TList<string>();
        for (int i = 0; i < Tags.Count; i++) TagNames.AddIf(Tags[i].ID, Tags[i].Contains(contentID));
        return TagNames;
    }
    public static TList<Content> GetAllContentsFromTag(string TagID) 
    {
        TList<Content> contents = new TList<Content>();
        Tag NTag = GetTag(TagID);
        IDataListComands.DataLists.ForEach(
            (Lisss) =>
            {
                for (int i = 0; i < NTag.Count; i++)
                    if (Lisss.Contains(NTag[i])) 
                        contents.Add(Lisss.GetContent(NTag[i]));
            }
        );
        return contents;
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
    public static void Save() 
    {
        List<string> TagIdes = new List<string>();
        Tags.ForEach((a) => TagIdes.Add(a.ID));
        PlayerPrefs.SetString("TagSystemIDSaver", JsonHelper.ToJson<string>(TagIdes));
    }


    public static void AddContent(string TagID, string ContentID) 
    {
        if (!ContainsTag(TagID)) return;
        GetTag(TagID).Add(ContentID);
    }
    public static void RemoveContent(string TagID, string ContentID) 
    {
        if (!ContainsTag(TagID)) return;
        GetTag(TagID).Remove(ContentID);
    }

}
