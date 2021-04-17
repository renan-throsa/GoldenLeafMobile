
using System;

namespace GoldenLeafMobile.Models.OrderModels
{
    public class Order : BaseModel
    {
        public string ClientName { get; set; }
        public int ClientId { get; set; }

        public string ClerkName { get; set; }
        public string ClerkId { get; set; }

        public DateTime Date { get; set; }

        public float Value { get; set; }

        public Status Status { get; set; }

        
        public string FormatedDate
        {            
            get { return Date.ToString("D", new System.Globalization.CultureInfo("pt-BR")); }
        }


        public override string ToJson()
        {
            return "Not Necessary";
        }
    }
}
