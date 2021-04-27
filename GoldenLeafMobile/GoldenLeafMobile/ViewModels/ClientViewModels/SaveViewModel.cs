using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ClientModels;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClientViewModels
{
    public class SaveViewModel : BaseClientViewModel
    {               

        public SaveViewModel(Clerk clerk, Client client) : base(clerk, client)
        {
            SaveClientCommand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send<Client>(Client, ASK);

                    },
                    () =>
                    {
                        return !string.IsNullOrEmpty(Client.Name)
                        && !string.IsNullOrEmpty(Client.Address)

                        && !string.IsNullOrEmpty(Client.PhoneNumber);
                    }
                );
        }

    }
}