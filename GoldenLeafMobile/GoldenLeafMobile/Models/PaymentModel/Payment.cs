using System;

namespace GoldenLeafMobile.Models.PaymentModel
{
    public class Payment : BaseModel
    {
        public string ClientName { get; set; }
        public int ClientId { get; set; }

        public string ClerkName { get; set; }
        public string ClerkId { get; set; }

        public DateTime Date { get; set; }

        public float Amount { get; set; }


        public string FormatedDate
        {
            get { return Date.ToString("D", new System.Globalization.CultureInfo("pt-BR")); }
        }

    }
}
