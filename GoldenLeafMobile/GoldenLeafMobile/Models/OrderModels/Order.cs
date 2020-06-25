using Newtonsoft.Json;
using System;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class Order : BaseModel
    {
        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("clerk")]
        public string Clerk { get; set; }
                
        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("total")]
        public float Total { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        public override string ToJson()
        {
            return "Not Necessary";
        }
    }
}
