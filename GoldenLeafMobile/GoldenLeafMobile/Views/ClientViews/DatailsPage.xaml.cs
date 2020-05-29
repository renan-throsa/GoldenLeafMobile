using GoldenLeafMobile.Models.ClientModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ClientViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatailsPage : ContentPage
    {
        public Client Client { get; set; }
        public DatailsPage(Client _client)
        {
            InitializeComponent();
            Client = _client;
            BindingContext = this;           
        }

        private void buttonEdit_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EditPage(Client));
        }
    }
}