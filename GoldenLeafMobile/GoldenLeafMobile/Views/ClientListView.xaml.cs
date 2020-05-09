using GoldenLeafMobile.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoldenLeafMobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ClientListView : ContentPage
    {
        public HashSet<Client> Clients { get; set; }
        public ClientListView()
        {
            InitializeComponent();
            BindingContext = this;
            Clients = new HashSet<Client>() { new Client {Name="Fulano",PhoneNumber="98291510" },
            new Client {Name="Betrano",PhoneNumber="98291510" },
            new Client {Name="Ciclano",PhoneNumber="98291510",Identification="123456",Status=true,Notifiable=false }};
            listViewClient.ItemsSource = Clients;
        }


        public void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Edit", mi.CommandParameter.ToString(), "OK");
        }

        public void OnNewOrder(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            DisplayAlert("Delete", mi.CommandParameter.ToString(), "OK");
        }

        public void OnViewDetails(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Navigation.PushAsync(new ClientDatailsView(mi.CommandParameter as Client));
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {

        }


    }
}
