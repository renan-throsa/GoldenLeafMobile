using GoldenLeafMobile.Models;
using GoldenLeafMobile.Models.CategoryModels;
using GoldenLeafMobile.ViewModels.CategoryViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GoldenLeafMobile.Views.CategoryViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditPage : ContentPage
    {
        public EditViewModel ViewModel { get; set; }

        public EditPage(Category category)
        {
            InitializeComponent();
            ViewModel = new EditViewModel(category);
            BindingContext = ViewModel;

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<Category>(this, "SavingEditedCategory", async (_Category) =>
            {
                var confirm = await DisplayAlert("Salvar Categoria", "Deseja mesmo salvar o categoria?", "Não", "Sim");
                if (confirm)
                {
                    ViewModel.SaveCategory();
                }
            });

            MessagingCenter.Subscribe<Category>(this, "SuccessPutCategory", (_msg) =>
            {
                DisplayAlert("Salvar Categoria", "Categoria salva com sucesso!", "Ok");
            });


            MessagingCenter.Subscribe<SimpleHttpResponseException>(this, "FailedPutCategory", (_msg) =>
            {
                DisplayAlert(_msg.ReasonPhrase, _msg.Message, "Ok");
            });

            MessagingCenter.Subscribe<string>(this, ViewModel.ACCESS, async (_msg) =>
            {
                await DisplayAlert("Salvar categoria", $"{_msg} o seu token expirou! Refaça o login.", "Ok");
                await Navigation.PopToRootAsync();
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<Category>(this, "SavingEditedCategory");
            MessagingCenter.Unsubscribe<Category>(this, "SuccessPutCategory");
            MessagingCenter.Unsubscribe<string>(this, ViewModel.ACCESS);
            MessagingCenter.Unsubscribe<ArgumentException>(this, "FailedPutCategory");
        }

    }
}