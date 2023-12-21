using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
namespace Base.Antonym
{
    public partial class Antonym
    {
        [JsonProperty, SerializeField]
        public List<string> attachments;
    }
}