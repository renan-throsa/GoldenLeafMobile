using GoldenLeafMobile.Models;
using GoldenLeafMobile.ViewModels.ClientViewModels;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ClientViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientEntryPage : ContentPage
    {
        public ClientEntryViewModel ViewModel { get; set; }
        public ClientEntryPage()
        {
            InitializeComponent();
            ViewModel = new ClientEntryViewModel();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            SignUpMessages();

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
            if (status != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Storage))
                {
                    await DisplayAlert("Acesso ao Armazenamento Externo", "É preciso dar permissão para Armazenamento Externo", "OK","Cancelar");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Storage);
                status = results[Permission.Storage];
            }

            if (status != PermissionStatus.Unknown)
            {
                await DisplayAlert("Armazenamento Externo Negado", "Não é possível continuar", "OK");
                await Navigation.PopToRootAsync();
            }

        }

        private void SignUpMessages()
        {
            MessagingCenter.Subscribe<Client>(this, "SavingClient", async (_client) =>
            {
                var confirm = await DisplayAlert("Salvar client", "Deseja mesmo salvar o cliente?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveClient();
                }
            });

            MessagingCenter.Subscribe<Client>(this, "SuccessPostClient", async (_msg) =>
            {
                await DisplayAlert("Salvar client", "Cliente salvo com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, "FailedPostClient", (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Client>(this, "SuccessPostClient");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FailedPostClient");
        }

    }
}