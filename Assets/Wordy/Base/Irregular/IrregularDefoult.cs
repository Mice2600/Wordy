using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class IrregularDefoult
{
    [JsonProperty, SerializeField] public string EnglishDiscretion;
    [JsonProperty, SerializeField] public string RusianDiscretion;
    [JsonProperty, SerializeField] public string SimplePast;
    [JsonProperty, SerializeField] public string PastParticiple;
}
