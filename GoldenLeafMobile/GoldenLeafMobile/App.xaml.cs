﻿using GoldenLeafMobile.Models.ClerkModels;
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
            MessagingCenter.Subscribe<Clerk>(this, "SuccessLogin", (_clerk) =>
            {
                //34c39183de01e7fe6224b4c9975d40f6a037ef1fdbe4c10b
                //GQDstcKsx0NHjPOuXOYg5MbeJ1XT0uFiwDVvVBrk
                Application.Current.Properties["Clerk"] = _clerk;
                Application.Current.Properties["Secret"] = "34c39183de01e7fe6224b4c9975d40f6a037ef1fdbe4c10b";
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
