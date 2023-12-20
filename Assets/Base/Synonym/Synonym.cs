using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Base.Synonym
{
    public partial class Synonym
    {
        [JsonProperty, SerializeField]
        public List<string> attachments;
    }
}