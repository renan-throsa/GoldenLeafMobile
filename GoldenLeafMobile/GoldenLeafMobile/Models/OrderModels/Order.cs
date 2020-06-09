using Newtonsoft.Json;
using System;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class Order : BaseModel
    {
        [JsonProperty("client_id ")]
        public int ClientId { get; set; }

        [JsonProperty("clerk_id")]
        public int ClerkId { get; set; }

        [JsonProperty("payment_id")]
        public int PaymentId { get; set; }

        [JsonProperty("ordered")]
        public DateTime Date { get; set; }

        [JsonProperty("total")]
        public float Total { get; set; }

        [JsonProperty("status")]
        public Status Status { get; set; }

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    id = Id,
                    client_id = ClientId,
                    clerk_id = ClerkId,
                    payment_id = PaymentId,
                    ordered = Date.ToString("O"),
                    total = Total,
                    status = Status.ToString()
                }
                ); 
        }
    }
}
