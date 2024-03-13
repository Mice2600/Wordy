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
    
}
