using GoldenLeafMobile.Data;
using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ClientModels;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClientViewModels
{
    public abstract class BaseClientViewModel
    {
        public Clerk Clerk { get; set; }
        private readonly string URL_CLIENT = "https://golden-leaf.herokuapp.com/api/client";
        public readonly string SUCCESS = "OnSuccessSavingClient";
        public readonly string FAIL = "OnFailedSavingClient";
        public readonly string ASK = "OnSavingClient";
        public readonly string ACCESS = "OnRequestUnauthorized";


        public ICommand SaveClientComand { get; set; }

        public Client Client { get; private set; }


        public string Address
        {
            get { return Client.Address; }
            set { Client.Address = value; ((Command)SaveClientComand).ChangeCanExecute(); }
        }


        public string PhoneNumber
        {
            get { return Client.PhoneNumber; }
            set { Client.PhoneNumber = value; ((Command)SaveClientComand).ChangeCanExecute(); }
        }

        public BaseClientViewModel(Client client)
        {
            Client = client;
            MessagingCenter.Subscribe<Clerk>(this, "CurrentClerk", (_clerk) =>
            {
                this.Clerk = _clerk;
            });

        }

        public async void SaveClient()
        {
            var b = this.Clerk.IsTokenExperationTimeValid();

            if (!this.Clerk.IsTokenExperationTimeValid())
            {
                MessagingCenter.Send<string>(Clerk.Name, ACCESS);
            }

            using (HttpClient httpClient = new HttpClient())
            {
                var stringContent = new StringContent(Client.ToJson(), Encoding.UTF8, "application/json");
                var response = new HttpResponseMessage();
                if (Client.Id == 0)
                {
                    response = await httpClient.PostAsync(URL_CLIENT, stringContent);
                }
                else
                {
                    response = await httpClient.PutAsync(URL_CLIENT, stringContent);
                }

                if (response.IsSuccessStatusCode)
                {
                    Client.Syncronized = true;
                    MessagingCenter.Send<Client>(Client, SUCCESS);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null)
                        response.Content.Dispose();

                    Client.Syncronized = false;
                    MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                        FAIL);
                }

            }

            SaveCLientInternaly();

        }

        private void SaveCLientInternaly()
        {
            using (var connection = DependencyService.Get<ISQLite>().GetConnection())
            {
                var dao = new Repository<Client>(connection);
                dao.Save(this.Client);
            }

        }
    }
}
