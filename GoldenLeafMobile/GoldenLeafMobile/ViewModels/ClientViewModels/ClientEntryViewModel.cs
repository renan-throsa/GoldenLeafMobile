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

        public Client Client { get; set; }

        public string Name
        {
            get { return Client.Name; }
            set { Client.Name = value; OnPropertyChanged(); ((Command)SaveClientComand).ChangeCanExecute(); }
        }
        public string Address
        {
            get { return Client.Address; }
            set { Client.Address = value; OnPropertyChanged(); ((Command)SaveClientComand).ChangeCanExecute(); }
        }

        public string Identification
        {
            get { return Client.Identification; }
            set { Client.Identification = value; OnPropertyChanged(); ((Command)SaveClientComand).ChangeCanExecute(); }
        }

        public string PhoneNumber
        {
            get { return Client.PhoneNumber; }
            set { Client.PhoneNumber = value; OnPropertyChanged(); ((Command)SaveClientComand).ChangeCanExecute(); }
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
                MessagingCenter.Send<Client>(Client, "SuccessPostClient");
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (response.Content != null)
                    response.Content.Dispose();


                MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                    "FailedPostClient");

            }

        }
    }
}
