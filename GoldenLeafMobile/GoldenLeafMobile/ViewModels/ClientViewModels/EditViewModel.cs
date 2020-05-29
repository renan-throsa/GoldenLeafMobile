using GoldenLeafMobile.Models.ClientModels;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClientViewModels
{
    public class EditViewModel : BaseClientViewModel
    {

        public EditViewModel(Client client) : base(client)
        {            
            SaveClientComand = new Command
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
