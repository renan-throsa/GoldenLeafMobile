using Newtonsoft.Json;
using System;

namespace GoldenLeafMobile.Models.ClerkModels
{
    public class Token
    {
        [JsonProperty("expiration_time")]
        public DateTime ExpirationTime { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
        
    }
}
