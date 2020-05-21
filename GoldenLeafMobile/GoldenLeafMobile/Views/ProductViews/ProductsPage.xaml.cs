using GoldenLeafMobile.Models.ProductModels;
using GoldenLeafMobile.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ProductViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductsPage : ContentPage
    {
        public ListViewModel<Product> ViewModel { get; set; }
        private const string MESSAGE_KEY = "SelectedProduct";

        public ProductsPage()
        {
            InitializeComponent();
            ViewModel = new ListViewModel<Product>();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listView.SelectedItem = false;
            MessagingCenter.Subscribe<Product>(this, MESSAGE_KEY,
                (_product) => Navigation.PushAsync(new DetailsPage(_product)));

            await ViewModel.GetEntities();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Product>(this, MESSAGE_KEY);
        }

        public void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Navigation.PushAsync(new EditPage(mi.CommandParameter as Product));
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}