using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.ClientModels
{
    public class ClientJson
    {
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string identification { get; set; }
        public string phone_number { get; set; }
        public bool status { get; set; }
        public bool notifiable { get; set; }

    }
}
