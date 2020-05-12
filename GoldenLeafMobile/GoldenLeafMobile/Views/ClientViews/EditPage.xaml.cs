using GoldenLeafMobile.Models;
using GoldenLeafMobile.ViewModels.ClientViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        public EditViewModel ViewModel { get; set; }

        public EditPage(Client client)
        {
            InitializeComponent();
            ViewModel = new EditViewModel(client);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Client>(this, "SavingEditedClient", async (_client) =>
            {
                var confirm = await DisplayAlert("Salvar cliente", "Deseja mesmo salvar o cliente?", "Não", "Sim");
                if (confirm)
                {
                    ViewModel.SaveClient();
                }
            });

            MessagingCenter.Subscribe<Client>(this, "SuccessPutClient", (_msg) =>
            {
                DisplayAlert("Salvar client", "Cliente salvo com sucesso!", "Ok");
            });


            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, "FailedPutClient", (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Client>(this, "SavingEditedClient");
            MessagingCenter.Unsubscribe<Client>(this, "SuccessPutClient");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FailedPutClient");
        }
    }
}