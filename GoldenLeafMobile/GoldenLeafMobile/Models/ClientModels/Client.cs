using Newtonsoft.Json;
using System;

namespace GoldenLeafMobile.Models.ClientModels
{
    public class Client : BaseModel
    {

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public float Debt { get; set; }

        public string Address { get; set; }

        public bool Status { get; set; }

        public bool Notifiable { get; set; }

        public DateTime lastPurchase { get; set; }

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
                    address = Address,
                    phoneNumber = PhoneNumber,
                    notifiable = Notifiable,
                }
           );
        }
    }
}
