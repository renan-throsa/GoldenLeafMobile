using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ProductModels;
using GoldenLeafMobile.ViewModels.ProductViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace GoldenLeafMobile.Views.ProductViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductEntryPage : ContentPage
    {
        public ProductEntryViewModel ViewModel { get; private set; }

        public ProductEntryPage()
        {
            InitializeComponent();
            ViewModel = new ProductEntryViewModel();
            BindingContext = ViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await ViewModel.GetCategories();
            SignUpMessages();
        }

        private void SignUpMessages()
        {
            MessagingCenter.Subscribe<Product>(this, "SavingProduct", async (_msg) =>
            {
                var confirm = await DisplayAlert("Salvar produto", "Deseja mesmo salvar o produto?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveProduct();
                }
            });

            MessagingCenter.Subscribe<Product>(this, "SuccessPostProduct", async (_msg) =>
            {
                await DisplayAlert("Salvar produto", "Produto salvo com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, "FailedPostProduct", (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Product>(this, "SuccessPostProduct");
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, "FailedPostProduct");
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            var scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += (result) =>
            {
                scanPage.IsScanning = false;                

                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopModalAsync();
                    Code.Text = result.Text;                                        
                });
            };

            await Navigation.PushModalAsync(scanPage);
        }
    }
}