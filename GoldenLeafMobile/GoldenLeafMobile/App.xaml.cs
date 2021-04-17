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
            MessagingCenter.Subscribe<Clerk>(this, "OnSuccessLogin", (_clerk) =>
            {
                Application.Current.Properties["Clerk"] = _clerk;
                MainPage = new MasterDetailView(_clerk);
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
