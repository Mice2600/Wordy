using Newtonsoft.Json;
using UnityEngine;

namespace Base.Word
{
    [System.Serializable]
    public partial class Word 
    {
        [JsonProperty, SerializeField] public string EnglishDiscretion;
        [JsonProperty, SerializeField] public string RusianDiscretion;
    }
}