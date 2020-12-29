using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.PaymentModel
{
    public class Payment
    {
        [JsonProperty("client")]
        public string Client { get; set; }

        [JsonProperty("clerk")]
        public string Clerk { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }
    }
}
