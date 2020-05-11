using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClientModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClientViewModels
{
    public class ListViewModel
    {
        private const string URL_GET_CLIENTS = "https://golden-leaf.herokuapp.com/api/client";

        private Client _selectedClient;
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                MessagingCenter.Send(_selectedClient, "SelectedClient");
            }
        }
        public HashSet<Client> Clients { get; set; }
        public ListViewModel()
        {
            Clients = new HashSet<Client>();
        }

        public async Task GetClients()
        {
            HttpClient httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(URL_GET_CLIENTS);
            var ClientsJson = JsonConvert.DeserializeObject<ClientJson[]>(result);

            foreach (var clientJson in ClientsJson)
            {
                Clients.Add(new Client(clientJson));
            }
        }
    }    
}
