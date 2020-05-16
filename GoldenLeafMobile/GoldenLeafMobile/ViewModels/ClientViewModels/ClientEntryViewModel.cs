using GoldenLeafMobile.Data;
using GoldenLeafMobile.Models;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClientViewModels
{
    public class ClientEntryViewModel : BaseViewModel
    {
        private readonly string URL_POST_CLIENT = "https://golden-leaf.herokuapp.com/api/client";
        public ICommand SaveClientComand { get; set; }

        public Client Client { get; private set; }

        public string Name
        {
            get { return Client.Name; }
            private set { Client.Name = value; ((Command)SaveClientComand).ChangeCanExecute(); }
        }
        public string Address
        {
            get { return Client.Address; }
            private set { Client.Address = value; ((Command)SaveClientComand).ChangeCanExecute(); }
        }

        public string Identification
        {
            get { return Client.Identification; }
            private set { Client.Identification = value; ((Command)SaveClientComand).ChangeCanExecute(); }
        }

        public string PhoneNumber
        {
            get { return Client.PhoneNumber; }
            private set { Client.PhoneNumber = value; ((Command)SaveClientComand).ChangeCanExecute(); }
        }

        public ClientEntryViewModel()
        {
            Client = new Client();
            SaveClientComand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send<Client>(Client, "SavingClient");

                    },
                    () =>
                    {
                        return !string.IsNullOrEmpty(Client.Name)
                        && !string.IsNullOrEmpty(Client.Address)
                        && !string.IsNullOrEmpty(Client.Identification)
                        && !string.IsNullOrEmpty(Client.PhoneNumber);
                    }
                );
        }

        public async void SaveClient()
        {
            HttpClient httpClient = new HttpClient();
            var stringContent = new StringContent(Client.ToJson(), Encoding.UTF8, "application/json");


            var response = await httpClient.PostAsync(URL_POST_CLIENT, stringContent);

            if (response.IsSuccessStatusCode)
            {
                Client.Syncronized = true;                
                MessagingCenter.Send<Client>(Client, "SuccessPostClient");
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (response.Content != null)
                    response.Content.Dispose();

                Client.Syncronized = false;
                MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                    "FailedPostClient");

            }

            SaveCLientInternaly();

        }

        private void SaveCLientInternaly()
        {
            var connection = DependencyService.Get<ISQLite>().GetConnection();
            var dao = new ClientDAO(connection);
            dao.Save(this.Client);
        }
    }
}
