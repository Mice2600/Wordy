using Base;
using Servises;
using Sirenix.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SystemBox;
using UnityEngine;

public static class TagSystem 
{
    static TagSystem() 
    {
        Tags = new List<Tag>();
        JsonHelper.FromJson<string>(PlayerPrefs.GetString("TagSystemIDSaver")).ForEach((a)=> Tags.Add(new Tag(a)));
    }

    public static List<Tag> Tags;
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
                {
                    if (Lisss.Contains(NTag[i])) 
                    {
                        contents.Add(NTag[i]);
                    }
                }

            }


            );

        
         
        


        return contents;

    }
    
}
