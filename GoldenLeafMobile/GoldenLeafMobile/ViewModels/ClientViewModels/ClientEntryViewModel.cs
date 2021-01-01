using GoldenLeafMobile.Models.ClientModels;
using Xamarin.Forms;

namespace GoldenLeafMobile.ViewModels.ClientViewModels
{
    public class ClientEntryViewModel : BaseClientViewModel
    {

        public string Name
        {
            get { return Client.Name; }
            set { Client.Name = value; ((Command)SaveClientComand).ChangeCanExecute(); }
        }
                

        public ClientEntryViewModel(Client client) : base(client)
        {
            SaveClientComand = new Command
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
