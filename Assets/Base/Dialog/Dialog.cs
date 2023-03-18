using Newtonsoft.Json;
using UnityEngine;

namespace Base.Dialog
{
    [System.Serializable]
    public partial class Dialog
    {
        [JsonProperty, SerializeField] public float Score;
        [JsonProperty, SerializeField] public bool Active;
    }
}