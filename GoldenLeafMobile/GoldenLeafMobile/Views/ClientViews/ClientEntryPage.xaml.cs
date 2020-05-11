using GoldenLeafMobile.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ClientViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientEntryPage : ContentPage
    {
        public Client Client { get; set; }
        public ClientEntryPage()
        {
            InitializeComponent();
            Client = new Client();
            BindingContext = this;
        }

        private void buttonSave_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(Client, "ClientSaved");
        }
    }
}