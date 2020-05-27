using GoldenLeafMobile.Models;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClientViewModels
{
    public class EditViewModel : BaseViewModel
    {
        private readonly string URL_PUT_CLIENT = "https://golden-leaf.herokuapp.com/api/client";
        public ICommand SaveEditedClientComand { get; set; }

        public Client Client { get; set; }

        public string Name
        {
            get { return Client.Name; }
            set { Client.Name = value; OnPropertyChanged(); ((Command)SaveEditedClientComand).ChangeCanExecute(); }
        }
        public string Address
        {
            get { return Client.Address; }
            set { Client.Address = value; OnPropertyChanged(); ((Command)SaveEditedClientComand).ChangeCanExecute(); }
        }

        public string PhoneNumber
        {
            get { return Client.PhoneNumber; }
            set { Client.PhoneNumber = value; OnPropertyChanged(); ((Command)SaveEditedClientComand).ChangeCanExecute(); }
        }


        public EditViewModel(Client _client)
        {
            Client = _client;
            SaveEditedClientComand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send(Client, "SavingEditedClient");
                    },
                      () =>
                      {
                          return !string.IsNullOrEmpty(Client.Name)
                          && !string.IsNullOrEmpty(Client.Address)
                          && !string.IsNullOrEmpty(Client.PhoneNumber);
                      }

                );
        }

        public async void SaveClient()
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var stringContent = new StringContent(Client.ToJson(), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(URL_PUT_CLIENT, stringContent);
                if (response.IsSuccessStatusCode)
                {
                    MessagingCenter.Send<Client>(Client, "SuccessPutClient");
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null)
                        response.Content.Dispose();

                    MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                        "FailedPutClient");

                }
            }


        }

    }
}
