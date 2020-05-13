using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.ClerkModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.ClerkViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {

            base.OnAppearing();
            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, "FailedPostClerk",
                (_msg) =>
                {
                    DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
                });

            MessagingCenter.Subscribe<LoginException>(this, "FailedConnection",
                (_msg) =>
                {
                    DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
                });

        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SimpleHttpResponseException>(this, "FailedPostClerk");
            MessagingCenter.Unsubscribe<LoginException>(this, "FailedConnection");
        }

    }
}