using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ClientModels;
using GoldenLeafMobile.Models.PaymentModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.PaymentViewModel
{
    public class PaymentEntryViewModel
    {
        private readonly string URL_PAYMENT = "https://golden-leaf.herokuapp.com/payment/new/client/";
        public readonly string SUCCESS = "SuccessSavingPayment";
        public readonly string FAIL = "FailedSavingPayment";
        public readonly string ASK = "SavingPayment";



        public ICommand PayCommand { get; set; }

        public float Value { get; set; }
        public float Total { get; set; }
        public Client Client { get; }
        public Clerk Clerk { get; }


        public PaymentEntryViewModel(Client client)
        {
            PayCommand = new Command
                (
                    () =>
                    {
                        //MessagingCenter.Send<string>(String, ASK);
                    },
                    () =>
                    {
                        return Value > 0 && Value <= Total;
                    }
                );
            Client = client;
            Clerk = Application.Current.Properties["Clerk"] as Clerk;
        }

        public async void GetTotal()
        {

        }

        public async void SavePayment()
        {
            using (HttpClient httpClient = new HttpClient())
            {

                var stringContent = new StringContent(PaymentToJson(), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(URL_PAYMENT, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    MessagingCenter.Send<String>("Pagamento recebido com sucesso!", SUCCESS);
                }
                else
                {
                    var content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    if (response.Content != null)
                    {
                        response.Content.Dispose();
                    }
                    var simpleHttpResponse = new SimpleHttpResponseException(response.StatusCode, response.ReasonPhrase, content);
                    MessagingCenter.Send(simpleHttpResponse, FAIL);

                }
            }
        }

        private string PaymentToJson()
        {
            var payload = new Dictionary<string, object>(){
            { "client_id", Client.Id },
            { "clerk_id", Clerk.Id },
                {"amount", Value }
            };

            var secretKey = Application.Current.Properties["Secret"] as String;
            string token = JWT.JsonWebToken.Encode(payload, secretKey, JWT.JwtHashAlgorithm.HS256);
            var payment = JsonConvert.SerializeObject(new
            {
                payment = token
            }
             );
            return payment;

        }
    }
}
