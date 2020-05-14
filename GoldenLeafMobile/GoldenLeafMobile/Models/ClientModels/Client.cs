using Newtonsoft.Json;
using System.Collections.Generic;

namespace GoldenLeafMobile.Models
{
    public class Client : User
    {
        [JsonProperty("identification")]
        public string Identification { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("notifiable")]
        public bool Notifiable { get; set; }

        public Client()
        {
            Identification = "";
            Status = true;
            Notifiable = true;
        }

       
        public override bool Equals(object obj)
        {
            var another = obj as Client;
            if (another == null)
            {
                return false;
            }
            return Identification.Equals(another.Identification);
        }

        public override int GetHashCode()
        {
            return -1687189325 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public override string ToString()
        {
            return $"Nome: {Name} Telefone: {PhoneNumber} Rg: {Identification} Notificável{Notifiable} Status: {Status}";
        }

        public string ToJson()
        {            
            return JsonConvert.SerializeObject(
                new
                {
                    id = Id,
                    name = Name,
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
