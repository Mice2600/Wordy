using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Base.Word
{
    public partial class Word 
    {
        [JsonProperty, SerializeField] public string EnglishDiscretion;
        [JsonProperty, SerializeField] public string RusianDiscretion;
        [JsonProperty, SerializeField] public float Score;
        [JsonProperty, SerializeField] public bool Active;
    }
}