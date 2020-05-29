using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ClientModels;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.OrderViewModel
{
    public class OrderEntryViewModel
    {
        public Client Client { get; }
        public Clerk Clerk { get; }

        public OrderEntryViewModel(Client client)
        {
            Client = client;
            Clerk = Application.Current.Properties["Clerk"] as Clerk;
        }


    }
}
