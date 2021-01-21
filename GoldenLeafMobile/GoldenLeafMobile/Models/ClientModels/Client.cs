using Newtonsoft.Json;

namespace GoldenLeafMobile.Models.ClientModels
{
    public class Client : User
    {
        [JsonProperty("identification")]
        public string Identification { get; set; }

        [JsonProperty("amount")]
        public float Amount { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("notifiable")]
        public bool Notifiable { get; set; }

        public Client()
        {
            Status = true;
            Notifiable = true;
        }
               

        public override string ToJson()
        {
            return JsonConvert.SerializeObject(
                new
                {
                    id = Id,
                    name = Name,
                    amount = Amount,
                    address = Address,
                    identification = Identification,
                    phone_number = PhoneNumber,
                    notifiable = Notifiable,
                    status = Status
                }
           );
        }
    }
}
