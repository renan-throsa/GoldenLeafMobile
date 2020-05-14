using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.ClerkModels
{
    public class Clerk : User
    {
        [JsonProperty("email")]
        public string Email { get; set; }

       
    }
}
