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

        public DateTime LastPurchase { get; set; }


        public string FormatedLastPurchase
        {
            get { return LastPurchase.ToString("D", new System.Globalization.CultureInfo("pt-BR")); }
        }
        public string FormatedDebt
        {
            get { return $"R$ {Debt}"; }
        }

        public string FormatedStatus
        {
            get { return Status ? "Sim" : "Não"; }
        }


        public Client()
        {
            Status = true;
            Notifiable = true;
        }


        public string ToJson()
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
