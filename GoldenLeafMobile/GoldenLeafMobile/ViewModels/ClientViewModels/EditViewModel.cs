using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ClientModels;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClientViewModels
{
    public class EditViewModel : BaseClientViewModel
    {

        public EditViewModel(Clerk clerk, Client client) : base(clerk, client)
        {
            SaveClientCommand = new Command
                (
                    () =>
                    {
                        MessagingCenter.Send(Client, ASK);
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
