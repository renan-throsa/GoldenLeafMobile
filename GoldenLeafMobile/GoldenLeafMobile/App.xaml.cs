using GoldenLeafMobile.Models.ClerkModels;
using GoldenLeafMobile.Views;
using GoldenLeafMobile.Views.ClerkViews;
using Xamarin.Forms;

namespace GoldenLeafMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            MessagingCenter.Subscribe<Clerk>(this, "SuccessLogin", (_mgs) =>
            {
                MainPage = new NavigationPage(new ClientsPage());
            });
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
