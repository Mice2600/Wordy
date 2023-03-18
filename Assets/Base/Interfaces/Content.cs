using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public  abstract partial class Content // Data
{
    [JsonProperty] public string EnglishSource;
    [JsonProperty] public string RussianSource;
    
}
