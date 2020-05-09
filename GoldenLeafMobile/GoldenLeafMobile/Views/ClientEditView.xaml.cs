using GoldenLeafMobile.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientEditView : ContentPage
    {
        public Client Client { get; set; }

        public ClientEditView(Client client)
        {
            InitializeComponent();
            Client = client;
            BindingContext = this;
        }

        private void buttonSave_Clicked(object sender, EventArgs e)
        {
            DisplayAlert("Cliente salvo", Client.ToString(), "ok");
        }
    }
}