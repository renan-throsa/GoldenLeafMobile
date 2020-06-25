using GoldenLeafMobile.Models.OrderModels;
using GoldenLeafMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.OrderViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        public ListViewModel<Item> ViewModel { get; set; }
        public DetailsPage(Order order)
        {
            InitializeComponent();
            ViewModel = new ListViewModel<Item>();
            ViewModel.URL = $"https://golden-leaf.herokuapp.com/api/order/{ order.Id }/items";
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();            
            await ViewModel.GetEntities();
        }
    }
}