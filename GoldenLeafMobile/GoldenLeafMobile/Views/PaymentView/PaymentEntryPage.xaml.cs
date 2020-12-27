using GoldenLeafMobile.Models.ClientModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.PaymentView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PaymentEntryPage : ContentPage
    {

        public PaymentEntryPage()
        {
            InitializeComponent();
        }

        public PaymentEntryPage(Client client)
        {
        }
    }
}