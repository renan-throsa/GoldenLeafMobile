using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ClientModels;
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
            ViewModel = new EditViewModel(Application.Current.Properties["Clerk"] as Clerk, client);
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Client>(this, ViewModel.ASK, async (_client) =>
            {
                var confirm = await DisplayAlert("Salvar cliente", "Deseja mesmo salvar o cliente?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveClient();
                }
            });

            MessagingCenter.Subscribe<Client>(this, ViewModel.SUCCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar cliente", "Cliente salvo com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<string>(this, ViewModel.ACCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar cliente", $"{_msg} o seu token expirou! Refaça o login.", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, ViewModel.FAIL, (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Client>(this, ViewModel.ASK);
            MessagingCenter.Unsubscribe<Client>(this, ViewModel.SUCCESS);
            MessagingCenter.Unsubscribe<string>(this, ViewModel.ACCESS);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAIL);
        }
    }
}