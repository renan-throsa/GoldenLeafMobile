using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Models.ProductModels;
using GoldenLeafMobile.ViewModels.ProductViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace GoldenLeafMobile.Views.ProductViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage : ContentPage
    {
        public ProductEntryViewModel ViewModel { get; private set; }
        
        public EntryPage()
        {
            InitializeComponent();
            ViewModel = new ProductEntryViewModel(Application.Current.Properties["Clerk"] as Clerk,new Product());
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
            MessagingCenter.Subscribe<Product>(this, ViewModel.ASK, async (_msg) =>
            {
                var confirm = await DisplayAlert("Salvar produto", "Deseja mesmo salvar o produto?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveProduct();
                }
            });

            MessagingCenter.Subscribe<Product>(this, ViewModel.SUCCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar produto", "Produto salvo com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, ViewModel.FAIL, (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });

            MessagingCenter.Subscribe<string>(this, ViewModel.ACCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar produto", $"{_msg} o seu token expirou! Refaça o login.", "Ok");
                await Navigation.PopToRootAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Product>(this, ViewModel.SUCCESS);
            MessagingCenter.Unsubscribe<Product>(this, ViewModel.ASK);            
            MessagingCenter.Unsubscribe<string>(this, ViewModel.ACCESS);
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAIL);

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