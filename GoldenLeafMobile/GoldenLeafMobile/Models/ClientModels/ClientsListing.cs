using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLeafMobile.Models.ClientModels
{
    class ClientsListing
    {
        public HashSet<Client> Clients { get; set; }
        public ClientsListing()
        {
            Clients = new HashSet<Client>() { new Client {Name="Fulano",PhoneNumber="98291510" },
            new Client {Name="Betrano",PhoneNumber="98291510" },
            new Client {Name="Ciclano",PhoneNumber="98291510",Identification="123456",Status=true,Notifiable=false }};
        }
    }
}
