using Newtonsoft.Json;
using Servises;
using System.Collections.Generic;
using System.Linq;
using SystemBox;
using UnityEngine;
[System.Serializable]
[JsonObject]
public class Tag
{
    public Tag(string ID) 
    {
        this.ID = ID;
    }
    [JsonProperty]
    [SerializeField]
    public string ID;
    public string this[int Index] 
    {
        get
        {
            try
            {
                return Contents[Index];
            }
            catch (System.Exception XX) { throw Tools.ExceptionThrow(XX, 2); }
        }
    }
    public void Add(string ID) //
    {
        if(!Contents.Contains(ID))Contents.Add(ID); 
    }
    public void Remove(string ID) { Contents.Remove(ID); }
    public int Count => Contents.Count;
    public bool Contains(string ID) => Contents.Contains(ID);
    [JsonProperty]//
    public List<string> Contents = new List<string>();
}
