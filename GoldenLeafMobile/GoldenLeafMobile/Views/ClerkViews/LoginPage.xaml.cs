using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.ViewModels.ClerkViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ClerkViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginViewModel ViewModel { get; private set; }

        public LoginPage()
        {
            InitializeComponent();
            ViewModel = new LoginViewModel();
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, ViewModel.FAILPOST,
                (_msg) =>
                {
                    DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
                });

            MessagingCenter.Subscribe<LoginException>(this, ViewModel.FAILCONNECTION,
                (_msg) =>
                {
                    DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
                });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, ViewModel.FAILPOST);
            MessagingCenter.Unsubscribe<LoginException>(this, ViewModel.FAILCONNECTION);
        }

    }
}