using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models
{
    public abstract class BaseClass
    {
        [JsonProperty("id")]
        public int Id { get; set; }        
        public bool Syncronized { get; set; }
    }
}
