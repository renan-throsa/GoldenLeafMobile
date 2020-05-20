using GoldenLeafMobile.Models;
using GoldenLeafMobile.ViewModels.ClientViewModels;
using GoldenLeafMobile.Views.ClientViews;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace GoldenLeafMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ClientsPage : ContentPage
    {
        public ListViewModel ViewModel { get; set; }
        public ClientsPage()
        {
            InitializeComponent();
            ViewModel = new ListViewModel();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            clientsListView.SelectedItem = false;
            MessagingCenter.Subscribe<Client>(this, "SelectedClient",
                (_client) => Navigation.PushAsync(new DatailsPage(_client)));

            await ViewModel.GetClients();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Client>(this, "SelectedClient");
        }

        public void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Navigation.PushAsync(new EditPage(mi.CommandParameter as Client));
        }

        public void OnNewOrder(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Novo pedido", mi.CommandParameter.ToString(), "OK");
        }

        private void ToolbarNewClient_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ClientEntryPage());
        }


    }
}
