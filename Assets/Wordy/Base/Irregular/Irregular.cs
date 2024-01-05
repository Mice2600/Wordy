using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Irregular
{
    [JsonProperty, SerializeField] public string EnglishDiscretion;
    [JsonProperty, SerializeField] public string RusianDiscretion;
    [JsonProperty, SerializeField] public string SimplePast;
    [JsonProperty, SerializeField] public string PastParticiple;
    [JsonProperty, SerializeField] public float Score;
    [JsonProperty, SerializeField] public bool Active;
}
