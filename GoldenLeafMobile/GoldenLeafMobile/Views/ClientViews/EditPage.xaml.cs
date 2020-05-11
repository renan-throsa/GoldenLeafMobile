using GoldenLeafMobile.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        public Client Client { get; set; }

        public EditPage(Client client)
        {
            InitializeComponent();
            Client = client;
            BindingContext = this;
        }

        private void buttonSave_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(Client, "ClientSaved");
        }
    }
}