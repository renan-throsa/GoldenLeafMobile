using GoldenLeafMobile.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientDatailsView : ContentPage
    {
        public Client Client { get; set; }
        public ClientDatailsView(Client _client)
        {
            InitializeComponent();
            Client = _client;
            BindingContext = this;
        }

        private void buttonEdit_Clicked(object sender, EventArgs e)
        {

        }
    }
}