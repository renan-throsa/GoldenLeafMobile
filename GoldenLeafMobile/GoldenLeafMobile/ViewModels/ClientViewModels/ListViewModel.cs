using GoldenLeafMobile.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClientViewModels
{
    public class ListViewModel : BaseViewModel
    {
        private const string URL_GET_CLIENTS = "https://golden-leaf.herokuapp.com/api/client";

        public ObservableCollection<Client> Clients { get; set; }


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


        public ListViewModel()
        {
            Clients = new ObservableCollection<Client>();
        }

        public async Task GetClients()
        {
            Wait = true;
            HttpClient httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(URL_GET_CLIENTS);

            var ClientsList = JsonConvert.DeserializeObject<List<Client>>(result);

            foreach (var clientJson in ClientsList)
            {
                Clients.Add(clientJson);
            }
            Wait = false;
        }
    }
}
