using GoldenLeafMobile.Models.OrderModels;
using GoldenLeafMobile.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.OrderViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrdersPage : ContentPage
    {
        public ListViewModel<Order> ViewModel { get; set; }

        public OrdersPage()
        {
            InitializeComponent();
            ViewModel = new ListViewModel<Order>();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            ViewModel.SearchBy = "Cliente";
            base.OnAppearing();
            MessagingCenter.Subscribe<Order>(this, "SelectedOrder",
                (_order) => Navigation.PushAsync(new DetailsPage(_order)));

            if (ViewModel.Entities.Count == 0)
            {
                await ViewModel.GetEntities();
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Order>(this, "SelectedOrder");
        }

        private async void searchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchString = $"?{ViewModel.SearchBy}={searchField.Text}";
            await ViewModel.GetEntities(searchString);
        }

        private void filterButton_Clicked(object sender, System.EventArgs e)
        {
            picker.Focus();
        }


    }
}