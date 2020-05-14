using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models
{
    public abstract class User : BaseClass
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("phone_number")]
        public string PhoneNumber { get; set; }
    }
}
