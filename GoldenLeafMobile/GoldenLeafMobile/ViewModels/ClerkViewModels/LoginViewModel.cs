using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClerkViewModels
{
    class LoginViewModel : BaseViewModel
    {
        private readonly string URL_POST_CLERK = "https://golden-leaf.herokuapp.com/api/clerk";
        private string _email;
        private string _password;

        public string Password
        {
            get { return _password; }
            set { _password = value; ((Command)LoginCommand).ChangeCanExecute(); }
        }

        public string Email
        {
            get { return _email; }
            set { _email = value; ((Command)LoginCommand).ChangeCanExecute(); }
        }


        public Command LoginCommand { get; private set; }


        public LoginViewModel()
        {
            LoginCommand = new Command
                (
                    async () =>
                    {
                        await Login();
                    },
                    () =>
                        {
                            return !string.IsNullOrEmpty(_email) && !string.IsNullOrEmpty(_password);
                        }
                );
        }

        private async Task Login()
        {

            HttpClient httpClient = new HttpClient();
            Wait = true;
            var form = new FormUrlEncodedContent(new[]
            {
                   new KeyValuePair<string,string>("username",Email),
                   new KeyValuePair<string,string>("password",Password),
            });


            /*
             * HTTP Basic Authentication --> https://en.wikipedia.org/wiki/Basic_access_authentication
             * While encoding the user name and password with the Base64 algorithm typically makes
             * them unreadable by the naked eye, they are as easily decoded as they are encoded.
             * Security is not the intent of the encoding step. Rather, the intent of the encoding 
             * is to encode non-HTTP-compatible characters that may be in the user name or password
             * into those that are HTTP-compatible.
             */
            var encoded = Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(Email + ":" + Password));
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {encoded}");
            HttpResponseMessage response = null;
            try
            {
                response = await httpClient.PostAsync(URL_POST_CLERK, form);
            }
            catch (Exception)
            {
                var reasonPhrase = "Erro na comunicação";
                var message = @"Ocorreu um erro de comunicação com o servidor.Por favor verifique 
                                a sua conexão e tente novamente mais tarde.";


                MessagingCenter.Send(new LoginException(reasonPhrase, message), "FailedConnection"); ;
            }

            Wait = false;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var clerk = JsonConvert.DeserializeObject<Clerk>(content);
                MessagingCenter.Send(clerk, "SuccessLogin");
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (response.Content != null)
                    response.Content.Dispose();

                MessagingCenter.Send(new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content),
                    "FailedPostClerk");
            }
        }
    }
}
