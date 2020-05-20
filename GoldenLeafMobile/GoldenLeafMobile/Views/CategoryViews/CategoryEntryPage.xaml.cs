using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.ViewModels.CategoryViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.CategoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryEntryPage : ContentPage
    {
        public CategoryEntryViewModel ViewModel { get; set; }
        public CategoryEntryPage()
        {
            InitializeComponent();
            ViewModel = new CategoryEntryViewModel();
            BindingContext = ViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SignUpMessages();
        }

        private void SignUpMessages()
        {
            MessagingCenter.Subscribe<Category>(this, "SavingCategory", async (_category) =>
            {
                var confirm = await DisplayAlert("Salvar categoria", "Deseja mesmo salvar a categoria?", "Sim", "Não");
                if (confirm)
                {
                    ViewModel.SaveCategory();
                }
            });

            MessagingCenter.Subscribe<Category>(this, "SuccessPostCategory", async (_msg) =>
            {
                await DisplayAlert("Salvar categoria", "Categoria salva com sucesso!", "Ok");
                await Navigation.PopToRootAsync();
            });

            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, "FailedPostCategory", (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Client>(this, "SuccessPostCategory");
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FailedPostCategory");
        }
    }
}