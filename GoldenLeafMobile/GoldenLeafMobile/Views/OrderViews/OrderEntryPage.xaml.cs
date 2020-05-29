using GoldenLeafMobile.Models.ClientModels;
using GoldenLeafMobile.ViewModels.OrderViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.OrderViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderEntryPage : TabbedPage
    {
        public OrderEntryViewModel ViewModel { get; private set; }

        public OrderEntryPage(Client client)
        {
            InitializeComponent();
            ViewModel = new OrderEntryViewModel(client);
            BindingContext = ViewModel;
        }

        private void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {

        }

        private void OnEdit(object sender, System.EventArgs e)
        {

        } 
        
        private void OnDelete(object sender, System.EventArgs e)
        {

        }
    }
}