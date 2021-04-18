using GoldenLeafMobile.Models;
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

        public ProductsPage()
        {
            InitializeComponent();
            ViewModel = new ListViewModel<Product>();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Product>(this, ViewModel.SELECTED,
                (_product) => Navigation.PushAsync(new DetailsPage(_product)));

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, ViewModel.FAIL,
              (_msg) =>
              {
                  DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
              });

            if (ViewModel.Entities.Count == 0)
            {
                await ViewModel.GetEntities();
            }
            else
            {
                listView.SelectedItem = false;
            }

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Product>(this, ViewModel.SELECTED);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAIL);
        }

        public void OnEdit(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Navigation.PushAsync(new EditPage(mi.CommandParameter as Product));
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new EntryPage());
        }

    }
}